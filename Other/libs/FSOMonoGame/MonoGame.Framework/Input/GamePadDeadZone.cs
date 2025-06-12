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

namespace Microsoft.Xna.Framework.Input
{
    /// <summary>
    /// Specifies a type of dead zone processing to apply to Xbox 360 Controller
    /// analog sticks when calling GetState.
    /// </summary>
    public enum GamePadDeadZone
    {
        /// <summary>
        /// The values of each stick are not processed and are returned by GetState as
        /// "raw" values. This is best if you intend to implement your own dead zone
        /// processing.
        /// </summary>
        None,

        /// <summary>
        /// The X and Y positions of each stick are compared against the dead zone independently.
        /// This setting is the default when calling GetState.
        /// </summary>
        IndependentAxes,

        /// <summary>
        /// The combined X and Y position of each stick is compared to the dead zone.
        /// This provides better control than IndependentAxes when the stick is used
        /// as a two-dimensional control surface, such as when controlling a character's
        /// view in a first-person game.
        /// </summary>
        Circular
    }
}
