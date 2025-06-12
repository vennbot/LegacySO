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

namespace Microsoft.Xna.Framework.Graphics
{
    /// <summary>
    /// A snapshot of rendering statistics from <see cref="GraphicsDevice.Metrics"/> to be used for runtime debugging and profiling.
    /// </summary>
    public struct GraphicsMetrics
    {
        internal long _clearCount;
        internal long _drawCount;
        internal long _pixelShaderCount;
        internal long _primitiveCount;
        internal long _spriteCount;
        internal long _targetCount;
        internal long _textureCount;
        internal long _vertexShaderCount;

        /// <summary>
        /// Number of times Clear was called.
        /// </summary>
        public long ClearCount { get { return _clearCount; } }

        /// <summary>
        /// Number of times Draw was called.
        /// </summary>
        public long DrawCount { get { return _drawCount; } }

        /// <summary>
        /// Number of times the pixel shader was changed on the GPU.
        /// </summary>
        public long PixelShaderCount { get { return _pixelShaderCount; } }

        /// <summary>
        /// Number of rendered primitives.
        /// </summary>
        public long PrimitiveCount { get { return _primitiveCount; } }

        /// <summary>
        /// Number of sprites and text characters rendered via <see cref="SpriteBatch"/>.
        /// </summary>
        public long SpriteCount { get { return _spriteCount; } }

        /// <summary>
        /// Number of times a target was changed on the GPU.
        /// </summary>
        public long TargetCount {get { return _targetCount; } }

        /// <summary>
        /// Number of times a texture was changed on the GPU.
        /// </summary>
        public long TextureCount { get { return _textureCount; } }

        /// <summary>
        /// Number of times the vertex shader was changed on the GPU.
        /// </summary>
        public long VertexShaderCount { get { return _vertexShaderCount; } }

        /// <summary>
        /// Returns the difference between two sets of metrics.
        /// </summary>
        /// <param name="value1">Source <see cref="GraphicsMetrics"/> on the left of the sub sign.</param>
        /// <param name="value2">Source <see cref="GraphicsMetrics"/> on the right of the sub sign.</param>
        /// <returns>Difference between two sets of metrics.</returns>
        public static GraphicsMetrics operator -(GraphicsMetrics value1, GraphicsMetrics value2)
        {
            return new GraphicsMetrics()
            {
                _clearCount = value1._clearCount - value2._clearCount,
                _drawCount = value1._drawCount - value2._drawCount,
                _pixelShaderCount = value1._pixelShaderCount - value2._pixelShaderCount,
                _primitiveCount = value1._primitiveCount - value2._primitiveCount,
                _spriteCount = value1._spriteCount - value2._spriteCount,
                _targetCount = value1._targetCount - value2._targetCount,
                _textureCount = value1._textureCount - value2._textureCount,
                _vertexShaderCount = value1._vertexShaderCount - value2._vertexShaderCount
            };
        }

        /// <summary>
        /// Returns the combination of two sets of metrics.
        /// </summary>
        /// <param name="value1">Source <see cref="GraphicsMetrics"/> on the left of the add sign.</param>
        /// <param name="value2">Source <see cref="GraphicsMetrics"/> on the right of the add sign.</param>
        /// <returns>Combination of two sets of metrics.</returns>
        public static GraphicsMetrics operator +(GraphicsMetrics value1, GraphicsMetrics value2)
        {
            return new GraphicsMetrics()
            {
                _clearCount =  value1._clearCount + value2._clearCount,
                _drawCount = value1._drawCount + value2._drawCount,
                _pixelShaderCount = value1._pixelShaderCount + value2._pixelShaderCount,
                _primitiveCount = value1._primitiveCount + value2._primitiveCount,
                _spriteCount = value1._spriteCount + value2._spriteCount,
                _targetCount = value1._targetCount + value2._targetCount,
                _textureCount = value1._textureCount + value2._textureCount,
                _vertexShaderCount = value1._vertexShaderCount + value2._vertexShaderCount
            };
        }
    }
}
