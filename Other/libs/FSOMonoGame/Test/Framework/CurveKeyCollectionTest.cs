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
using System.ComponentModel;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using NUnit.Framework;

namespace MonoGame.Tests.Framework
{
    class CurveKeyCollectionTest
    {
        //[Test]
        //public void TypeConverter()
        //{
        //    var curveKeyCollection = new CurveKeyCollection
        //    {
        //        new CurveKey(0, 1),
        //        new CurveKey(1, 2),
        //        new CurveKey(3, 4)
        //    };

        //    // Gets the attributes for the instance.

        //    var attributes = TypeDescriptor.GetAttributes(curveKeyCollection);

        //    // Assert.AreEqual(5, attributes.Count);

        //    for (var i = 0; i < attributes.Count; ++i)
        //    {
        //        Debug.WriteLine("attribute #" + i + " = " + attributes[i]);
        //        Assert.AreEqual(false, attributes[i].IsDefaultAttribute());
        //    }
        //}

        [Test]
        public void Properties()
        {
            var curveKeyCollection = new CurveKeyCollection
            {
                new CurveKey(0, 0),
                new CurveKey(-1, 1),
                new CurveKey(-1, 1)
            };

            // Count property

            Assert.AreEqual(3, curveKeyCollection.Count);

            // IsReadOnly property

            Assert.AreEqual(false, curveKeyCollection.IsReadOnly);

            // Item indexer

            var key1 = new CurveKey(-1, 1);
            var key2 = curveKeyCollection[1];
            var key3 = curveKeyCollection[2];

            Assert.AreEqual(true, key1 == key2);
            Assert.AreEqual(false, key2 == key3);
            Assert.AreEqual(key1, key2);
            Assert.AreNotEqual(key2, key3);
            Assert.AreNotEqual(key1, key3);
        }
    }
}
