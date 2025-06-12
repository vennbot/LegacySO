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
using System.IO;
#if !NETFX_CORE
using NUnit.Framework;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif
using Mina.Core.Buffer;
using Mina.Core.Service;
using Mina.Core.Session;

namespace Mina.Filter.Codec.Serialization
{
    [TestClass]
    public class ObjectSerializationTest
    {
        [TestMethod]
        public void TestEncoder()
        {
            String expected = "1234";

            ProtocolCodecSession session = new ProtocolCodecSession();
            IProtocolEncoderOutput output = session.EncoderOutput;

            IProtocolEncoder encoder = new ObjectSerializationEncoder();
            encoder.Encode(session, expected, output);

            Assert.AreEqual(1, session.EncoderOutputQueue.Count);
            IoBuffer buf = (IoBuffer)session.EncoderOutputQueue.Dequeue();

            TestDecoderAndInputStream(expected, buf);
        }

        private void TestDecoderAndInputStream(String expected, IoBuffer input)
        {
            // Test ProtocolDecoder
            IProtocolDecoder decoder = new ObjectSerializationDecoder();
            ProtocolCodecSession session = new ProtocolCodecSession();
            IProtocolDecoderOutput decoderOut = session.DecoderOutput;
            decoder.Decode(session, input.Duplicate(), decoderOut);

            Assert.AreEqual(1, session.DecoderOutputQueue.Count);
            Assert.AreEqual(expected, session.DecoderOutputQueue.Dequeue());
        }
    }
}
