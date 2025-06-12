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

using MonoGame.OpenGL;

namespace Microsoft.Xna.Framework.Graphics
{
    public partial class BlendState
    {
        internal void PlatformApplyState(GraphicsDevice device, bool force = false)
        {
            var blendEnabled = !(this.ColorSourceBlend == Blend.One && 
                                 this.ColorDestinationBlend == Blend.Zero &&
                                 this.AlphaSourceBlend == Blend.One &&
                                 this.AlphaDestinationBlend == Blend.Zero);
            if (force || blendEnabled != device._lastBlendEnable)
            {
                if (blendEnabled)
                    GL.Enable(EnableCap.Blend);
                else
                    GL.Disable(EnableCap.Blend);
                GraphicsExtensions.CheckGLError();
                device._lastBlendEnable = blendEnabled;
            }

            if (force || 
                this.ColorBlendFunction != device._lastBlendState.ColorBlendFunction || 
                this.AlphaBlendFunction != device._lastBlendState.AlphaBlendFunction)
            {
                GL.BlendEquationSeparate(
                    this.ColorBlendFunction.GetBlendEquationMode(),
                    this.AlphaBlendFunction.GetBlendEquationMode());
                GraphicsExtensions.CheckGLError();
                device._lastBlendState.ColorBlendFunction = this.ColorBlendFunction;
                device._lastBlendState.AlphaBlendFunction = this.AlphaBlendFunction;
            }

            if (force ||
                this.ColorSourceBlend != device._lastBlendState.ColorSourceBlend ||
                this.ColorDestinationBlend != device._lastBlendState.ColorDestinationBlend ||
                this.AlphaSourceBlend != device._lastBlendState.AlphaSourceBlend ||
                this.AlphaDestinationBlend != device._lastBlendState.AlphaDestinationBlend)
            {
                GL.BlendFuncSeparate(
                    this.ColorSourceBlend.GetBlendFactorSrc(), 
                    this.ColorDestinationBlend.GetBlendFactorDest(), 
                    this.AlphaSourceBlend.GetBlendFactorSrc(), 
                    this.AlphaDestinationBlend.GetBlendFactorDest());
                GraphicsExtensions.CheckGLError();
                device._lastBlendState.ColorSourceBlend = this.ColorSourceBlend;
                device._lastBlendState.ColorDestinationBlend = this.ColorDestinationBlend;
                device._lastBlendState.AlphaSourceBlend = this.AlphaSourceBlend;
                device._lastBlendState.AlphaDestinationBlend = this.AlphaDestinationBlend;
            }

            if (force || this.ColorWriteChannels != device._lastBlendState.ColorWriteChannels)
            {
                GL.ColorMask(
                    (this.ColorWriteChannels & ColorWriteChannels.Red) != 0,
                    (this.ColorWriteChannels & ColorWriteChannels.Green) != 0,
                    (this.ColorWriteChannels & ColorWriteChannels.Blue) != 0,
                    (this.ColorWriteChannels & ColorWriteChannels.Alpha) != 0);
                GraphicsExtensions.CheckGLError();
                device._lastBlendState.ColorWriteChannels = this.ColorWriteChannels;
            }

            
        }
    }
}

