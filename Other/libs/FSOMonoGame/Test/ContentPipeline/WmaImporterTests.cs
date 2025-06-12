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
using Microsoft.Xna.Framework.Content.Pipeline;
using NUnit.Framework;

namespace MonoGame.Tests.ContentPipeline
{
    class WmaImporterTests
    {
        [Test]
        public void Arguments()
        {
            var context = new TestImporterContext("TestObj", "TestBin");
            Assert.Throws<ArgumentNullException>(() => new WmaImporter().Import(null, context));
            Assert.Throws<ArgumentNullException>(() => new WmaImporter().Import("", context));
            Assert.Throws<ArgumentNullException>(() => new WmaImporter().Import(@"Assets/Audio/rock_loop_stereo.wma", null));
            Assert.Throws<FileNotFoundException>(() => new WmaImporter().Import(@"this\does\not\exist.wma", context));
        }

        public void InvalidFormat()
        {
            Assert.Throws<InvalidContentException>(() => new WmaImporter().Import(@"Assets/Audio/rock_loop_stereo.wav", new TestImporterContext("TestObj", "TestBin")));
        }

        [TestCase(@"Assets/Audio/rock_loop_stereo.wma", 2, 176400, 44100, 16, 4)]
        [TestCase(@"Assets/Audio/rock_loop_stereo.mp3", 2, 176400, 44100, 16, 4)]
        public void Import(string sourceFile, int channels, int averageBytesPerSecond, int sampleRate, int bitsPerSample, int blockAlign)
        {
            var content = new WmaImporter().Import(sourceFile, new TestImporterContext("TestObj", "TestBin"));

            Assert.AreEqual(1, content.Format.Format);
            Assert.AreEqual(channels, content.Format.ChannelCount);
            Assert.AreEqual(averageBytesPerSecond, content.Format.AverageBytesPerSecond);
            Assert.AreEqual(sampleRate, content.Format.SampleRate);
            Assert.AreEqual(bitsPerSample, content.Format.BitsPerSample);
            Assert.AreEqual(blockAlign, content.Format.BlockAlign);

            content.Dispose();
        }
    }
}
