
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
using FSO.Client.Network;
using FSO.Client.UI.Panels;
using FSO.Common.DataService;
using FSO.Common.DataService.Model;
using FSO.Server.DataService.Model;
using System.Collections.Generic;

namespace FSO.Client.Controllers.Panels
{
    public class LotPageController
    {
        private UILotPage View;
        private IClientDataService DataService;
        private ITopicSubscription Topic;
        private uint LotId;

        public LotPageController(UILotPage view, IClientDataService dataService)
        {
            this.View = view;
            this.DataService = dataService;
            this.Topic = dataService.CreateTopicSubscription();
        }

        ~LotPageController(){
            Topic.Dispose();
        }

        public void Close()
        {
            View.TrySaveDescription();
            View.Visible = false;
            ChangeTopic();
        }

        public void Show(uint lotId)
        {
            View.TrySaveDescription();
            LotId = lotId;
            DataService.Get<Lot>(lotId).ContinueWith(x =>
            {
                View.CurrentLot.Value = x.Result;
                DataService.Request(MaskedStruct.AdmitInfo_Lot, lotId);
            });
            View.Parent.Add(View);
            View.Visible = true;
            View.AsyncAPIThumb(lotId);
            ChangeTopic();
            FSOFacade.Hints.TriggerHint("ui:lot_page");
        }

        public void SaveDescription(Lot target)
        {
            DataService.Sync(target, new string[] { "Lot_Description" });
        }
        
        public void SaveCategory(Lot target)
        {
            DataService.Sync(target, new string[] { "Lot_Category" });
        }

        public void SaveSkillmode(Lot target)
        {
            DataService.Sync(target, new string[] { "Lot_SkillGamemode" });
        }

        public void SaveName(Lot target)
        {
            DataService.Sync(target, new string[] { "Lot_Name" });
        }

        private void ChangeTopic()
        {
            List<ITopic> topics = new List<ITopic>();
            if (View.Visible && LotId != 0)
            {
                topics.Add(Topics.For(MaskedStruct.PropertyPage_LotInfo, LotId));
            }
            Topic.Set(topics);
        }
    }
}
