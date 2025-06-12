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
using FSO.SimAntics.NetPlay.EODs.Model;
using System.Linq;
using FSO.SimAntics.Engine.TSOGlobalLink.Model;
using FSO.SimAntics.Model.TSOPlatform;
using System.Text.RegularExpressions;

namespace FSO.SimAntics.NetPlay.EODs.Handlers
{
    public class VMEODRackOwnerPlugin : VMAbstractEODRackPlugin
    {
        //TODO: Read this from tuning variables?
        public const int MAX_OUTFITS = 20;
        public static Regex PRICE_VALIDATION = new Regex("^([1-9]){1}([0-9]){0,5}$");


        public VMEODRackOwnerPlugin(VMEODServer server) : base(server)
        {
            PlaintextHandlers["rackowner_update_name"] = UpdateNameHandler;
            PlaintextHandlers["rackowner_stock"] = Stock;
            PlaintextHandlers["rackowner_delete"] = DeleteStock;
            PlaintextHandlers["rackowner_update_price"] = UpdatePrice;
        }

        private void UpdateNameHandler(string evt, string proposedNewName, VMEODClient client)
        {
            // validate the proposedNewName, if valid store to be sent to server upon disconnection
            // should there be other methods of validating in case of illegal characters?
            bool isOwner = (((VMTSOObjectState)Server.Object.TSOState).OwnerID == client.Avatar.PersistID);
            if ((proposedNewName.Length > 0) && (proposedNewName.Length < 33) && (isOwner))
            {
                HasRackNameChanged = true;
                ProposedNewRackName = proposedNewName;
            }
        }

        private void UpdatePrice(string evt, string data, VMEODClient client)
        {
            bool isOwner = (((VMTSOObjectState)Server.Object.TSOState).OwnerID == client.Avatar.PersistID);
            if ((Lobby.GetPlayerSlot(client) != 0) || (!isOwner))
            {
                return;
            }

            var split = data.Split(',');
            uint outfitId = 0;
            int newSalePrice = 0;
            
            if(!uint.TryParse(split[0], out outfitId) || 
                !int.TryParse(split[1], out newSalePrice)){
                return;
            }

            if (!PRICE_VALIDATION.IsMatch(newSalePrice.ToString())){
                return;
            }

            var VM = client.vm;
            
            VM.GlobalLink.UpdateOutfitSalePrice(VM, outfitId, Server.Object.PersistID,newSalePrice, success => {
                BroadcastOutfits(VM, false);
            });
        }

        private void DeleteStock(string evt, string data, VMEODClient client)
        {
            bool isOwner = (((VMTSOObjectState)Server.Object.TSOState).OwnerID == client.Avatar.PersistID);
            if ((Lobby.GetPlayerSlot(client) != 0) || (!isOwner))
            {
                return;
            }

            uint outfitId;
            var valid = uint.TryParse(data, out outfitId);

            if (!valid)
                return;

            var VM = client.vm;

            //TODO: Some kind of refund?
            VM.GlobalLink.DeleteOutfit(VM, outfitId, VMGLOutfitOwner.OBJECT, Server.Object.PersistID, success => {
                BroadcastOutfits(VM, true);
            });
        }

        private void Stock(string evt, string data, VMEODClient client){
            bool isOwner = (((VMTSOObjectState)Server.Object.TSOState).OwnerID == client.Avatar.PersistID);
            if ((Lobby.GetPlayerSlot(client) != 0) || (!isOwner))
            {
                return;
            }

            ulong outfitAssetId;
            var valid = ulong.TryParse(data, out outfitAssetId);

            if (!valid)
                return;

            var outfit = Content.Content.Get().RackOutfits.GetByRackType(RackType).Outfits.FirstOrDefault(x => x.AssetID == outfitAssetId);
            if (outfit == null) { return; }

            var VM = client.vm;

            //TODO: Do stores get bulk discounts on clothes?
            VM.GlobalLink.PerformTransaction(VM, false, client.Avatar.PersistID, uint.MaxValue, (int)outfit.Price,
                        
                (bool success, int transferAmount, uint uid1, uint budget1, uint uid2, uint budget2) =>
                {
                    if (success)
                    {
                        //Create the outfit
                        VM.GlobalLink.StockOutfit(VM, new VMGLOutfit {
                            owner_type = VMGLOutfitOwner.OBJECT,
                            owner_id = Server.Object.PersistID,
                            asset_id = outfit.AssetID,
                            purchase_price = outfit.Price,
                            sale_price = outfit.Price,
                            outfit_type = (byte)GetSuitType(this.RackType),
                            outfit_source = VMGLOutfitSource.rack
                        }, (bool created, uint outfitId) => {
                            client.SendOBJEvent(new VMEODEvent((short)VMEODRackEvent.StockOutfit, 0));
                            BroadcastOutfits(VM, true);
                        });
                    }else{
                        client.SendOBJEvent(new VMEODEvent((short)VMEODRackEvent.StockOutfit, 0));
                    }
            });
        }
    }
}
