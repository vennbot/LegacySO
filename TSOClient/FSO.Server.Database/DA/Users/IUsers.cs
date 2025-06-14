
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
using FSO.Server.Database.DA.Utils;
using System.Collections.Generic;

namespace FSO.Server.Database.DA.Users
{
    public interface IUsers
    {
        User GetById(uint id);
        List<User> GetByRegisterIP(string ip);
        void UpdateConnectIP(uint id, string ip);
        void UpdateBanned(uint id, bool banned);
        void UpdateClientID(uint id, string cid);
        User GetByUsername(string username);
        UserAuthenticate GetAuthenticationSettings(uint userId);
        PagedList<User> All(int offset = 0, int limit = 20, string orderBy = "register_date");
        uint Create(User user);
        void CreateAuth(UserAuthenticate auth);
        User GetByEmail(string email);
        void UpdateAuth(UserAuthenticate auth);
        void UpdateLastLogin(uint id, uint last_login);

        DbAuthAttempt GetRemainingAuth(uint user_id, string ip);
        int FailedConsecutive(uint user_id, string ip);
        int FailedAuth(uint attempt_id, uint delay, int failLimit);
        void NewFailedAuth(uint user_id, string ip, uint delay);
        void SuccessfulAuth(uint user_id, string ip);
    }
}
