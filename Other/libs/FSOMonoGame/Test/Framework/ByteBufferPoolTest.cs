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
using Microsoft.Xna.Framework.Utilities;
using NUnit.Framework;

namespace MonoGame.Tests.Framework
{
    public class ByteBufferPoolTest
    {
        [Test]
        public void CorrectPoolingTest()
        {
            var pool = new ByteBufferPool();

            var buf = pool.Get(5);
            pool.Return(buf);
            Assert.AreEqual(1, pool.FreeAmount);

            var buf2 = pool.Get(4);
            pool.Return(buf2);
            Assert.AreEqual(1, pool.FreeAmount);

            var buf3 = pool.Get(6);
            pool.Return(buf3);
            Assert.AreEqual(2, pool.FreeAmount);
        }
    }
}
