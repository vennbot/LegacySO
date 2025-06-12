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
using FSO.Common.Utils;
using FSO.Files.Formats.tsodata;
using FSO.Server.DataService.Model;
using FSO.Server.Protocol.Electron.Packets;
using System;

namespace FSO.Client.Controllers
{
    public class MessagingWindowController
    {
        private UIMessageWindow View;
        private MessagingController Parent;
        private Message Message;
        private Network.Network Network;
        private IClientDataService DataService;

        public MessagingWindowController(UIMessageWindow view, Network.Network network, IClientDataService dataService)
        {
            this.View = view;
            this.Network = network;
            this.DataService = dataService;
        }

        public void Init(Message message, MessagingController parent){
            Message = message;
            Parent = parent;
            View.User.Value = message.User;
            View.SetType(message.Type);

        }

        public void SendIM(string body){
            var cref = Network.MyCharacterRef;
            var color = GlobalSettings.Default.ChatColor;
            if (GlobalSettings.Default.ChatOnlyEmoji > 0)
                body = GameFacade.Emojis.EmojiOnly(body, GlobalSettings.Default.ChatOnlyEmoji);
            View.AddMessage(cref, body, color, IMEntryType.MESSAGE_OUT);

            if (View.MyUser.Value == null)
            {
                View.MyUser.Value = cref;
                DataService.Request(MaskedStruct.Messaging_Message_Avatar, Network.MyCharacter).ContinueWith(x =>
                {
                    GameThread.NextUpdate(y =>
                    {
                        View.RenderMessages();
                    });
                });
            }

            if (Message.User.Type != Common.Enum.UserReferenceType.AVATAR){
                return;
            }

            Network.CityClient.Write(new InstantMessage {
                FromType = Common.Enum.UserReferenceType.AVATAR,
                From = Network.MyCharacter,
                Message = body,
                To = Message.User.Id,
                Type = InstantMessageType.MESSAGE,
                AckID = Guid.NewGuid().ToString(),
                Color = color
            });
        }

        public void SendLetter(MessageItem letter)
        {
            var cref = Network.MyCharacterRef;

            if (View.MyUser.Value == null)
            {
                View.MyUser.Value = cref;
                DataService.Request(MaskedStruct.Messaging_Message_Avatar, Network.MyCharacter).ContinueWith(x =>
                {
                    GameThread.NextUpdate(y =>
                    {
                        SendLetter(letter);
                    });
                });
                return;
            }

            letter.SenderID = cref.Id;
            if (letter.SenderName == null) letter.SenderName = cref.Name;
            letter.TargetID = Message.User.Id;

            Network.CityClient.Write(new MailRequest
            {
                Type = MailRequestType.SEND,
                Item = letter
            });

            Close();
        }

        public void Close(){
            Parent.CloseWindow(Message);
        }

        public void Hide()
        {
            Parent.ToggleWindow(Message);
        }
        public void UpdateOpacity()
        {
            View.Opacity = GlobalSettings.Default.ChatWindowsOpacity;
        }
    }
}
