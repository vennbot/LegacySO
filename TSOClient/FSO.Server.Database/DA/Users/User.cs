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
namespace FSO.Server.Database.DA.Users
{
    public class User
    {
        public uint user_id { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public UserState user_state { get; set; }
        public uint register_date { get; set; }
        public bool is_admin { get; set; }
        public bool is_moderator { get; set; }
        public bool is_banned { get; set; }
        public string register_ip { get; set; }
        public string last_ip { get; set; }
        public string client_id { get; set; }
        public uint last_login { get; set; }
    }

    public enum UserState
    {
        valid,
        email_confirm,
        moderated
    }
}
