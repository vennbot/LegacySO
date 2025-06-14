
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
using FSO.SimAntics.NetPlay.Model;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FSO.SimAntics.NetPlay.EODs.Utils
{
    public class EODPersist<T> where T : VMSerializable
    {
        private VMEODServer Server;
        private T CurrentData;
        private TaskCompletionSource<T> CurrentDataTask;

        public EODPersist(VMEODServer server)
        {
            Server = server;
            Load();   
        }

        public void Patch(Func<T, T> callback)
        {
            GetData().ContinueWith(x =>
            {
                if (x.IsFaulted){
                    return;
                }

                var newValue = callback(x.Result);
                Put(newValue);
            });
        }

        public void Put(T newValue)
        {
            if (newValue == null){
                newValue = CreateDefaultValue();
            }

            var newTask = new TaskCompletionSource<T>();
            newTask.SetResult(newValue);
            CurrentDataTask = newTask;
            CurrentData = newValue;

            using (var stream = new MemoryStream())
            {
                newValue.SerializeInto(new BinaryWriter(stream));
                stream.Seek(0, SeekOrigin.Begin);
                Server.vm.GlobalLink.SavePluginPersist(Server.vm, Server.Object.PersistID, Server.PluginID, stream.ToArray());
            }
        }

        public Task<T> GetData()
        {
            return CurrentDataTask.Task;
        }

        protected virtual void Load()
        {
            CurrentDataTask = new TaskCompletionSource<T>();
            Server.vm.GlobalLink.LoadPluginPersist(Server.vm, Server.Object.PersistID, Server.PluginID, (byte[] data) =>
            {
                T newValue;

                if(data == null){
                    //We expect the constructor to set sensible defaults
                    newValue = CreateDefaultValue();
                }
                else{
                    newValue = Activator.CreateInstance<T>();
                    newValue.Deserialize(new BinaryReader(new MemoryStream(data)));
                }
                InternalUpdate(newValue);
            });
        }

        protected T CreateDefaultValue()
        {
            return Activator.CreateInstance<T>();
        }

        private void InternalUpdate(T value)
        {
            CurrentData = value;
            CurrentDataTask.SetResult(value);
        }
    }
}
