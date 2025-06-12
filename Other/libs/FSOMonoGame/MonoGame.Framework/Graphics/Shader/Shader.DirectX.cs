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

using System.IO;
using SharpDX.Direct3D11;

namespace Microsoft.Xna.Framework.Graphics
{
    internal partial class Shader
    {
        private VertexShader _vertexShader;
        private PixelShader _pixelShader;
        private byte[] _shaderBytecode;

        // Caches the DirectX input layouts for this vertex shader.
        private InputLayoutCache _inputLayouts;

        internal byte[] Bytecode
        {
            get { return _shaderBytecode; }
        }

        internal InputLayoutCache InputLayouts
        {
            get { return _inputLayouts; }
        }

        internal VertexShader VertexShader
        {
            get
            {
                if (_vertexShader == null)
                    CreateVertexShader();
                return _vertexShader;
            }
        }

        internal PixelShader PixelShader
        {
            get
            {
                if (_pixelShader == null)
                    CreatePixelShader();
                return _pixelShader;
            }
        }

        private static int PlatformProfile()
        {
            return 1;
        }

        private void PlatformConstruct(bool isVertexShader, byte[] shaderBytecode)
        {
            // We need the bytecode later for allocating the
            // input layout from the vertex declaration.
            _shaderBytecode = shaderBytecode;

            HashKey = MonoGame.Utilities.Hash.ComputeHash(Bytecode);

            if (isVertexShader)
                CreateVertexShader();
            else
                CreatePixelShader();
        }

        private void PlatformGraphicsDeviceResetting()
        {
            SharpDX.Utilities.Dispose(ref _vertexShader);
            SharpDX.Utilities.Dispose(ref _pixelShader);
            SharpDX.Utilities.Dispose(ref _inputLayouts);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                SharpDX.Utilities.Dispose(ref _vertexShader);
                SharpDX.Utilities.Dispose(ref _pixelShader);
                SharpDX.Utilities.Dispose(ref _inputLayouts);
            }

            base.Dispose(disposing);
        }

        private void CreatePixelShader()
        {
            System.Diagnostics.Debug.Assert(Stage == ShaderStage.Pixel);
            _pixelShader = new PixelShader(GraphicsDevice._d3dDevice, _shaderBytecode);
        }

        private void CreateVertexShader()
        {
            System.Diagnostics.Debug.Assert(Stage == ShaderStage.Vertex);
            _vertexShader = new VertexShader(GraphicsDevice._d3dDevice, _shaderBytecode, null);
            _inputLayouts = new InputLayoutCache(GraphicsDevice, Bytecode);
        }
    }
}
