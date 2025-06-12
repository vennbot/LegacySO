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
using Newtonsoft.Json;

namespace FSO.Common.Utils
{
    public class Base64JsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            if (typeof(byte[]).IsAssignableFrom(objectType))
            {
                return true;
            }
            return false;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            string data = (string)reader.Value;
            return Convert.FromBase64String(data);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            byte[] bytes = (byte[])value;
            writer.WriteValue(Convert.ToBase64String(bytes));
        }
    }
}
