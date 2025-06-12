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
using FSO.Common.DataService;
using FSO.Common.DataService.Model;
using FSO.Common.Utils;
using FSO.SimAntics;
using FSO.SimAntics.Model.TSOPlatform;
using System.Threading;

namespace FSO.Client.Network
{
    public class VMDataServiceNameCache : VMBasicAvatarNameCache
    {
        private IClientDataService DataService;
        public VMDataServiceNameCache(IClientDataService dataService)
        {
            DataService = dataService;
        }

        public override bool Precache(VM vm, uint persistID)
        {
            if (!base.Precache(vm, persistID))
            {
                //we need to ask the data service for this name
                DataService.Request(Server.DataService.Model.MaskedStruct.Messaging_Icon_Avatar, persistID).ContinueWith(x =>
                {
                    if (x.IsFaulted || x.IsCanceled || x.Result == null) return;
                    var ava = (Avatar)x.Result;
                    var failCount = 0;
                    while (ava.Avatar_Name == "Retrieving...")
                    {
                        if (failCount++ > 100) return;
                        Thread.Sleep(100);
                    }
                    GameThread.NextUpdate(y =>
                    {
                        AvatarNames[persistID] = ava.Avatar_Name;
                    });
                });
            }
            return true;
        }
    }
}
