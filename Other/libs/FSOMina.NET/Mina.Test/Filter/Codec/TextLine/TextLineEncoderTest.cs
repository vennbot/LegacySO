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
using System.Text;
#if !NETFX_CORE
using NUnit.Framework;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif
using Mina.Core.Buffer;

namespace Mina.Filter.Codec.TextLine
{
    [TestClass]
    public class TextLineEncoderTest
    {
        [TestMethod]
        public void TestEncode()
        {
            TextLineEncoder encoder = new TextLineEncoder(Encoding.UTF8, LineDelimiter.Windows);
            ProtocolCodecSession session = new ProtocolCodecSession();
            IProtocolEncoderOutput output = session.EncoderOutput;

            encoder.Encode(session, "ABC", output);
            Assert.AreEqual(1, session.EncoderOutputQueue.Count);
            IoBuffer buf = (IoBuffer)session.EncoderOutputQueue.Dequeue();
            Assert.AreEqual(5, buf.Remaining);
            Assert.AreEqual((Byte)'A', buf.Get());
            Assert.AreEqual((Byte)'B', buf.Get());
            Assert.AreEqual((Byte)'C', buf.Get());
            Assert.AreEqual((Byte)'\r', buf.Get());
            Assert.AreEqual((Byte)'\n', buf.Get());
        }
    }
}
