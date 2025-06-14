
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
using FSO.Server.DataService.Model;
using System;

namespace FSO.Client.Controllers.Panels
{
    /// <summary>
    /// Used to obtain our lot's name.
    /// </summary>
    public class SecureTradeController
    {
        private Network.Network Network;
        private IClientDataService DataService;

        public SecureTradeController(IClientDataService dataService, Network.Network network)
        {
            this.Network = network;
            this.DataService = dataService;
        }

        /// <summary>
        /// Finds a lot owned by us, then returns its name, along with if we are its owner.
        /// </summary>
        /// <param name="callback">A function to take the name and owner status found. Name is null if a lot was not found.</param>
        public void GetOurLotsName(Action<string, bool> callback)
        {
            DataService.Request(MaskedStruct.SimPage_Main, Network.MyCharacter)
                .ContinueWith(x =>
                {
                    var avatar = x.Result as Avatar;
                    if (avatar == null) return;

                    var lotLoc = avatar.Avatar_LotGridXY;
                    if (lotLoc == 0) GameThread.NextUpdate(state => callback(null, false));
                    else
                    {
                        DataService.Request(MaskedStruct.PropertyPage_LotInfo, lotLoc).ContinueWith(y =>
                        {
                            var lot = y.Result as Lot;
                            if (lot == null) GameThread.NextUpdate(state => callback(null, false));
                            else {
                                GameThread.NextUpdate(state => {
                                    callback(lot.Lot_Name, lot.Lot_LeaderID == Network.MyCharacter);
                                });
                            }
                        });
                    }
                });
        }
    }
}
