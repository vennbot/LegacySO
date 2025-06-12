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
using FSO.SimAntics.Engine.TSOGlobalLink.Model;
using FSO.SimAntics.NetPlay.EODs.Model;
using FSO.SimAntics.NetPlay.Model.Commands;
using System.Linq;

namespace FSO.SimAntics.NetPlay.EODs.Handlers
{
    public class VMEODRackPlugin : VMAbstractEODRackPlugin
    {
        public VMEODRackPlugin(VMEODServer server) : base(server)
        {
            PlaintextHandlers["rack_try_outfit_on"] = TryOutfitOn;
            PlaintextHandlers["rack_purchase"] = Purchase;
        }

        private void Purchase(string evt, string data, VMEODClient client)
        {
            var split = data.Split(',');
            if (split.Length != 2) { return; }

            uint outfitId = 0;
            if (!uint.TryParse(split[0], out outfitId)){
                return;
            }

            var putOnNow = false;
            if(!bool.TryParse(split[1], out putOnNow)){
                return;
            }

            var VM = client.vm;


            GetOutfit(VM, outfitId, outfit =>
            {
                if (outfit == null) { return; }


                //Make sure we don't already have this outfit, can't have an outfit twice
                VM.GlobalLink.GetOutfits(VM, VMGLOutfitOwner.AVATAR, Controller.Avatar.PersistID, avatarOutfits =>
                {
                    if(avatarOutfits.FirstOrDefault(x => x.asset_id == outfit.asset_id) != null){
                        //I already have this outfit
                        client.Send("rack_buy_error", new byte[] { 0 });
                        return;
                    }

                    var outfitsInCategory = avatarOutfits.Where(x => x.outfit_type == (byte)outfit.outfit_type).ToList();
                    if (outfitsInCategory.Count >= 5)
                    {
                        // I already own 5 outfits of this type
                        client.Send("rack_buy_error", new byte[] { 1 });
                        return;
                    }

                    //Take payment
                    VM.GlobalLink.PerformTransaction(VM, false, client.Avatar.PersistID, Server.Object.PersistID, (int)outfit.sale_price,
                    (bool success, int transferAmount, uint uid1, uint budget1, uint uid2, uint budget2) =>
                    {
                        if (success)
                        {
                            //Transfer outfit to my avatar
                            VM.GlobalLink.PurchaseOutfit(VM, outfit.outfit_id, Server.Object.PersistID, client.Avatar.PersistID, purchaseSuccess => {
                                    if (purchaseSuccess && putOnNow)
                                    {
                                        PutOnNow(outfit, client);
                                    }
                                    if (!purchaseSuccess) {
                                        VM.GlobalLink.PerformTransaction(VM, false, Server.Object.PersistID, client.Avatar.PersistID, (int)outfit.sale_price,
                                        (bool success2, int transferAmount2, uint uid1d, uint budget1d, uint uid2d, uint budget2d) =>
                                        {
                                        });
                                    }

                                    BroadcastOutfits(VM, true);
                                });
                        }
                        else
                        {
                            // purchase failed, did I not have enough money?
                            client.Send("rack_buy_error", new byte[] { 2 });
                        }
                    });


                });
            });
        }

        private void PutOnNow(VMGLOutfit outfit, VMEODClient client)
        {
            var slot = GetSuitSlot(false);
            client.vm.SendCommand(new VMNetSetOutfitCmd
            {
                UID = client.Avatar.PersistID,
                Scope = slot,
                Outfit = outfit.asset_id
            });
            client.SendOBJEvent(new VMEODEvent((short)VMEODRackEvent.PutOnNow, (short)RackType));
        }

        private void TryOutfitOn(string evt, string data, VMEODClient client)
        {
            uint outfitId = 0;
            if(!uint.TryParse(data, out outfitId)){
                return;
            }

            GetOutfit(client.vm, outfitId, outfit => {
                if (outfit == null) { return; }

                var slot = GetSuitSlot(true);

                //store the outfit under dynamic costume
                client.vm.SendCommand(new VMNetSetOutfitCmd {
                    UID = client.Avatar.PersistID,
                    Scope = slot,
                    Outfit = outfit.asset_id
                });
                //3 uses dynamic costume, by using this we avoid updating default outfits without a good reason to
                client.SendOBJEvent(new VMEODEvent((short)VMEODRackEvent.TryOnOutfit, (short)RackType));
            });
        }

        public override void OnDisconnection(VMEODClient client)
        {
            client.SendOBJEvent(new VMEODEvent((short)VMEODRackEvent.PutClothesBack, (short)RackType));
            base.OnDisconnection(client);
        }
    }
}
