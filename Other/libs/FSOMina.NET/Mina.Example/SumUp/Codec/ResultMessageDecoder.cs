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
using Mina.Core.Buffer;
using Mina.Core.Session;
using Mina.Example.SumUp.Message;

namespace Mina.Example.SumUp.Codec
{
    class ResultMessageDecoder : AbstractMessageDecoder
    {
        private Int32 _code;
        private Boolean _readCode;

        public ResultMessageDecoder()
            : base(Constants.RESULT)
        { }

        protected override AbstractMessage DecodeBody(IoSession session, IoBuffer input)
        {
            if (!_readCode)
            {
                if (input.Remaining < Constants.RESULT_CODE_LEN)
                {
                    return null; // Need more data.
                }

                _code = input.GetInt16();
                _readCode = true;
            }

            if (_code == Constants.RESULT_OK)
            {
                if (input.Remaining < Constants.RESULT_VALUE_LEN)
                {
                    return null;
                }

                ResultMessage m = new ResultMessage();
                m.OK = true;
                m.Value = input.GetInt32();
                _readCode = false;
                return m;
            }
            else
            {
                ResultMessage m = new ResultMessage();
                m.OK = false;
                _readCode = false;
                return m;
            }
        }
    }
}
