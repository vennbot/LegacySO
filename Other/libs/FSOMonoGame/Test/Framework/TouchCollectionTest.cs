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
using Microsoft.Xna.Framework.Input.Touch;
using NUnit.Framework;

namespace MonoGame.Tests.Framework
{
    [TestFixture]
    class TouchCollectionTest
    {
        [Test]
        public void WorksWhenConstructedEmpty()
        {
            TouchCollection collection = new TouchCollection();

            Assert.AreEqual(0, collection.Count);
            foreach (var touch in collection)
                Assert.Fail("Shouldn't have any touches in an empty collection");

            Assert.AreEqual(-1, collection.IndexOf(new TouchLocation()));

            TouchLocation touchLocation;
            Assert.False(collection.FindById(1, out touchLocation));
        }
    }
}
