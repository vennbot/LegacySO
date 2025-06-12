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

using Microsoft.Xna.Framework.Graphics;

namespace Microsoft.Xna.Framework
{
    public partial class GraphicsDeviceManager
    {
        partial void PlatformInitialize(PresentationParameters presentationParameters)
        {
            var surfaceFormat = _game.graphicsDeviceManager.PreferredBackBufferFormat.GetColorFormat();
            var depthStencilFormat = _game.graphicsDeviceManager.PreferredDepthStencilFormat;

            // TODO Need to get this data from the Presentation Parameters
            Sdl.GL.SetAttribute(Sdl.GL.Attribute.RedSize, surfaceFormat.R);
            Sdl.GL.SetAttribute(Sdl.GL.Attribute.GreenSize, surfaceFormat.G);
            Sdl.GL.SetAttribute(Sdl.GL.Attribute.BlueSize, surfaceFormat.B);
            Sdl.GL.SetAttribute(Sdl.GL.Attribute.AlphaSize, surfaceFormat.A);

            switch (depthStencilFormat)
            {
                case DepthFormat.None:
                    Sdl.GL.SetAttribute(Sdl.GL.Attribute.DepthSize, 0);
                    Sdl.GL.SetAttribute(Sdl.GL.Attribute.StencilSize, 0);
                    break;
                case DepthFormat.Depth16:
                    Sdl.GL.SetAttribute(Sdl.GL.Attribute.DepthSize, 16);
                    Sdl.GL.SetAttribute(Sdl.GL.Attribute.StencilSize, 0);
                    break;
                case DepthFormat.Depth24:
                    Sdl.GL.SetAttribute(Sdl.GL.Attribute.DepthSize, 24);
                    Sdl.GL.SetAttribute(Sdl.GL.Attribute.StencilSize, 0);
                    break;
                case DepthFormat.Depth24Stencil8:
                    Sdl.GL.SetAttribute(Sdl.GL.Attribute.DepthSize, 24);
                    Sdl.GL.SetAttribute(Sdl.GL.Attribute.StencilSize, 8);
                    break;
            }

            Sdl.GL.SetAttribute(Sdl.GL.Attribute.DoubleBuffer, 1);
            Sdl.GL.SetAttribute(Sdl.GL.Attribute.ContextMajorVersion, 2);
            Sdl.GL.SetAttribute(Sdl.GL.Attribute.ContextMinorVersion, 1);

            ((SdlGameWindow)SdlGameWindow.Instance).CreateWindow();
        }
    }
}
