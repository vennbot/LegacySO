
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
using FSO.Client.UI.Screens;
using FSO.Common.DatabaseService.Model;
using FSO.Common.Utils;
using FSO.Server.Protocol.CitySelector;
using System;

namespace FSO.Client.Controllers
{
    public class ConnectCityController : IDisposable
    {
        private TransitionScreen View;
        private CityConnectionRegulator CityConnectionRegulator;
        private Callback onConnect;
        private Callback onError;
        public LoadAvatarByIDResponse AvatarData;

        public ConnectCityController(TransitionScreen view,
                                     CityConnectionRegulator cityConnectionRegulator)
        {
            this.View = view;
            this.CityConnectionRegulator = cityConnectionRegulator;
            this.CityConnectionRegulator.OnTransition += CityConnectionRegulator_OnTransition;
            this.CityConnectionRegulator.OnError += CityConnectionRegulator_OnError;

            View.ShowProgress = true;
            View.SetProgress(0, 4);
        }

        public void Connect(string shardName, uint avatarId, Callback onConnect, Callback onError)
        {
            this.onConnect = onConnect;
            this.onError = onError;

            CityConnectionRegulator.Connect(CityConnectionMode.NORMAL, new ShardSelectorServletRequest
            {
                AvatarID = avatarId.ToString(),
                ShardName = shardName
            });
        }

        private void CityConnectionRegulator_OnError(object data)
        {
            onError();
        }

        private void CityConnectionRegulator_OnTransition(string state, object data)
        {
            GameThread.NextUpdate((x) =>
            {
                switch (state)
                {
                    case "Disconnected":
                        break;
                    case "SelectCity":
                        //4  ^Starting engines^                 # City is Selected...
                        View.SetProgress((1.0f / 14.0f) * 100, 4);
                        break;
                    case "ConnectToCitySelector":
                        //5  ^Talking with the mothership^      # Connecting to City Selector...
                        View.SetProgress((2.0f / 14.0f) * 100, 5);
                        break;
                    case "CitySelected":
                        //6  ^1^ # Processing XML response...
                        View.SetProgress((3.0f / 14.0f) * 100, 6);
                        break;
                    case "OpenSocket":
                        //7	  ^Sterilizing TCP/IP sockets^       # Connecting to City...
                        View.SetProgress((4.0f / 14.0f) * 100, 7);
                        break;
                    case "PartiallyConnected":
                        //7	  ^Sterilizing TCP/IP sockets^       # Connecting to City...
                        View.SetProgress((5.0f / 14.0f) * 100, 8);
                        break;
                    case "AskForAvatarData":
                        //9  ^Reticulating spleens^             # Asking for Avatar data from DB...
                        View.SetProgress((6.0f / 14.0f) * 100, 9);
                        break;
                    case "ReceivedAvatarData":
                        //10 ^Spleens Reticulated^              # Received Avatar data from DB...

                        var dbResponse = (LoadAvatarByIDResponse)data;
                        if (dbResponse != null)
                        {
                            AvatarData = dbResponse;
                        }

                        View.SetProgress((7.0f / 14.0f) * 100, 10);
                        break;
                    case "AskForCharacterData":
                        //11 ^Purging psychographic metrics^    # Asking for Character data from DB...
                        View.SetProgress((8.0f / 14.0f) * 100, 11);
                        break;

                    case "ReceivedCharacterData":
                        //12 ^Metrics Purged^                   # Received Character data from DB...
                        View.SetProgress((9.0f / 14.0f) * 100, 12);
                        break;
                    case "Connected":
                        onConnect();
                        break;
                }
            });
        }

        public void Dispose()
        {
            CityConnectionRegulator.OnTransition -= CityConnectionRegulator_OnTransition;
            CityConnectionRegulator.OnError -= CityConnectionRegulator_OnError;
        }
    }
}
