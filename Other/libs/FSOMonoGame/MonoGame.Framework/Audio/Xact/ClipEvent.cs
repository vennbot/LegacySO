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

namespace Microsoft.Xna.Framework.Audio
{
	abstract class ClipEvent
    {
        protected XactClip _clip;

	    protected ClipEvent(XactClip clip, float timeStamp, float randomOffset)
        {
            _clip = clip;
            TimeStamp = timeStamp;
            RandomOffset = randomOffset;
        }

	    public float RandomOffset { get; private set; }

	    public float TimeStamp { get; private set; }

	    public abstract void Play();
	    public abstract void Stop();
		public abstract void Pause();
        public abstract void Resume();
        public abstract void SetFade(float fadeInDuration, float fadeOutDuration);
        public abstract void SetTrackVolume(float volume);
        public abstract void SetTrackPan(float pan);
        public abstract void SetState(float volume, float pitch, float reverbMix, float? filterFrequency, float? filterQFactor);
	    public abstract bool Update(float dt);
    }
}

