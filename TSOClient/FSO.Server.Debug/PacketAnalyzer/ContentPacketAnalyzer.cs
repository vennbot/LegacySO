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
using FSO.Content.Framework;
using FSO.Vitaboy;
using System.Collections.Generic;

namespace FSO.Server.Debug.PacketAnalyzer
{
    public class ContentPacketAnalyzer : ConstantsPacketAnalyzer
    {
        private List<Constant> Constants = new List<Constant>();

        public ContentPacketAnalyzer()
        {
            var content = Content.Content.Get();

            /** Avatar Collections **/
            foreach(var collection in content.AvatarCollections.List())
            {
                var items = collection.Get();
                var collectionCast = (Far3ProviderEntry<Collection>)collection;

                foreach(var item in items)
                {
                    Constants.Add(new Constant {
                        Type = ConstantType.ULONG,
                        Value = item.PurchasableOutfitId,
                        Description = collectionCast.FarEntry.Filename + "." + item.Index
                    });

                    /**Constants.Add(new Constant
                    {
                        Type = ConstantType.UINT,
                        Value = item.FileID,
                        Description = collectionCast.FarEntry.Filename + "." + item.Index
                    });**/
                }
            }


            //TSODataDefinition file
            var dataDef = content.DataDefinition;

            foreach (var str in dataDef.Strings)
            {
                Constants.Add(new Constant
                {
                    Type = ConstantType.UINT,
                    Description = "TSOData_datadefinition(" + str.Value + ")",
                    Value = str.ID
                });
            }
        }

        public override List<Constant> GetConstants()
        {
            return Constants;
        }
    }
}
