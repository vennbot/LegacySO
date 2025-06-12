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
using FSO.Client.Regulators;
using FSO.Client.UI.Controls;
using FSO.Client.UI.Framework;
using FSO.Client.UI.Screens;
using FSO.Client.Utils;
using FSO.Server.Protocol.Electron.Packets;
using FSO.Server.Protocol.Voltron.Packets;
using System;

namespace FSO.Client.Controllers
{
    public class PersonSelectionEditController : IDisposable
    {
        private PersonSelectionEdit View;
        private CreateASimRegulator CASRegulator;

        public PersonSelectionEditController(PersonSelectionEdit view, CreateASimRegulator casRegulator)
        {
            this.View = view;
            this.CASRegulator = casRegulator;

            CASRegulator.OnTransition += CASRegulator_OnTransition;
        }
        
        private void CASRegulator_OnTransition(string state, object data)
        {
            Common.Utils.GameThread.NextUpdate(x =>
            {
                switch (state)
                {
                    case "Idle":
                        break;
                    case "CreateSim":
                        break;
                    case "Waiting":
                        //Show waiting dialog
                        View.ShowCreationProgressBar(true);
                        break;
                    case "Error":
                        if (data != null)
                        {
                            var casr = (CreateASimResponse)data;
                            if (casr.Reason == CreateASimFailureReason.NAME_TAKEN)
                            {
                                ShowError(ErrorMessage.FromUIText("222", "4", "5"));
                            }
                            else
                            {
                                //name validation error... just say its in use for now (unless client validation incorrect, should not appear.
                                ShowError(ErrorMessage.FromUIText("222", "4", "5"));
                            }
                        }
                        View.ShowCreationProgressBar(false);
                        break;
                    case "Success":
                        //Connect to the city with our new avatar
                        var response = (CreateASimResponse)data;
                        FSOFacade.Controller.ConnectToCity(null, response.NewAvatarId, null);
                        break;
                }
            });
        }

        private void ShowError(ErrorMessage errorMsg) { 
            /** Error message intended for the user **/
            UIAlertOptions Options = new UIAlertOptions();
            Options.Message = errorMsg.Message;
            Options.Title = errorMsg.Title;
            Options.Buttons = errorMsg.Buttons;
            UIScreen.GlobalShowAlert(Options, true);
        }

        public void Create(){
            var skinTone = Server.Protocol.Voltron.Model.SkinTone.LIGHT;
            switch (View.AppearanceType)
            {
                case Vitaboy.AppearanceType.Medium:
                    skinTone = Server.Protocol.Voltron.Model.SkinTone.MEDIUM;
                    break;
                case Vitaboy.AppearanceType.Dark:
                    skinTone = Server.Protocol.Voltron.Model.SkinTone.DARK;
                    break;
            }

            var packet = new RSGZWrapperPDU
            {
                BodyOutfitId = (uint)(View.BodyOutfitId >> 32),
                HeadOutfitId = (uint)(View.HeadOutfitId >> 32),
                Name = View.Name,
                Description = View.Description,
                Gender = View.Gender == Gender.Male ? Server.Protocol.Voltron.Model.Gender.MALE : Server.Protocol.Voltron.Model.Gender.FEMALE,
                SkinTone = skinTone
            };

            CASRegulator.CreateSim(packet);
        }

        public void Cancel(){
            if (CASRegulator.CurrentState.Name == "Idle")
            {
                //Cant cancel while cas in progress
                FSOFacade.Controller.Disconnect();
            }
        }

        public void Dispose()
        {
            CASRegulator.OnTransition -= CASRegulator_OnTransition;
        }
    }
}
