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
using Microsoft.Xna.Framework.Audio;
using NUnit.Framework;

namespace MonoGame.Tests.Audio
{
    // Tests specific to SoundEffectInstance.XAudio (Windows DirectX)
    public class SoundEffectInstanceXAudioTest
    {
        // Mono source
        [TestCase( 0f, 1f, 1, 1f, 1f)]
        [TestCase(-1f, 1f, 1, 1f, 0f)]
        [TestCase( 1f, 1f, 1, 0f, 1f)]
        [TestCase(-0.75f, 1f, 1, 1f, 0.25f)]
        [TestCase( 0.75f, 1f, 1, 0.25f, 1f)]
        [TestCase( 0f, 0.75f, 1, 0.75f, 0.75f)]
        // Mono source, scaled
        [TestCase(0f, 0.75f, 1, 0.75f, 0.75f)]
        [TestCase(-1f, 0.75f, 1, 0.75f, 0f)]
        [TestCase( 1f, 0.75f, 1, 0f, 0.75f)]
        // Stereo source
        [TestCase(0f, 1f, 2, 1f, 0f, 0f, 1f)]
        [TestCase(-1f, 1f, 2, 0.5f, 0.5f, 0f, 0f)]
        [TestCase(1f, 1f, 2, 0f, 0f, 0.5f, 0.5f)]
        [TestCase(-0.5f, 1f, 2, 0.75f, 0.25f, 0f, 0.5f)]
        [TestCase(0.5f, 1f, 2, 0.5f, 0f, 0.25f, 0.75f)]
        [TestCase(-0.75f, 1f, 2, 0.625f, 0.375f, 0f, 0.25f)]
        [TestCase(0.75f, 1f, 2, 0.25f, 0f, 0.375f, 0.625f)]
        // Stereo source, scaled
        [TestCase(0f, 0.75f, 2, 0.75f, 0f, 0f, 0.75f)]
        [TestCase(-1f, 0.75f, 2, 0.375f, 0.375f, 0f, 0f)]
        [TestCase(1f, 0.75f, 2, 0f, 0f, 0.375f, 0.375f)]
        public void CalculateOutputMatrixReturnsExpectedResults(
            float pan, float scale, int inputChannels,
            params float[] expectedValues)
        {
            var outputMatrix = SoundEffectInstance.CalculateOutputMatrix(pan, scale, inputChannels);

            for (int i = 0; i < expectedValues.Length; i++)
                Assert.AreEqual(expectedValues[i], outputMatrix[i], "Channel#" + i);

            // the remaining values should be 0
            for (int i = expectedValues.Length; i < outputMatrix.Length; i++)
                Assert.AreEqual(0f, outputMatrix[i], "Channel#" + i);
        }
    }
}
