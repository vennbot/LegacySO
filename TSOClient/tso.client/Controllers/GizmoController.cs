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
using FSO.Common.DataService;
using FSO.Common.DataService.Model;
using FSO.Common.Enum;
using System;

namespace FSO.Client.Controllers
{
    public class GizmoController : IDisposable
    {
        private UIGizmo Gizmo;
        private Network.Network Network;
        private IClientDataService DataService;

        public GizmoController(UIGizmo view, Network.Network network, IClientDataService dataService)
        {
            this.Gizmo = view;
            this.Network = network;
            this.DataService = dataService;

            Initialize();
        }

        private void Initialize()
        {
            DataService.Get<Avatar>(Network.MyCharacter).ContinueWith(x =>
            {
                if (!x.IsFaulted){
                    Gizmo.CurrentAvatar.Value = x.Result;
                    FSO.UI.Model.DiscordRpcEngine.SendFSOPresence(x.Result.Avatar_Name, null, 0, 0, 0, 0, null, x.Result.Avatar_PrivacyMode > 0);
                }
            });
        }

        public void Dispose()
        {
            try {
                Gizmo.CurrentAvatar.Value = null;
            }catch(Exception ex){
            }
        }

        public void RequestFilter(LotCategory cat)
        {
            if (Gizmo.CurrentAvatar != null && Gizmo.CurrentAvatar.Value != null)
            {
                Gizmo.CurrentAvatar.Value.Avatar_Top100ListFilter.Top100ListFilter_Top100ListID = (uint)cat;
                DataService.Sync(Gizmo.CurrentAvatar.Value, new string[] { "Avatar_Top100ListFilter.Top100ListFilter_Top100ListID" });
            }
        }

        public void ClearFilter()
        {
            Gizmo.FilterList = System.Collections.Immutable.ImmutableList<uint>.Empty;
        }
    }
}
