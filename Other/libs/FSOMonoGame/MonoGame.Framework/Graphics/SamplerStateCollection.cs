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
//
// Author: Kenneth James Pouncey

using System;

namespace Microsoft.Xna.Framework.Graphics
{
    public sealed partial class SamplerStateCollection
	{
        private readonly GraphicsDevice _graphicsDevice;

        private readonly SamplerState _samplerStateAnisotropicClamp;
        private readonly SamplerState _samplerStateAnisotropicWrap;
        private readonly SamplerState _samplerStateLinearClamp;
        private readonly SamplerState _samplerStateLinearWrap;
        private readonly SamplerState _samplerStatePointClamp;
        private readonly SamplerState _samplerStatePointWrap;

        private readonly SamplerState[] _samplers;
        private readonly SamplerState[] _actualSamplers;
        private readonly bool _applyToVertexStage;

		internal SamplerStateCollection(GraphicsDevice device, int maxSamplers, bool applyToVertexStage)
		{
		    _graphicsDevice = device;

            _samplerStateAnisotropicClamp = SamplerState.AnisotropicClamp.Clone();
            _samplerStateAnisotropicWrap = SamplerState.AnisotropicWrap.Clone();
            _samplerStateLinearClamp = SamplerState.LinearClamp.Clone();
            _samplerStateLinearWrap = SamplerState.LinearWrap.Clone();
            _samplerStatePointClamp = SamplerState.PointClamp.Clone();
            _samplerStatePointWrap = SamplerState.PointWrap.Clone();

            _samplers = new SamplerState[maxSamplers];
            _actualSamplers = new SamplerState[maxSamplers];
            _applyToVertexStage = applyToVertexStage;

		    Clear();
        }
		
		public SamplerState this [int index] 
        {
			get 
            { 
                return _samplers[index]; 
            }

			set 
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                if (_samplers[index] == value)
                    return;

                _samplers[index] = value;

                // Static state properties never actually get bound;
                // instead we use our GraphicsDevice-specific version of them.
                var newSamplerState = value;
                if (ReferenceEquals(value, SamplerState.AnisotropicClamp))
                    newSamplerState = _samplerStateAnisotropicClamp;
                else if (ReferenceEquals(value, SamplerState.AnisotropicWrap))
                    newSamplerState = _samplerStateAnisotropicWrap;
                else if (ReferenceEquals(value, SamplerState.LinearClamp))
                    newSamplerState = _samplerStateLinearClamp;
                else if (ReferenceEquals(value, SamplerState.LinearWrap))
                    newSamplerState = _samplerStateLinearWrap;
                else if (ReferenceEquals(value, SamplerState.PointClamp))
                    newSamplerState = _samplerStatePointClamp;
                else if (ReferenceEquals(value, SamplerState.PointWrap))
                    newSamplerState = _samplerStatePointWrap;

                newSamplerState.BindToGraphicsDevice(_graphicsDevice);

                _actualSamplers[index] = newSamplerState;

                PlatformSetSamplerState(index);
            }
		}

        internal void Clear()
        {
            for (var i = 0; i < _samplers.Length; i++)
            {
                _samplers[i] = SamplerState.LinearWrap;

                _samplerStateLinearWrap.BindToGraphicsDevice(_graphicsDevice);
                _actualSamplers[i] = _samplerStateLinearWrap;
            }

            PlatformClear();
        }

        /// <summary>
        /// Mark all the sampler slots as dirty.
        /// </summary>
        internal void Dirty()
        {
            PlatformDirty();
        }
	}
}
