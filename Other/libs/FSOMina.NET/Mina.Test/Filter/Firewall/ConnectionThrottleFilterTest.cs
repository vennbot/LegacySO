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
using System.Net;
using System.Threading;
#if !NETFX_CORE
using NUnit.Framework;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif
using Mina.Core.Session;

namespace Mina.Filter.Firewall
{
    [TestClass]
    public class ConnectionThrottleFilterTest
    {
        private ConnectionThrottleFilter filter = new ConnectionThrottleFilter();
        private DummySession sessionOne = new DummySession();
        private DummySession sessionTwo = new DummySession();

        public ConnectionThrottleFilterTest()
        {
            sessionOne.SetRemoteEndPoint(new IPEndPoint(IPAddress.Any, 1234));
            sessionTwo.SetRemoteEndPoint(new IPEndPoint(IPAddress.Any, 1235));
        }

        [TestMethod]
        public void TestGoodConnection()
        {
            filter.AllowedInterval = 100;
            filter.IsConnectionOk(sessionOne);

            Thread.Sleep(1000);

            Assert.IsTrue(filter.IsConnectionOk(sessionOne));
        }

        [TestMethod]
        public void TestBadConnection()
        {
            filter.AllowedInterval = 1000;
            filter.IsConnectionOk(sessionTwo);
            Assert.IsFalse(filter.IsConnectionOk(sessionTwo));
        }
    }
}
