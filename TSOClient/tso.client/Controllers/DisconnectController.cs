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
using FSO.Client.UI.Screens;
using System;

namespace FSO.Client.Controllers
{
    public class DisconnectController : IDisposable
    {
        private TransitionScreen View;
        private CityConnectionRegulator CityConnectionRegulator;
        private LotConnectionRegulator LotConnectionRegulator;
        private LoginRegulator LoginRegulator;

        int totalComplete = 0;
        int targetComplete = 2;
        private Action<bool> onDisconnected;

        public DisconnectController(TransitionScreen view, CityConnectionRegulator cityRegulator, LotConnectionRegulator lotRegulator, LoginRegulator logRegulator, Network.Network network)
        {
            View = view;
            View.ShowProgress = false;

            CityConnectionRegulator = cityRegulator;
            CityConnectionRegulator.OnTransition += CityConnectionRegulator_OnTransition;
            LotConnectionRegulator = lotRegulator;
            LoginRegulator = logRegulator;
            LoginRegulator.OnError += LoginRegulator_OnError;
            LoginRegulator.OnTransition += LoginRegulator_OnTransition;
        }

        private void LoginRegulator_OnTransition(string state, object data)
        {
            switch (state)
            {
                case "LoggedIn":
                    if (++totalComplete == targetComplete) onDisconnected(false);
                    break;
            }
        }

        private void LoginRegulator_OnError(object data)
        {
            onDisconnected(true);
        }

        private void CityConnectionRegulator_OnTransition(string state, object data)
        {
            switch (state)
            {
                case "Disconnect":
                    break;
                case "Disconnected":
                    if (++totalComplete == targetComplete) onDisconnected(false);
                    break;
            }
        }

        public void Disconnect(Action<bool> onDisconnected, bool forceLogin)
        {
            totalComplete = 0;
            this.onDisconnected = onDisconnected;

            if (forceLogin)
            {
                targetComplete = 1;
                LoginRegulator.Logout();
            }

            CityConnectionRegulator.Disconnect();
            LotConnectionRegulator.Disconnect();

            if (!forceLogin) LoginRegulator.AsyncTransition("AvatarData");
        }

        public void Dispose()
        {
            CityConnectionRegulator.OnTransition -= CityConnectionRegulator_OnTransition;
            LoginRegulator.OnTransition -= LoginRegulator_OnTransition;
            LoginRegulator.OnError -= LoginRegulator_OnError;
        }
    }
}
