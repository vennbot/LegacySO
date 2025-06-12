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

namespace Microsoft.Xna.Framework.Content.Pipeline.Graphics
{
    /// <summary>
    /// Provides properties for managing a bone weight.
    /// </summary>
    public struct BoneWeight
    {
        string boneName;
        float weight;

        /// <summary>
        /// Gets the name of the bone.
        /// </summary>
        public string BoneName
        {
            get
            {
                return boneName;
            }
        }

        /// <summary>
        /// Gets the amount of bone influence, ranging from zero to one. The complete set of weights in a BoneWeightCollection should sum to one.
        /// </summary>
        public float Weight
        {
            get
            {
                return weight;
            }
            internal set
            {
                weight = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of BoneWeight with the specified name and weight.
        /// </summary>
        /// <param name="boneName">Name of the bone.</param>
        /// <param name="weight">Amount of influence, ranging from zero to one.</param>
        public BoneWeight(string boneName, float weight)
        {
            this.boneName = boneName;
            this.weight = weight;
        }
    }
}
