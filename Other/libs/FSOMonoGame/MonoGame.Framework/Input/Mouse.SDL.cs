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

namespace Microsoft.Xna.Framework.Input
{
    public static partial class Mouse
    {
        internal static int ScrollX;
        internal static int ScrollY;

        private static IntPtr PlatformGetWindowHandle()
        {
            return PrimaryWindow.Handle;
        }
        
        private static void PlatformSetWindowHandle(IntPtr windowHandle)
        {
        }

        private static MouseState PlatformGetState(GameWindow window)
        {
            int x, y;
            var winFlags = Sdl.Window.GetWindowFlags(window.Handle);
            var state = Sdl.Mouse.GetGlobalState(out x, out y);

            if ((winFlags & Sdl.Window.State.MouseFocus) != 0)
            {
                // Window has mouse focus, position will be set from the motion event
                window.MouseState.LeftButton = (state & Sdl.Mouse.Button.Left) != 0 ? ButtonState.Pressed : ButtonState.Released;
                window.MouseState.MiddleButton = (state & Sdl.Mouse.Button.Middle) != 0 ? ButtonState.Pressed : ButtonState.Released;
                window.MouseState.RightButton = (state & Sdl.Mouse.Button.Right) != 0 ? ButtonState.Pressed : ButtonState.Released;
                window.MouseState.XButton1 = (state & Sdl.Mouse.Button.X1Mask) != 0 ? ButtonState.Pressed : ButtonState.Released;
                window.MouseState.XButton2 = (state & Sdl.Mouse.Button.X2Mask) != 0 ? ButtonState.Pressed : ButtonState.Released;

                window.MouseState.HorizontalScrollWheelValue = ScrollX;
                window.MouseState.ScrollWheelValue = ScrollY;
            }
            else
            {
                // Window does not have mouse focus, we need to manually get the position
                var clientBounds = window.ClientBounds;
                window.MouseState.X = x - clientBounds.X;
                window.MouseState.Y = y - clientBounds.Y;
            }

            return window.MouseState;
        }

        private static void PlatformSetPosition(int x, int y)
        {
            PrimaryWindow.MouseState.X = x;
            PrimaryWindow.MouseState.Y = y;
            
            Sdl.Mouse.WarpInWindow(PrimaryWindow.Handle, x, y);
        }

        private static void PlatformSetCursor(MouseCursor cursor)
        {
            Sdl.Mouse.SetCursor(cursor.Handle);
        }
    }
}
