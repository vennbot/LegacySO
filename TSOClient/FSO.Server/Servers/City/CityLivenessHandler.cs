
// If a copy of the MPL was not distributed with this file, You can obtain one at https://mozilla.org/MPL/2.0.

/*
    Original Source: FreeSO (https://github.com/riperiperi/FreeSO)
    Original Author(s): The FreeSO Development Team

    Modifications for LegacySO by Benjamin Venn (https://github.com/vennbot):
    - Adjusted to support self-hosted LegacySO servers.
    - Modified to allow the LegacySO game client to connect to a predefined server by default.
    - Gameplay logic changes for a balanced and fair experience.
    - Updated references from "FreeSO" to "LegacySO" where appropriate.
    - Other changes documented in commit history and project README.

    Credit is retained for the original FreeSO project and its contributors.
*/
using FSO.Server.Common;
using FSO.Server.Framework.Aries;
using FSO.Server.Protocol.Electron.Packets;
using FSO.Server.Servers.City.Domain;
using System;
using System.Collections.Generic;
using System.Threading;

namespace FSO.Server.Servers.City
{
    /// <summary>
    /// This is used to periodically check if connections which have not sent any messages to us recently are alive.
    /// It is also used to prevent SQL related crashes due to over-parallelization when too many people disconnect from the server.
    /// (since freeing claims goes through multiple sequential steps, doing too many at once will buffer up a lot of connections)
    /// </summary>

    public class CityLivenessEngine
    {
        private Thread LivenessThread;
        private List<Action> LivenessActions = new List<Action>();
        private AutoResetEvent OnChange = new AutoResetEvent(true);
        private ISessions Sessions;
        private EventSystem Events;
        private bool Alive;

        public CityLivenessEngine(ISessions sessions, EventSystem events)
        {
            Sessions = sessions;
            Events = events;
        }

        public void Start()
        {
            Alive = true;
            LivenessThread = new Thread(Run);
            LivenessThread.Start();
        }

        public void EnqueueChange(Action change)
        {
            if (!Alive) return;
            lock (LivenessActions)
            {
                LivenessActions.Add(change);
                OnChange.Set();
            }
        }

        private void Run()
        {
            while (Alive)
            {
                //run background actions that should not be heavily parallelized.
                List<Action> actionCopy;
                lock (LivenessActions)
                {
                    actionCopy = new List<Action>(LivenessActions);
                    LivenessActions.Clear();
                }

                foreach (var action in actionCopy)
                {
                    action();
                }

                //check connections for inactivity
                //sending a packet to the recipient should close their connection if they are unreachable.

                var sessions = Sessions.Clone();
                var now = Epoch.Now;
                foreach (var s in sessions)
                {
                    try
                    {
                        if ((now - s.LastRecv) > 60)
                        {
                            s.Write(new KeepAlive());
                            s.LastRecv = now;
                        }
                    }
                    catch { }
                }

                Events.TickEvents();

                OnChange.WaitOne(30000);
            }
        }

        public void Stop()
        {
            Alive = false;
            OnChange.Set();
            LivenessThread.Join();
            OnChange.Dispose();
        }
    }
}
