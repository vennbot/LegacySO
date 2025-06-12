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
using FSO.Common;
using FSO.Files.Formats.tsodata;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FSO.Client.UI.Panels.MessageStore
{
    public class UIMessageStore
    {
        public int AvatarID = 0;
        public List<MessageItem> Items = new List<MessageItem>();
        public long LastMessageTime;

        private static UIMessageStore _store;
        public static UIMessageStore Store
        {
            get
            {
                if (_store == null)
                    _store = new UIMessageStore();
                return _store;
            }
        } 

        public UIMessageStore()
        {
            
        }

        public void Save(MessageItem newItem)
        {
            if (AvatarID == 0) return;
            int index = Items.FindIndex(x => x.ID == newItem.ID);
            if (index != -1) //replace old message
                Items.RemoveAt(index);
            else
                index = 0;
            var storedir = Path.Combine(FSOEnvironment.UserDir, "Inbox/" + AvatarID.ToString() + "/");
            try
            {
                using (var str = File.Open(Path.Combine(storedir, newItem.ID + ".fsoi"), FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                {
                    newItem.Save(str);
                }
                if (newItem.Time > LastMessageTime)
                {
                    LastMessageTime = newItem.Time;
                    using (var str = new StreamWriter(File.Open(Path.Combine(storedir, "timestamp.txt"), FileMode.Create, FileAccess.Write, FileShare.ReadWrite)))
                    {
                        str.Write(newItem.Time.ToString());
                    }
                }
            } catch
            {
                //something went wrong, but we shouldnt crash the game over it.
            }
            Items.Insert(index, newItem);
        }

        public void Delete(int id)
        {
            int index = Items.FindIndex(x => x.ID == id);
            if (index != -1) //delete from inbox
                Items.RemoveAt(index);

            //delete from local storage too. don't change timestamp - it's still relevant.
            var storedir = Path.Combine(FSOEnvironment.UserDir, "Inbox/" + AvatarID.ToString() + "/");
            try
            {
                File.Delete(Path.Combine(storedir, id + ".fsoi"));
            }
            catch { }
        }

        public void Load(int avatarID)
        {
            AvatarID = avatarID;
            var items = new List<MessageItem>();

            //load messages from the store
            var storedir = Path.Combine(FSOEnvironment.UserDir, "Inbox/" + avatarID.ToString() + "/");
            Directory.CreateDirectory(storedir);
            var files = Directory.EnumerateFiles(storedir);
            foreach (var file in files)
            {
                if (Path.GetFileName(file) == "timestamp.txt")
                {
                    //load the last timestamp from the file
                    using (var str = new StreamReader(File.Open(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
                    {
                        var line = str.ReadLine();
                        LastMessageTime = long.Parse(line);
                    }
                } else if (file.ToLower().EndsWith(".fsoi"))
                {
                    //mail message
                    using (var str = File.Open(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        var mail = new MessageItem(str);
                        items.Add(mail);
                    }
                }
            }

            Items = items.OrderBy(x => -x.Time).ToList();
        }
    }
}
