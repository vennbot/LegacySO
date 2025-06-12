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
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Xna.Framework.Input
{
    public static partial class MessageBox
    {
        public static bool IsVisible { get; private set; }

        /// <summary>
        /// Displays the message box interface asynchronously.
        /// </summary>
        /// <param name="title">Title of the message box.</param>
        /// <param name="description">Description of the message box.</param>
        /// <param name="buttons">Captions of the message box buttons. Up to three supported.</param>
        /// <returns>Index of button selected by the player. Null if back was used.</returns>
        /// <exception cref="System.Exception">Thrown when the message box is already visible</exception>
        /// <example>
        /// <code>
        /// var color = await MessageBox.Show("Color", "What's your favorite color?", new[] { "Red", "Green", "Blue" });
        /// </code>
        /// </example>
        public static async Task<int?> Show(string title, string description, IEnumerable<string> buttons)
        {
            if (IsVisible)
                throw new Exception("The function cannot be completed at this time: the MessageBox UI is already active. Wait until MessageBox.IsVisible is false before issuing this call.");

            IsVisible = true;

            var buttonsList = buttons.ToList();
            if (buttonsList.Count > 3 || buttonsList.Count == 0)
                throw new ArgumentException("Invalid number of buttons: one to three required", "buttons");

            var result = await PlatformShow(title, description, buttonsList);

            IsVisible = false;

            return result;
        }

        /// <summary>
        /// Hides the message box interface and returns the parameter as the result of <see cref="Show"/>
        /// </summary>
        /// <param name="result">Result to return</param>
        /// <exception cref="System.Exception">Thrown when the message box is not visible</exception>
        /// <example>
        /// <code>
        /// var colorTask = MessageBox.Show("Color", "What's your favorite color?", new[] { "Red", "Green", "Blue" });
        /// MessageBox.Cancel(0);
        /// var color = await colorTask;
        /// </code>
        /// </example>
        public static void Cancel(int? result)
        {
            if (!IsVisible)
                throw new Exception("The function cannot be completed at this time: the MessageBox UI is not active.");

            PlatformCancel(result);
        }
    }
}
