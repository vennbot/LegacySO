
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
using FSO.Server.Framework.Voltron;
using FSO.Server.Protocol.Voltron.Packets;

namespace FSO.Server.Servers.City.Handlers
{
    public class SetPreferencesHandler
    {
        public SetPreferencesHandler()
        {
        }

        public void Handle(IVoltronSession session, SetIgnoreListPDU packet)
        {
            session.Write(new SetIgnoreListResponsePDU {
                StatusCode = 0,
                ReasonText = "OK",
                MaxNumberOfIgnored = 50
            });
        }

        public void Handle(IVoltronSession session, SetInvinciblePDU packet)
        {
        }
    }
}
