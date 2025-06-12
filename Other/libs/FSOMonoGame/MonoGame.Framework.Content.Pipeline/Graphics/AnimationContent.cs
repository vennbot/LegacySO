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

namespace Microsoft.Xna.Framework.Content.Pipeline.Graphics
{
    /// <summary>
    /// Provides properties for maintaining an animation.
    /// </summary>
    public class AnimationContent : ContentItem
    {
        AnimationChannelDictionary channels;
        TimeSpan duration;

        /// <summary>
        /// Gets the collection of animation data channels. Each channel describes the movement of a single bone or rigid object.
        /// </summary>
        public AnimationChannelDictionary Channels
        {
            get
            {
                return channels;
            }
        }

        /// <summary>
        /// Gets or sets the total length of the animation.
        /// </summary>
        public TimeSpan Duration
        {
            get
            {
                return duration;
            }
            set
            {
                duration = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of AnimationContent.
        /// </summary>
        public AnimationContent()
        {
            channels = new AnimationChannelDictionary();
        }
    }
}
