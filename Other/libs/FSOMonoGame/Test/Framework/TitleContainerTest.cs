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
// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using System.IO;
using Microsoft.Xna.Framework;
using NUnit.Framework;

namespace MonoGame.Tests.Framework
{
    class TitleContainerTest
    {
        [Test]
        public void OpenStream()
        {
            Assert.Throws<ArgumentNullException>(() => TitleContainer.OpenStream(null));
            Assert.Throws<ArgumentNullException>(() => TitleContainer.OpenStream(string.Empty));
            Assert.Throws<ArgumentNullException>(() => TitleContainer.OpenStream(""));
            Assert.Throws<FileNotFoundException>(() => TitleContainer.OpenStream(" "));
            Assert.Throws<FileNotFoundException>(() => TitleContainer.OpenStream("notfound"));
            // under mono we get a FileNotFoundException for this path.
            Assert.Throws(Is.TypeOf<ArgumentException>().Or.TypeOf<FileNotFoundException>(), () => TitleContainer.OpenStream(@"C:\\"));

            // TODO: This always fails on XNA... even though it shouldn't.  I suspect 
            // this is because internally XNA uses the entry/active assembly as the
            // root of the path.  Since we are launched from some Nunit runner things
            // end up not working as we expect.
            //
            // We need to figure out the trick to hack around this to validate 
            // non-failure tests against XNA.
#if !XNA
            var stream = TitleContainer.OpenStream(@"Assets\Xml\01_TheBasics.xml");
            Assert.AreEqual(0, stream.Position);
            Assert.AreEqual(true, stream.CanRead);
            Assert.AreEqual(true, stream.CanSeek);
            Assert.AreEqual(false, stream.CanWrite);
            stream.Dispose();
#endif
        }
    }
}
