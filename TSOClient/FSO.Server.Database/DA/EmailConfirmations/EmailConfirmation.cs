
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
namespace FSO.Server.Database.DA.EmailConfirmation
{
    /// <summary>
    /// EmailConfirmation model
    /// </summary>
    public class EmailConfirmation
    {
        /// <summary>
        /// Confirmation type. Can be an email confirmation or password
        /// reset confirmation.
        /// </summary>
        public ConfirmationType type { get; set; }
        /// <summary>
        /// The user email address.
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// Randomized token.
        /// </summary>
        public string token { get; set; }
        /// <summary>
        /// Timestamp when the confirmation token will expire.
        /// </summary>
        public uint expires { get; set; }
    }

    public enum ConfirmationType
    {
        email = 1,
        password
    }
}
