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
using Mina.Core.Buffer;
using Mina.Core.Session;
using Mina.Example.SumUp.Message;

namespace Mina.Example.SumUp.Codec
{
    class AddMessageDecoder : AbstractMessageDecoder
    {
        public AddMessageDecoder()
            : base(Constants.ADD)
        { }

        protected override Message.AbstractMessage DecodeBody(IoSession session, IoBuffer input)
        {
            if (input.Remaining < Constants.ADD_BODY_LEN)
            {
                return null;
            }

            AddMessage m = new AddMessage();
            m.Value = input.GetInt32();
            return m;
        }
    }
}
