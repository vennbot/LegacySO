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
using Mina.Core.Session;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FSO.Server.Framework.Aries
{
    public class AriesSession : IAriesSession
    {
        public bool IsAuthenticated { get; set; }
        public uint LastRecv { get; set; }
        public IoSession IoSession;
        protected Dictionary<string, object> MigratableAttributes = new Dictionary<string, object>();
        public TaskCompletionSource<bool> DisconnectSource = new TaskCompletionSource<bool>();
        public TaskCompletionSource<bool> AuthSource = new TaskCompletionSource<bool>();

        public AriesSession(IoSession ioSession)
        {
            this.IoSession = ioSession;
            IsAuthenticated = false;
        }

        public void TimeoutIfNoAuth(int time)
        {
            Task.Run(async () =>
            {
                var timeout = Task.Delay(time);
                await Task.WhenAny(timeout, DisconnectSource.Task, AuthSource.Task);
                if (!IsAuthenticated && IoSession.Connected)
                {
                    Close();
                }
            });
        }

        public void Authenticate(string secret)
        {
            IsAuthenticated = true;
            SetAttribute("sessionKey", secret);
            AuthSource.SetResult(true);
        }

        public virtual void Migrate(IoSession newSession)
        {
            //migrate this aries session and all attributes onto a new IoSession.
            lock (MigratableAttributes)
            {
                foreach (var attr in MigratableAttributes)
                {
                    newSession.SetAttribute(attr.Key, attr.Value);
                }
            }
            IoSession.SetAttribute("migrated", true);
            DisconnectSource = new TaskCompletionSource<bool>();
            newSession.SetAttribute("s", this);
            IoSession = newSession;
        }

        public bool Connected
        {
            get
            {
                return IoSession?.Connected ?? false;
            }
        }

        public virtual void Close()
        {
            SetAttribute("dc", true);
            //if we're being kept alive, cut the disconnection timeout short
            DisconnectSource.TrySetResult(true);
            this.IoSession.Close(false);
        }
        
        public void Write(params object[] messages)
        {
            //TODO: Frame this more efficiently
            foreach(var message in messages)
            {
                this.IoSession.Write(message);
            }
        }

        public override string ToString()
        {
            return IoSession.ToString();
        }

        public T UpgradeSession<T>() where T : AriesSession {
            var instance = (T)Activator.CreateInstance(typeof(T), new object[] { IoSession });
            instance.IsAuthenticated = this.IsAuthenticated;
            instance.DisconnectSource = this.DisconnectSource;
            instance.AuthSource = this.AuthSource;
            IoSession.SetAttribute("s", instance);
            return instance;
        }

        public object GetAttribute(string key)
        {
            return IoSession.GetAttribute(key);
        }

        public void SetAttribute(string key, object value)
        {
            lock (MigratableAttributes) MigratableAttributes[key] = value;
            IoSession.SetAttribute(key, value);
        }
    }
}
