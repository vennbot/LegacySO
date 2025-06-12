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
using FSO.Client.UI.Panels;
using FSO.Common.DatabaseService;
using FSO.Common.DatabaseService.Model;
using FSO.Common.DataService;
using FSO.Common.DataService.Model;
using FSO.Common.Utils;
using FSO.Server.DataService.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FSO.Client.Controllers.Panels
{
    public class RelationshipDialogController : IDisposable
    {
        private Network.Network Network;
        private IClientDataService DataService;
        private UIRelationshipDialog View;
        private IDatabaseService DatabaseService;
        //private Binding<Avatar> Binding;

        public RelationshipDialogController(UIRelationshipDialog view, IClientDataService dataService, Network.Network network, IDatabaseService database)
        {
            this.Network = network;
            this.DataService = dataService;
            this.View = view;
            this.DatabaseService = database;
            //this.Binding = new Binding<Avatar>().WithMultiBinding(x => { RefreshResults(); }, "Avatar_BookmarksVec");

            //Init();
        }

        public void Toggle(uint character)
        {
            if (View.Visible)
            {
                Close();
            }
            else
            {
                Show(character);
            }
        }

        public void Close()
        {
            View.Visible = false;
        }

        public void Show(uint character)
        {
            View.Parent.Add(View);
            if (!View.Visible)
            {
                View.CenterAround(GameFacade.Screens.CurrentUIScreen);
            }
            View.Visible = true;
            View.SetPersonID(character);

            DataService.Request(MaskedStruct.FriendshipWeb_Avatar, character).ContinueWith(y =>
            {
                View.UpdateRelationships(((Avatar)y.Result).Avatar_FriendshipVec);
            });

            DataService.Get<Avatar>(character).ContinueWith(x =>
            {
                var sim = x.Result;
                if (x.Result != null && x.Result.Avatar_LotGridXY != 0)
                {
                    DataService.Request(MaskedStruct.PropertyPage_LotInfo, x.Result.Avatar_LotGridXY).ContinueWith(y =>
                    {
                        View.SetRoommates(((Lot)y.Result).Lot_RoommateVec);
                    });
                }
            });
        }

        public void Search(string name)
        {
            DatabaseService.Search(new SearchRequest { Query = name, Type = SearchType.SIMS }, false)
            .ContinueWith(x =>
            {
                GameThread.InUpdate(() =>
                {
                    View.SetFilter(new HashSet<uint>(x.Result.Items.Select(y => y.EntityId)));
                });
            });
        }

        public void Dispose()
        {
        }
    }
}
