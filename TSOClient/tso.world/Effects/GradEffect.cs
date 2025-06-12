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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FSO.LotView.Effects
{
    /// <summary>
    /// An effect for drawing gradients. Used to draw objcet/wall shadow geometry for Moderate FSO Lighting
    /// </summary>
    public class GradEffect : WorldEffect
    {
        protected override Type TechniqueType => base.TechniqueType;

        private EffectParameter pProjection;
        
        public Matrix Projection
        {
            set
            {
                pProjection.SetValue(value);
            }
        }

        public GradEffect(GraphicsDevice graphicsDevice, byte[] effectCode) : base(graphicsDevice, effectCode)
        {
        }

        public GradEffect(GraphicsDevice graphicsDevice, byte[] effectCode, int index, int count) : base(graphicsDevice, effectCode, index, count)
        {
        }

        public GradEffect(Effect cloneSource) : base(cloneSource)
        {
        }

        protected override void PrepareParams()
        {
            base.PrepareParams();

            pProjection = Parameters["Projection"];
        }
    }

    public enum GradEffectTechniques
    {
        Draw2D
    }
}
