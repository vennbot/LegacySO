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
using SharpDX.MediaFoundation;
using System;
using System.Runtime.InteropServices;

namespace Microsoft.Xna.Framework.Media
{
    internal class VideoSampleGrabber : SharpDX.CallbackBase, SampleGrabberSinkCallback
    {
        internal byte[] TextureData { get; private set; }

        public void OnProcessSample(Guid guidMajorMediaType, int dwSampleFlags, long llSampleTime, long llSampleDuration, IntPtr sampleBufferRef, int dwSampleSize)
        {
            if (TextureData == null || TextureData.Length != dwSampleSize)
                TextureData = new byte[dwSampleSize];

            Marshal.Copy(sampleBufferRef, TextureData, 0, dwSampleSize);
        }

        public void OnSetPresentationClock(PresentationClock presentationClockRef)
        {

        }

        public void OnShutdown()
        {

        }

        public void OnClockPause(long systemTime)
        {

        }

        public void OnClockRestart(long systemTime)
        {

        }

        public void OnClockSetRate(long systemTime, float flRate)
        {

        }

        public void OnClockStart(long systemTime, long llClockStartOffset)
        {

        }

        public void OnClockStop(long hnsSystemTime)
        {

        }
    }
}
