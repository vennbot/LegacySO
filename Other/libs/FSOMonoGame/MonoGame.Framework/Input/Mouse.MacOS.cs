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
using System.Runtime.InteropServices;
using System.Drawing;

#if PLATFORM_MACOS_LEGACY
using MonoMac.Foundation;
using MonoMac.AppKit;
#else
using Foundation;
using AppKit;
using PointF = CoreGraphics.CGPoint;
#endif

namespace Microsoft.Xna.Framework.Input
{
    public static partial class Mouse
    {
#if PLATFORM_MACOS_LEGACY
        [DllImport (MonoMac.Constants.CoreGraphicsLibrary)]
        extern static void CGWarpMouseCursorPosition(PointF newCursorPosition);
        
        [DllImport (MonoMac.Constants.CoreGraphicsLibrary)]
        extern static void CGSetLocalEventsSuppressionInterval(double seconds);
#else
        [DllImport(ObjCRuntime.Constants.CoreGraphicsLibrary)]
        extern static void CGWarpMouseCursorPosition(CoreGraphics.CGPoint newCursorPosition);

        [DllImport(ObjCRuntime.Constants.CoreGraphicsLibrary)]
        extern static void CGSetLocalEventsSuppressionInterval(double seconds);
#endif

        internal static GameWindow Window;
        internal static float HorizontalScrollWheelValue;
        internal static float ScrollWheelValue;

        private static IntPtr PlatformGetWindowHandle()
        {
            return IntPtr.Zero;
        }

        private static void PlatformSetWindowHandle(IntPtr windowHandle)
        {
        }

        private static MouseState PlatformGetState(GameWindow window)
        {
            //We need to maintain precision...
            window.MouseState.HorizontalScrollWheelValue = (int)HorizontalScrollWheelValue;
            window.MouseState.ScrollWheelValue = (int)ScrollWheelValue;

            return window.MouseState;
        }

        private static void PlatformSetPosition(int x, int y)
        {
            PrimaryWindow.MouseState.X = x;
            PrimaryWindow.MouseState.Y = y;
            
            var mousePt = NSEvent.CurrentMouseLocation;
            NSScreen currentScreen = null;
            foreach (var screen in NSScreen.Screens)
            {
                if (screen.Frame.Contains(mousePt))
                {
                    currentScreen = screen;
                    break;
                }
            }

            var point = new PointF(x, Window.ClientBounds.Height - y);
            var windowPt = Window.ConvertPointToView(point, null);
            var screenPt = Window.Window.ConvertBaseToScreen(windowPt);
            var flippedPt = new PointF(screenPt.X, currentScreen.Frame.Size.Height - screenPt.Y);
            flippedPt.Y += currentScreen.Frame.Location.Y;

            CGSetLocalEventsSuppressionInterval(0.0);
            CGWarpMouseCursorPosition(flippedPt);
            CGSetLocalEventsSuppressionInterval(0.25);
        }

        public static void PlatformSetCursor(MouseCursor cursor)
        {

        }
    }
}
