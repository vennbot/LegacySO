// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
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
using System;
using System.Collections.Generic;
using System.Threading;

namespace MonoGame.Tests.Utilities
{
    internal class ActionDaemon
    {
        private readonly Queue<Action> _actions = new Queue<Action>();
        private Thread _thread;

        public Thread Thread
        {
            get { return _thread; }
        }

        public bool Finished
        {
            get { return _actions.Count == 0 && (_thread == null || _thread.IsAlive == false); }
        }

        public void AddAction(Action action)
        {
            lock (_actions)
                _actions.Enqueue(action);

            if (_thread == null || !_thread.IsAlive)
                Start();
        }

        public void ForceTermination()
        {
            _thread.Abort();
        }

        public void Clear(bool abortCurrent = false)
        {
            lock (_actions)
                _actions.Clear();

            if (abortCurrent)
                ForceTermination();
        }

        private void Start()
        {
            _thread = new Thread(DoActions);
            _thread.Priority = ThreadPriority.Lowest;
            _thread.IsBackground = true;
            _thread.Start();
        }

        private void DoActions()
        {
            while (true)
            {
                Action currentAction;
                lock (_actions)
                {
                    if (_actions.Count == 0)
                        break;
                    currentAction = _actions.Dequeue();
                }
                currentAction();
            }
        }

    }
}
