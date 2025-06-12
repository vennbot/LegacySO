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
using System.IO;

namespace MonoGame.Tests.Framework
{
    class UtilitiesTest
    {
        [TestCase(  @"C:\Game\Content\file",            @"file.extension",          @"C:\Game\Content\file.extension")]
        [TestCase(  @"C:\Game\Content\file",            @"..\file.extension",       @"C:\Game\file.extension")]
        [TestCase(  @"C:\Game\Content\..\file",         @"file.extension",          @"C:\Game\file.extension")]
        [TestCase(  @"C:\Game\Content\..\file",         @"..\file.extension",       @"C:\file.extension")]
        [TestCase(  @"C:\Game\Content\.\file",          @"file.extension",          @"C:\Game\Content\file.extension")]
        [TestCase(  @"C:\Game\Content\.\file",          @".\file.extension",        @"C:\Game\Content\file.extension")]
        [TestCase(  @"C:/Game/Content/file",            @"file.extension",          @"C:/Game/Content/file.extension")]
        [TestCase(  @"C:/Game/Content/file",            @"../file.extension",       @"C:/Game/file.extension")]
        [TestCase(  @"C:/Game/Content/../file",         @"file.extension",          @"C:/Game/file.extension")]
        [TestCase(  @"C:/Game/Content/../file",         @"../file.extension",       @"C:/file.extension")]
        [TestCase(  @"C:/Game/Content/./file",          @"file.extension",          @"C:/Game/Content/file.extension")]
        [TestCase(  @"C:/Game/Content/./file",          @"./file.extension",        @"C:/Game/Content/file.extension")]
        [TestCase(  @"\application0\Content\file",      @"file.extension",          @"\application0\Content\file.extension")]
        [TestCase(  @"\application0\Content\file",      @"..\file.extension",       @"\application0\file.extension")]
        [TestCase(  @"\application0\Content\..\file",   @"file.extension",          @"\application0\file.extension")]
        [TestCase(  @"\application0\Content\..\file",   @"..\file.extension",       @"\file.extension")]
        [TestCase(  @"\application0\Content\.\file",    @"file.extension",          @"\application0\Content\file.extension")]
        [TestCase(  @"\application0\Content\.\file",    @".\file.extension",        @"\application0\Content\file.extension")]
        [TestCase(  @"/application0/Content/file",      @"file.extension",          @"/application0/Content/file.extension")]
        [TestCase(  @"/application0/Content/file",      @"../file.extension",       @"/application0/file.extension")]
        [TestCase(  @"/application0/Content/../file",   @"file.extension",          @"/application0/file.extension")]
        [TestCase(  @"/application0/Content/../file",   @"../file.extension",       @"/file.extension")]
        [TestCase(  @"/application0/Content/./file",    @"file.extension",          @"/application0/Content/file.extension")]
        [TestCase(  @"/application0/Content/./file",    @"./file.extension",        @"/application0/Content/file.extension")]
        [TestCase(  @"Content\file",                    @"file.extension",          @"Content\file.extension")]
        [TestCase(  @"Content\file",                    @"..\file.extension",       @"file.extension")]
        [TestCase(  @"Content\..\file",                 @"file.extension",          @"file.extension")]
        [TestCase(  @"Content\..\file",                 @"..\file.extension",       @"file.extension")]
        [TestCase(  @"Content\.\file",                  @"file.extension",          @"Content\file.extension")]
        [TestCase(  @"Content\.\file",                  @".\file.extension",        @"Content\file.extension")]
        [TestCase(  @"Content/file",                    @"file.extension",          @"Content/file.extension")]
        [TestCase(  @"Content/file",                    @"../file.extension",       @"file.extension")]
        [TestCase(  @"Content/../file",                 @"file.extension",          @"file.extension")]
        [TestCase(  @"Content/../file",                 @"../file.extension",       @"file.extension")]
        [TestCase(  @"Content/./file",                  @"file.extension",          @"Content/file.extension")]
        [TestCase(  @"Content/./file",                  @"./file.extension",        @"Content/file.extension")]
        [TestCase(  @"C:\Game\Content\fi#le",           @"fi#le.extension",         @"C:\Game\Content\fi#le.extension")]
        public void ResolveRelativePath(
                    string rootFilePath,                string relativePath,        string matchFullPath)
        {
            var fullPath = FileHelpers.ResolveRelativePath(rootFilePath, relativePath);
            Assert.NotNull(fullPath);

            // Make sure the matching path has the right seperators as well.
            matchFullPath = FileHelpers.NormalizeFilePathSeparators(matchFullPath);
           
            Assert.AreEqual(matchFullPath, fullPath);
            Assert.AreEqual(-1, fullPath.IndexOf(FileHelpers.NotSeparator));
        }
    }
}
