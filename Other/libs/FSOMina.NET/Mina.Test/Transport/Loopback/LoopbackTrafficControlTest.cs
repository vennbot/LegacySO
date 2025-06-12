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
#if !NETFX_CORE
using NUnit.Framework;
using TestClass = NUnit.Framework.TestFixtureAttribute;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif
using Mina.Core.Future;
using Mina.Core.Service;

namespace Mina.Transport.Loopback
{
    [TestClass]
    public class LoopbackTrafficControlTest : AbstractTrafficControlTest
    {
        public LoopbackTrafficControlTest()
            : base(new LoopbackAcceptor())
        { }

        protected override System.Net.EndPoint CreateServerEndPoint(Int32 port)
        {
            return new LoopbackEndPoint(port);
        }

        protected override Int32 GetPort(System.Net.EndPoint ep)
        {
            return ((LoopbackEndPoint)ep).Port;
        }

        protected override IConnectFuture Connect(Int32 port, IoHandler handler)
        {
            IoConnector connector = new LoopbackConnector();
            connector.Handler = handler;
            return connector.Connect(new LoopbackEndPoint(port));
        }
    }
}
