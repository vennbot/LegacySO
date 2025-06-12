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

namespace Microsoft.Xna.Framework.Audio
{
    public sealed partial class DynamicSoundEffectInstance : SoundEffectInstance
    {
        private void PlatformCreate()
        {
        }

        private int PlatformGetPendingBufferCount()
        {
            return 0;
        }

        private void PlatformPlay()
        {
        }

        private void PlatformPause()
        {
        }

        private void PlatformResume()
        {
        }

        private void PlatformStop()
        {
        }

        private void PlatformSubmitBuffer(byte[] buffer, int offset, int count)
        {
        }

        private void PlatformDispose(bool disposing)
        {
        }

        private void PlatformUpdateQueue()
        {
        }
    }
}
