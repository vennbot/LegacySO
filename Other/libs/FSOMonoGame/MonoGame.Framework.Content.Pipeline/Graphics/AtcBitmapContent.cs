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
using Microsoft.Xna.Framework.Graphics;
using ATI.TextureConverter;

namespace Microsoft.Xna.Framework.Content.Pipeline.Graphics
{
    public abstract class AtcBitmapContent : BitmapContent
    {
        internal byte[] _bitmapData;

        public AtcBitmapContent()
            : base()
        {
        }

        public AtcBitmapContent(int width, int height)
            : base(width, height)
        {
        }

        public override byte[] GetPixelData()
        {
            return _bitmapData;
        }

        public override void SetPixelData(byte[] sourceData)
        {
            _bitmapData = sourceData;
        }

		protected override bool TryCopyFrom(BitmapContent sourceBitmap, Rectangle sourceRegion, Rectangle destinationRegion)
        {
            SurfaceFormat sourceFormat;
            if (!sourceBitmap.TryGetFormat(out sourceFormat))
                return false;

            SurfaceFormat format;
            TryGetFormat(out format);

            // A shortcut for copying the entire bitmap to another bitmap of the same type and format
            if (format == sourceFormat && (sourceRegion == new Rectangle(0, 0, Width, Height)) && sourceRegion == destinationRegion)
            {
                SetPixelData(sourceBitmap.GetPixelData());
                return true;
            }

            // Destination region copy is not yet supported
            if (destinationRegion != new Rectangle(0, 0, Width, Height))
                return false;

            // If the source is not Vector4 or requires resizing, send it through BitmapContent.Copy
            if (!(sourceBitmap is PixelBitmapContent<Vector4>) || sourceRegion.Width != destinationRegion.Width || sourceRegion.Height != destinationRegion.Height)
            {
                try
                {
                    BitmapContent.Copy(sourceBitmap, sourceRegion, this, destinationRegion);
                    return true;
                }
                catch (InvalidOperationException)
                {
                    return false;
                }
            }

            // Convert to full colour 32-bit format. Floating point would be preferred for processing, but it appears the ATICompressor does not support this
            var colorBitmap = new PixelBitmapContent<Color>(sourceRegion.Width, sourceRegion.Height);
            BitmapContent.Copy(sourceBitmap, sourceRegion, colorBitmap, new Rectangle(0, 0, colorBitmap.Width, colorBitmap.Height));
            sourceBitmap = colorBitmap;

			ATICompressor.CompressionFormat targetFormat;
			switch (format)
            {
				case SurfaceFormat.RgbaAtcExplicitAlpha:
					targetFormat = ATICompressor.CompressionFormat.AtcRgbaExplicitAlpha;
					break;
				case SurfaceFormat.RgbaAtcInterpolatedAlpha:
					targetFormat = ATICompressor.CompressionFormat.AtcRgbaInterpolatedAlpha;
					break;
				default:
					return false;
			}

			var sourceData = sourceBitmap.GetPixelData();
			var compressedData = ATICompressor.Compress(sourceData, Width, Height, targetFormat);
			SetPixelData(compressedData);

			return true;
        }

        protected override bool TryCopyTo(BitmapContent destinationBitmap, Rectangle sourceRegion, Rectangle destinationRegion)
        {
            SurfaceFormat destinationFormat;
            if (!destinationBitmap.TryGetFormat(out destinationFormat))
                return false;

            SurfaceFormat format;
            TryGetFormat(out format);

            // A shortcut for copying the entire bitmap to another bitmap of the same type and format
            if (format == destinationFormat && (sourceRegion == new Rectangle(0, 0, Width, Height)) && sourceRegion == destinationRegion)
            {
                destinationBitmap.SetPixelData(GetPixelData());
                return true;
            }

            // No other support for copying from a ATC texture yet
            return false;
        }
    }
}
