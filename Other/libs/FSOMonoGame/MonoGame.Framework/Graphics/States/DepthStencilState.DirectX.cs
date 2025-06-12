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
using System.Diagnostics;

namespace Microsoft.Xna.Framework.Graphics
{
    public partial class DepthStencilState
    {
        private SharpDX.Direct3D11.DepthStencilState _state;

        protected internal override void GraphicsDeviceResetting()
        {
            SharpDX.Utilities.Dispose(ref _state);
            base.GraphicsDeviceResetting();
        }

        internal void PlatformApplyState(GraphicsDevice device)
        {
            if (_state == null)
            {
                // Build the description.
                var desc = new SharpDX.Direct3D11.DepthStencilStateDescription();

                desc.IsDepthEnabled = DepthBufferEnable;
                desc.DepthComparison = DepthBufferFunction.ToComparison();

                if (DepthBufferWriteEnable)
                    desc.DepthWriteMask = SharpDX.Direct3D11.DepthWriteMask.All;
                else
                    desc.DepthWriteMask = SharpDX.Direct3D11.DepthWriteMask.Zero;

                desc.IsStencilEnabled = StencilEnable;
                desc.StencilReadMask = (byte)StencilMask; // TODO: Should this instead grab the upper 8bits?
                desc.StencilWriteMask = (byte)StencilWriteMask;

                if (TwoSidedStencilMode)
                {
                    desc.BackFace.Comparison = CounterClockwiseStencilFunction.ToComparison();
                    desc.BackFace.DepthFailOperation = GetStencilOp(CounterClockwiseStencilDepthBufferFail);
                    desc.BackFace.FailOperation = GetStencilOp(CounterClockwiseStencilFail);
                    desc.BackFace.PassOperation = GetStencilOp(CounterClockwiseStencilPass);
                }
                else
                {   //use same settings as frontFace 
                    desc.BackFace.Comparison = StencilFunction.ToComparison();
                    desc.BackFace.DepthFailOperation = GetStencilOp(StencilDepthBufferFail);
                    desc.BackFace.FailOperation = GetStencilOp(StencilFail);
                    desc.BackFace.PassOperation = GetStencilOp(StencilPass);
                }

                desc.FrontFace.Comparison = StencilFunction.ToComparison();
                desc.FrontFace.DepthFailOperation = GetStencilOp(StencilDepthBufferFail);
                desc.FrontFace.FailOperation = GetStencilOp(StencilFail);
                desc.FrontFace.PassOperation = GetStencilOp(StencilPass);

                // Create the state.
                _state = new SharpDX.Direct3D11.DepthStencilState(GraphicsDevice._d3dDevice, desc);
            }

            Debug.Assert(GraphicsDevice == device, "The state was created for a different device!");

            // NOTE: We make the assumption here that the caller has
            // locked the d3dContext for us to use.

            // Apply the state!
            device._d3dContext.OutputMerger.SetDepthStencilState(_state, ReferenceStencil);
        }

        static private SharpDX.Direct3D11.StencilOperation GetStencilOp(StencilOperation op)
        {
            switch (op)
            {
                case StencilOperation.Decrement:
                    return SharpDX.Direct3D11.StencilOperation.Decrement;

                case StencilOperation.DecrementSaturation:
                    return SharpDX.Direct3D11.StencilOperation.DecrementAndClamp;

                case StencilOperation.Increment:
                    return SharpDX.Direct3D11.StencilOperation.Increment;

                case StencilOperation.IncrementSaturation:
                    return SharpDX.Direct3D11.StencilOperation.IncrementAndClamp;

                case StencilOperation.Invert:
                    return SharpDX.Direct3D11.StencilOperation.Invert;

                case StencilOperation.Keep:
                    return SharpDX.Direct3D11.StencilOperation.Keep;

                case StencilOperation.Replace:
                    return SharpDX.Direct3D11.StencilOperation.Replace;

                case StencilOperation.Zero:
                    return SharpDX.Direct3D11.StencilOperation.Zero;

                default:
                    throw new ArgumentException("Invalid stencil operation!");
            }
        }

        partial void PlatformDispose()
        {
            SharpDX.Utilities.Dispose(ref _state);
        }
    }
}

