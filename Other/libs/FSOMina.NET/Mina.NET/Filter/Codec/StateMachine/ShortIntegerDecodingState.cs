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

namespace Mina.Filter.Codec.StateMachine
{
    /// <summary>
    /// <see cref="IDecodingState"/> which decodes <code>short</code> values
    /// in big-endian order (high bytes come first).
    /// </summary>
    public abstract class ShortIntegerDecodingState : IDecodingState
    {
        private Int32 _highByte;
        private Int32 _counter;

        public IDecodingState Decode(IoBuffer input, IProtocolDecoderOutput output)
        {
            while (input.HasRemaining)
            {
                switch (_counter)
                {
                    case 0:
                        _highByte = input.Get() & 0xff;
                        break;
                    case 1:
                        _counter = 0;
                        return FinishDecode((Int16)((_highByte << 8) | (input.Get() & 0xff)), output);
                }

                _counter++;
            }
            return this;
        }

        public IDecodingState FinishDecode(IProtocolDecoderOutput output)
        {
            throw new ProtocolDecoderException("Unexpected end of session while waiting for a short integer.");
        }

        /// <summary>
        /// Invoked when this state has consumed a complete <code>short</code>.
        /// </summary>
        /// <param name="value">the short</param>
        /// <param name="output">the current <see cref="IProtocolDecoderOutput"/> used to write decoded messages.</param>
        /// <returns>
        /// the next state if a state transition was triggered (use 
        /// <code>this</code> for loop transitions) or <code>null</code> if 
        /// the state machine has reached its end.
        /// </returns>
        protected abstract IDecodingState FinishDecode(Int16 value, IProtocolDecoderOutput output);
    }
}
