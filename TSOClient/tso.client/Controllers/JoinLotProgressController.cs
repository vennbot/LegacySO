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
using FSO.Client.UI.Panels;
using FSO.Common.Rendering.Framework;
using FSO.Common.Utils;
using FSO.Server.Protocol.Electron.Model;
using System;

namespace FSO.Client.Controllers
{
    public class JoinLotProgressController : IDisposable
    {
        private UIJoinLotProgress View;
        LotConnectionRegulator ConnectionReg;

        public JoinLotProgressController(UIJoinLotProgress view, LotConnectionRegulator regulator)
        {
            this.View = view;

            regulator.OnError += Regulator_OnError;
            regulator.OnTransition += Regulator_OnTransition;

            ConnectionReg = regulator;
        }

        public void Dispose()
        {
            ConnectionReg.OnError -= Regulator_OnError;
            ConnectionReg.OnTransition -= Regulator_OnTransition;
        }

        private void Regulator_OnError(object data)
        {
            //UIScreen.RemoveDialog(View);
            GameThread.InUpdate(() =>
            {
                GameFacade.Cursor.SetCursor(CursorType.Normal);

                var errorTitle = GameFacade.Strings.GetString("211", "45");
                var errorBody = GameFacade.Strings.GetString("211", "45");

                if (data is FindLotResponseStatus)
                {
                    var status = (FindLotResponseStatus)data;

                    switch (status)
                    {
                        case FindLotResponseStatus.NOT_OPEN:
                        case FindLotResponseStatus.NOT_PERMITTED_TO_OPEN:
                            errorTitle = GameFacade.Strings.GetString("211", "7");
                            errorBody = GameFacade.Strings.GetString("211", "8");
                            break;
                        case FindLotResponseStatus.NO_CAPACITY:
                            errorTitle = GameFacade.Strings.GetString("211", "11");
                            errorBody = GameFacade.Strings.GetString("211", "12");
                            break;
                        case FindLotResponseStatus.CLAIM_FAILED:
                            errorTitle = GameFacade.Strings.GetString("211", "45");
                            errorBody = GameFacade.Strings.GetString("211", "41");
                            break;
                        case FindLotResponseStatus.NO_ADMIT:
                            errorTitle = GameFacade.Strings.GetString("211", "45");
                            errorBody = GameFacade.Strings.GetString("211", "42");
                            break;
                        default:
                            break;
                    }
                }

                UIAlert alert = null;
                alert = UIScreen.GlobalShowAlert(new UIAlertOptions()
                {
                    Title = errorTitle,
                    Message = errorBody,
                    Buttons = UIAlertButton.Ok(x =>
                    {
                        UIScreen.RemoveDialog(View);
                        UIScreen.RemoveDialog(alert);
                    })
                }, true);
            });
        }

        private void Regulator_OnTransition(string state, object data)
        {
            var progress = 0;

            GameThread.InUpdate(() =>
            {
                switch (state)
                {
                    case "Disconnected":
                        UIScreen.RemoveDialog(View);
                        break;

                    case "SelectLot":
                        UIScreen.GlobalShowDialog(View, true);
                        break;

                    case "FindLot":
                        break;

                    case "FoundLot":
                    case "OpenSocket":
                    case "SocketOpen":
                        progress = 1;
                        break;

                    case "HostOnline":
                    case "RequestClientSession":
                        progress = 2;
                        break;

                    case "PartiallyConnected":
                        progress = 3;
                        break;

                    case "LotCommandStream":
                        progress = 4;
                        break;
                }
                var progressPercent = (((float)progress) / 12.0f) * 100;
                if (progress < 4) View.Progress = progressPercent;
                switch (progress)
                {
                    case 0:
                        View.ProgressCaption = GameFacade.Strings.GetString("211", "4");
                        break;
                    default:
                        View.ProgressCaption = GameFacade.Strings.GetString("211", (21 + progress).ToString());
                        break;
                }
            });
        }
    }
}
