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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using GonzoNet;
using PDChat.Sims;

namespace PDChat
{
    public partial class ChatFrm : Form
    {
        Sim m_CurrentSim;

        public ChatFrm(Sim Avatar)
        {
            InitializeComponent();

            m_CurrentSim = Avatar;
            PictureBox1.Image = Avatar.Thumbnail;
            LblName.Text = Avatar.Name;

            NetworkController.OnPlayerJoinedSession += new OnPlayerJoinedSessionDelegate(NetworkController_OnPlayerJoinedSession);
            NetworkController.OnReceivedMessage += new OnReceivedMessageDelegate(NetworkController_OnReceivedMessage);
        }

        /// <summary>
        /// A new player joined the session.
        /// </summary>
        /// <param name="Avatar">Player's avatar.</param>
        private void NetworkController_OnPlayerJoinedSession(Sim Avatar)
        {
            LstParticipants.Invoke(new MethodInvoker(delegate
                {
                    LstParticipants.Items.Add(Avatar.Name);
                }));
        }

        /// <summary>
        /// New message was received from a player.
        /// </summary>
        /// <param name="Msg">The message received.</param>
        private void NetworkController_OnReceivedMessage(string Msg)
        {
            TxtChat.Invoke(new MethodInvoker(delegate
            {
                TxtChat.Text += "\r\n" + Msg;
            }));
        }

        private void BtnSend_Click(object sender, EventArgs e)
        {
            if (TxtMessage.Text != string.Empty)
            {
                //Sending an empty string isn't a good idea.
                PacketSenders.BroadcastLetter(NetworkFacade.Client, TxtMessage.Text, "");

                TxtChat.Invoke(new MethodInvoker(delegate
                    {
                        TxtChat.Text += "\r\n" + TxtMessage.Text;
                        TxtMessage.Text = string.Empty;
                    }));
            }
        }
    }
}
