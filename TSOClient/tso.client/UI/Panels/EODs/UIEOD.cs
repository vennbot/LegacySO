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
using FSO.Client.UI.Controls;
using FSO.Client.UI.Framework;
using FSO.Client.UI.Screens;
using FSO.SimAntics.NetPlay.EODs.Handlers;
using FSO.SimAntics.NetPlay.Model.Commands;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FSO.Client.UI.Panels.EODs
{
    public abstract class UIEOD : UIContainer
    {
        public Dictionary<string, EODDirectPlaintextEventHandler> PlaintextHandlers;
        public Dictionary<string, EODDirectBinaryEventHandler> BinaryHandlers;

        public UIEODController EODController;

        public UIEOD(UIEODController controller)
        {
            PlaintextHandlers = new Dictionary<string, EODDirectPlaintextEventHandler>();
            BinaryHandlers = new Dictionary<string, EODDirectBinaryEventHandler>();
            EODController = controller;
        }

        public UILotControl LotController
        {
            get
            {
                var screen = UIScreen.Current as IGameScreen;
                if (screen == null){
                    return null;
                }

                return screen.LotControl;
            }
        }

        public virtual void OnExpand()
        {
        }

        public virtual void OnContract()
        {
        }

        public virtual void OnClose()
        {
            EODController.CloseEOD();
        }

        public void SetTip(string txt)
        {
            EODController.EODMessage = txt;
        }

        public void SetTime(int time)
        {
            EODController.EODTime = " "+((time<0)?"":((time/60)+":"+((time%60).ToString().PadLeft(2, '0'))));
        }

        public void CloseInteraction()
        {
            var me = EODController.Lot.ActiveEntity;
            if (me != null)
            {
                var action = me.Thread.Queue.FirstOrDefault(x => x.Mode != SimAntics.Engine.VMQueueMode.Idle);
                if (action != null)
                {
                    EODController.Lot.vm.SendCommand(new VMNetInteractionCancelCmd
                    {
                        ActionUID = action.UID
                    });
                }
            }
        }

        public void SimpleUIAlert(string title, string message)
        {
            SimpleUIAlert(title, message, null);
        }

        public void SimpleUIAlert(string title, string message, Action action)
        {
            UIAlert alert = null;
            alert = UIScreen.GlobalShowAlert(new UIAlertOptions()
            {
                TextSize = 12,
                Title = title,
                Message = message,
                Alignment = TextAlignment.Center,
                TextEntry = false,
                Buttons = UIAlertButton.Ok((btn) =>
                {
                    UIScreen.RemoveDialog(alert);
                    action?.Invoke();
                }),
            }, true);
        }

        public void Send(string evt, string data)
        {
            if (data == null) return;
            EODController.Lot.vm.SendCommand(new VMNetEODMessageCmd
            {
                PluginID = EODController.ActivePID,
                EventName = evt,
                Binary = false,
                TextData = data
            });
        }

        public void Send(string evt, byte[] data)
        {
            if (data == null) return;
            EODController.Lot.vm.SendCommand(new VMNetEODMessageCmd
            {
                PluginID = EODController.ActivePID,
                EventName = evt,
                Binary = true,
                BinData = data
            });
        }
    }
}
