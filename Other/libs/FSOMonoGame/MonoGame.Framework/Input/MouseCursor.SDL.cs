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
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Utilities;

namespace Microsoft.Xna.Framework.Input
{
    public partial class MouseCursor
    {
        private MouseCursor(Sdl.Mouse.SystemCursor cursor)
        {
            Handle = Sdl.Mouse.CreateSystemCursor(cursor);
        }

        private static void PlatformInitalize()
        {
            Arrow = new MouseCursor(Sdl.Mouse.SystemCursor.Arrow);
            IBeam = new MouseCursor(Sdl.Mouse.SystemCursor.IBeam);
            Wait = new MouseCursor(Sdl.Mouse.SystemCursor.Wait);
            Crosshair = new MouseCursor(Sdl.Mouse.SystemCursor.Crosshair);
            WaitArrow = new MouseCursor(Sdl.Mouse.SystemCursor.WaitArrow);
            SizeNWSE = new MouseCursor(Sdl.Mouse.SystemCursor.SizeNWSE);
            SizeNESW = new MouseCursor(Sdl.Mouse.SystemCursor.SizeNESW);
            SizeWE = new MouseCursor(Sdl.Mouse.SystemCursor.SizeWE);
            SizeNS = new MouseCursor(Sdl.Mouse.SystemCursor.SizeNS);
            SizeAll = new MouseCursor(Sdl.Mouse.SystemCursor.SizeAll);
            No = new MouseCursor(Sdl.Mouse.SystemCursor.No);
            Hand = new MouseCursor(Sdl.Mouse.SystemCursor.Hand);
        }

        private static MouseCursor PlatformFromTexture2D(Texture2D texture, int originx, int originy)
        {
            IntPtr surface = IntPtr.Zero;
            IntPtr handle = IntPtr.Zero;
            try
            {
                var bytes = new byte[texture.Width * texture.Height * 4];
                texture.GetData(bytes);
                surface = Sdl.CreateRGBSurfaceFrom(bytes, texture.Width, texture.Height, 32, texture.Width * 4, 0x000000ff, 0x0000FF00, 0x00FF0000, 0xFF000000);
                if (surface == IntPtr.Zero)
                    throw new InvalidOperationException("Failed to create surface for mouse cursor: " + Sdl.GetError());

                handle = Sdl.Mouse.CreateColorCursor(surface, originx, originy);
                if (handle == IntPtr.Zero)
                    throw new InvalidOperationException("Failed to set surface for mouse cursor: " + Sdl.GetError());
            }
            finally
            {
                if (surface != IntPtr.Zero)
                    Sdl.FreeSurface(surface);
            }

            return new MouseCursor(handle);
        }

        private void PlatformDispose()
        {
            if (Handle == IntPtr.Zero)
                return;
            
            Sdl.Mouse.FreeCursor(Handle);
            Handle = IntPtr.Zero;
        }
    }
}
