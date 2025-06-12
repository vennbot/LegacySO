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

namespace Microsoft.Xna.Framework.Graphics
{
	public partial class RenderTarget3D : Texture3D, IRenderTarget
	{
		public DepthFormat DepthStencilFormat { get; private set; }
		
		public int MultiSampleCount { get; private set; }
		
		public RenderTargetUsage RenderTargetUsage { get; private set; }
		
		public bool IsContentLost { get { return false; } }
		
		public event EventHandler<EventArgs> ContentLost;

        private bool SuppressEventHandlerWarningsUntilEventsAreProperlyImplemented()
        {
            return ContentLost != null;
        }

		public RenderTarget3D(GraphicsDevice graphicsDevice, int width, int height, int depth, bool mipMap, SurfaceFormat preferredFormat, DepthFormat preferredDepthFormat, int preferredMultiSampleCount, RenderTargetUsage usage)
			:base (graphicsDevice, width, height, depth, mipMap, QuerySelectedFormat(graphicsDevice, preferredFormat), true)
		{
			DepthStencilFormat = preferredDepthFormat;
			MultiSampleCount = preferredMultiSampleCount;
			RenderTargetUsage = usage;

            // If we don't need a depth buffer then we're done.
            if (preferredDepthFormat == DepthFormat.None)
                return;

            PlatformConstruct(graphicsDevice, width, height, mipMap, preferredDepthFormat, preferredMultiSampleCount, usage);
        }

        protected static SurfaceFormat QuerySelectedFormat(GraphicsDevice graphicsDevice, SurfaceFormat preferredFormat)
        {
			SurfaceFormat selectedFormat;
			DepthFormat selectedDepthFormat;
			int selectedMultiSampleCount;

            graphicsDevice.Adapter.QueryRenderTargetFormat(graphicsDevice.GraphicsProfile, preferredFormat, DepthFormat.None, 0, 
                out selectedFormat, out selectedDepthFormat, out selectedMultiSampleCount);
            
            return selectedFormat;
        }

		public RenderTarget3D(GraphicsDevice graphicsDevice, int width, int height, int depth, bool mipMap, SurfaceFormat preferredFormat, DepthFormat preferredDepthFormat)
			:this (graphicsDevice, width, height, depth, mipMap, preferredFormat, preferredDepthFormat, 0, RenderTargetUsage.DiscardContents) 
		{}
		
		public RenderTarget3D(GraphicsDevice graphicsDevice, int width, int height, int depth)
			: this(graphicsDevice, width, height, depth, false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.DiscardContents) 
		{}
	}
}
