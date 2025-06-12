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
using TestInitialize = NUnit.Framework.SetUpAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif
using Mina.Core.File;

namespace Mina.Filter.Stream
{
    [TestClass]
    public class FileRegionWriteFilterTest : AbstractStreamWriteFilterTest<IFileRegion, FileRegionWriteFilter>
    {
        private String file;

        [TestInitialize]
        public void SetUp()
        {
            file = Path.GetTempFileName();
        }

        [TestCleanup]
        public void TearDown()
        {
            File.Delete(file);
        }

        protected override AbstractStreamWriteFilter<IFileRegion> CreateFilter()
        {
            return new FileRegionWriteFilter();
        }

        protected override IFileRegion CreateMessage(Byte[] data)
        {
            File.WriteAllBytes(file, data);
            return new FileInfoFileRegion(new FileInfo(file));
        }
    }
}
