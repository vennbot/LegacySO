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
using Mina.Core.Session;

namespace Mina.Handler.Chain
{
    [TestClass]
    public class ChainedIoHandlerTest
    {
        [TestMethod]
        public void TestChainedCommand()
        {
            IoHandlerChain chain = new IoHandlerChain();
            StringBuilder buf = new StringBuilder();
            chain.AddLast("A", new TestCommand(buf, 'A'));
            chain.AddLast("B", new TestCommand(buf, 'B'));
            chain.AddLast("C", new TestCommand(buf, 'C'));

            new ChainedIoHandler(chain).MessageReceived(new DummySession(), null);

            Assert.AreEqual("ABC", buf.ToString());
        }

        private class TestCommand : IoHandlerCommand
        {
            private readonly StringBuilder _sb;
            private readonly Char _ch;

            public TestCommand(StringBuilder sb, Char ch)
            {
                _sb = sb;
                _ch = ch;
            }

            public void Execute(INextCommand next, IoSession session, Object message)
            {
                _sb.Append(_ch);
                next.Execute(session, message);
            }
        }
    }
}
