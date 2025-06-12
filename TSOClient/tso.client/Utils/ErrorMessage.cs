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

namespace FSO.Client.Utils
{
    public class ErrorMessage
    {
        public string Message = "Unknown Error";
        public string Title = "Error";
        public UIAlertButton[] Buttons = UIAlertButton.Ok();

        public static ErrorMessage FromLiteral(string message)
        {
            return new ErrorMessage { Message = message };
        }

        public static ErrorMessage FromLiteral(string title, string message)
        {
            return new ErrorMessage { Title = title, Message = message };
        }

        public static ErrorMessage FromUIText(string table, string msgKey)
        {
            return new ErrorMessage { Message = GameFacade.Strings.GetString(table, msgKey) };
        }

        public static ErrorMessage FromUIText(string table, string titleKey, string msgKey)
        {
            return new ErrorMessage
            {
                Title = GameFacade.Strings.GetString(table, titleKey),
                Message = GameFacade.Strings.GetString(table, msgKey)
            };
        }
    }
}
