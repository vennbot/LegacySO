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
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Converters;
using FSO.Server.Common;
using FSO.Common.Utils;
using tso.debug.network;

namespace FSO.Server.Debug
{
    public class NetworkStash
    {
        
        private static JsonSerializerSettings SETTINGS;

        static NetworkStash()
        {
            SETTINGS = new JsonSerializerSettings();
            SETTINGS.Formatting = Formatting.Indented;
            SETTINGS.Converters.Add(new StringEnumConverter());
        }



        private string Dir;
        public List<NetworkStashItem> Items;


        public NetworkStash(string dir)
        {
            this.Dir = dir;
            this.Items = new List<NetworkStashItem>();
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }


            string[] files = Directory.GetFiles(dir);
            foreach (var file in files)
            {
                if (file.EndsWith(".json"))
                {
                    var parsedItem = JsonConvert.DeserializeObject<NetworkStashItem>(File.ReadAllText(file), SETTINGS);
                    this.Items.Add(parsedItem);
                }
            }
        }

        public void Add(string name, RawPacketReference[] packets)
        {
            var item = new NetworkStashItem();
            item.Name = name;
            item.Packets = new List<NetworkStashItemPacket>();

            foreach (var packet in packets)
            {
                item.Packets.Add(new NetworkStashItemPacket {
                    Type = packet.Packet.Type,
                    SubType = (ushort)packet.Packet.SubType,
                    Data = packet.Packet.Data,
                    Direction = packet.Packet.Direction
                });
            }

            this.Items.Add(item);

            var jsonData = JsonConvert.SerializeObject(item, SETTINGS);
            File.WriteAllText(Path.Combine(Dir, "stash-" + DateTime.Now.Ticks + ".json"), jsonData);
        }
    }

    public class NetworkStashItem
    {
        [JsonProperty]
        public string Name;

        [JsonProperty]
        public List<NetworkStashItemPacket> Packets;
    }

    public class NetworkStashItemPacket
    {
        [JsonProperty]
        public PacketType Type;

        [JsonProperty]
        public uint SubType;

        [JsonProperty]
        public PacketDirection Direction;

        [JsonConverter(typeof(Base64JsonConverter))]
        public byte[] Data;
    }
}
