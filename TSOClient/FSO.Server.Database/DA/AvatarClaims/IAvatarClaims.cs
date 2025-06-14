
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
using System.Collections.Generic;

namespace FSO.Server.Database.DA.AvatarClaims
{
    public interface IAvatarClaims
    {
        DbAvatarClaim Get(int id);
        IEnumerable<DbAvatarClaim> GetAll();
        IEnumerable<DbAvatarActive> GetAllActiveAvatars();
        int? GetAllActiveAvatarsCount();
        DbAvatarClaim GetByAvatarID(uint id);
        IEnumerable<DbAvatarClaim> GetAllByOwner(string owner);

        int? TryCreate(DbAvatarClaim claim);
        bool Claim(int id, string previousOwner, string newOwner, uint location);
        void RemoveRemaining(string previousOwner, uint location);

        void Delete(int id, string owner);
        void DeleteAll(string owner);
    }
}
