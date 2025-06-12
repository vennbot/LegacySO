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
using FSO.Common.Utils;
using FSO.Server.Framework.Voltron;
using System.Collections.Generic;

namespace FSO.Server.Framework.Aries
{
    public interface ISessions
    {
        T UpgradeSession<T>(IAriesSession session, Callback<T> init) where T : AriesSession;

        ISessionGroup GetOrCreateGroup(object id);
        IVoltronSession GetByAvatarId(uint id);
        ISessionProxy All();
        HashSet<IAriesSession> Clone();

        void Broadcast(params object[] messages);
    }

    public interface ISessionProxy {
        void Broadcast(params object[] messages);
    }

    public interface ISessionGroup : ISessionProxy
    {
        void Enroll(IAriesSession session);
        void UnEnroll(IAriesSession session);
    }
}
