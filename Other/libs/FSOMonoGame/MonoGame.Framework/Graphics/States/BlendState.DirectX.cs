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

using System.Diagnostics;

namespace Microsoft.Xna.Framework.Graphics
{
    public partial class BlendState
    {
        private SharpDX.Direct3D11.BlendState _state;

        protected internal override void GraphicsDeviceResetting()
        {
            SharpDX.Utilities.Dispose(ref _state);
            base.GraphicsDeviceResetting();
        }

        internal SharpDX.Direct3D11.BlendState GetDxState(GraphicsDevice device)
        {
            if (_state == null)
            {
                // Build the description.
                var desc = new SharpDX.Direct3D11.BlendStateDescription();
                _targetBlendState[0].GetState(ref desc.RenderTarget[0]);
                _targetBlendState[1].GetState(ref desc.RenderTarget[1]);
                _targetBlendState[2].GetState(ref desc.RenderTarget[2]);
                _targetBlendState[3].GetState(ref desc.RenderTarget[3]);
                desc.IndependentBlendEnable = _independentBlendEnable;

                // This is a new DX11 feature we should consider 
                // exposing as part of the extended MonoGame API.
                desc.AlphaToCoverageEnable = false;

                // Create the state.
                _state = new SharpDX.Direct3D11.BlendState(GraphicsDevice._d3dDevice, desc);
            }

            Debug.Assert(GraphicsDevice == device, "The state was created for a different device!");

			// Apply the state!
			return _state;
        }

        partial void PlatformDispose()
        {
            SharpDX.Utilities.Dispose(ref _state);
        }
    }
}

