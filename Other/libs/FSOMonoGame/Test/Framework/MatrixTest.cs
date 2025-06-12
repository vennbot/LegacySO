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
using Microsoft.Xna.Framework;
using NUnit.Framework;

namespace MonoGame.Tests.Framework
{
    class MatrixTest
    {
        [Test]
        public void Add()
        {
            Matrix test = new Matrix(1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16);
            Matrix expected = new Matrix(2,4,6,8,10,12,14,16,18,20,22,24,26,28,30,32);
            Matrix result;
            Matrix.Add(ref test,ref test, out result);
            Assert.AreEqual(expected, result);
            Assert.AreEqual(expected, Matrix.Add(test,test));
            Assert.AreEqual(expected, test+test);
        }
    }
}
