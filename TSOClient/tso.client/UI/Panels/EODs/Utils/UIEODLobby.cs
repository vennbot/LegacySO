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
using FSO.SimAntics;
using FSO.SimAntics.NetPlay.EODs.Utils;
using System;
using System.Collections.Generic;

namespace FSO.Client.UI.Panels.EODs.Utils
{
    public class UIEODLobby : UIContainer
    {
        private int NumPlayers;
        private UIEOD EOD;

        private List<UIEODLobbyPlayer> PlayerUI;
        private Func<UIEODLobbyPlayer, VMAvatar, string> CaptionFunction;

        public UIEODLobby(UIEOD eod, int numPlayers)
        {
            this.EOD = eod;
            this.NumPlayers = numPlayers;
            this.PlayerUI = new List<UIEODLobbyPlayer>();
        }

        public UIEODLobby WithCaptionProvider(Func<UIEODLobbyPlayer, VMAvatar, string> func)
        {
            CaptionFunction = func;
            return this;
        }

        public UIEODLobby WithPlayerUI(UIEODLobbyPlayer player)
        {
            PlayerUI.Add(player);
            return this;
        }

        public UIVMPersonButton GetPlayerButton(int playerIndex)
        {
            if (playerIndex > -1 && playerIndex < PlayerUI.Count)
                return PlayerUI[playerIndex].PersonButton;
            return null;
        }

        public UIVMPersonButton GetAvatarButton(short objectID, bool small)
        {
            var avatar = (VMAvatar)EOD.EODController.Lot.vm.GetObjectById((short)objectID);
            if (avatar != null)
                return new UIVMPersonButton((VMAvatar)avatar, EOD.EODController.Lot.vm, small);
            return null;
        }

        public void UpdatePlayers(string evt, string msg)
        {
            var players = EODLobby.ParsePlayers(msg);
            if (players.Length != NumPlayers) { return; }

            for(var i=0; i < PlayerUI.Count; i++)
            {
                var ui = PlayerUI[i];
                var playerId = players[ui.Slot];

                var avatar = (VMAvatar)EOD.EODController.Lot.vm.GetObjectById((short)playerId);

                if (avatar == null)
                {
                    //No player
                    RemovePlayerUI(ui);
                }
                else
                {
                    UpdatePlayerUI(ui, avatar);
                }
            }
        }

        private void UpdatePlayerUI(UIEODLobbyPlayer player, VMAvatar avatar)
        {
            if (player.PersonButton != null)
            {
                Remove(player.PersonButton);
                player.PersonButton = null;
            }

            player.PersonButton = new UIVMPersonButton((VMAvatar)avatar, EOD.EODController.Lot.vm, true);
            player.PersonButton.Position = player.Image.Position + new Microsoft.Xna.Framework.Vector2(2, 2);
            Add(player.PersonButton);

            if (CaptionFunction != null)
            {
                player.Label.Caption = CaptionFunction(player, avatar);
            }
        }

        private void RemovePlayerUI(UIEODLobbyPlayer player)
        {
            if(player.PersonButton != null)
            {
                Remove(player.PersonButton);
                player.PersonButton = null;
            }

            if (CaptionFunction != null)
            {
                player.Label.Caption = CaptionFunction(player, null);
            }
        }
    }

    public class UIEODLobbyPlayer
    {
        public int Slot { get; internal set; }

        public UIImage Image { get; internal set; }
        public UILabel Label { get; internal set; }
        public UIVMPersonButton PersonButton;

        public UIEODLobbyPlayer(int slot, UIImage background, UILabel label)
        {
            this.Slot = slot;
            this.Image = background;
            this.Label = label;
        }
    }
}
