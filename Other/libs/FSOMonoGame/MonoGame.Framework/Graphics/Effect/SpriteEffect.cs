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

// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.

namespace Microsoft.Xna.Framework.Graphics
{
    /// <summary>
    /// The default effect used by SpriteBatch.
    /// </summary>
    public class SpriteEffect : Effect
    {
        #region Effect Parameters

        EffectParameter matrixParam;

        #endregion

        #region Methods

        /// <summary>
        /// Creates a new SpriteEffect.
        /// </summary>
        public SpriteEffect(GraphicsDevice device)
            : base(device, EffectResource.SpriteEffect.Bytecode)
        {
            CacheEffectParameters();
        }

        /// <summary>
        /// Creates a new SpriteEffect by cloning parameter settings from an existing instance.
        /// </summary>
        protected SpriteEffect(SpriteEffect cloneSource)
            : base(cloneSource)
        {
            CacheEffectParameters();
        }


        /// <summary>
        /// Creates a clone of the current SpriteEffect instance.
        /// </summary>
        public override Effect Clone()
        {
            return new SpriteEffect(this);
        }


        /// <summary>
        /// Looks up shortcut references to our effect parameters.
        /// </summary>
        void CacheEffectParameters()
        {
            matrixParam = Parameters["MatrixTransform"];
        }

        /// <summary>
        /// Lazily computes derived parameter values immediately before applying the effect.
        /// </summary>
        protected internal override void OnApply()
        {
            var viewport = GraphicsDevice.Viewport;

            var projection = Matrix.CreateOrthographicOffCenter(0, viewport.Width, viewport.Height, 0, 0, 1);
            var halfPixelOffset = Matrix.CreateTranslation(0, 0, 0);

            if (SpriteBatch.NeedsHalfPixelOffset){
                halfPixelOffset += Matrix.CreateTranslation(-0.5f, -0.5f, 0);
            }

            matrixParam.SetValue(halfPixelOffset * projection);
        }


        #endregion
    }
}
