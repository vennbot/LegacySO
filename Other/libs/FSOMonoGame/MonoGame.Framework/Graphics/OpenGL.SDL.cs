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
using System.Security;
using MonoGame.Utilities;

namespace MonoGame.OpenGL
{
    internal partial class GL
    {
        static partial void LoadPlatformEntryPoints()
        {
            BoundApi = RenderApi.GL;
        }

        private static IGraphicsContext PlatformCreateContext (IWindowInfo info)
        {
            return new GraphicsContext(info);
        }
    }

    internal class EntryPointHelper
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr d_sdl_gl_getprocaddress(IntPtr proc);
        public static d_sdl_gl_getprocaddress GetProcAddress = FuncLoader.LoadFunction<d_sdl_gl_getprocaddress>(Sdl.NativeLibrary, "SDL_GL_GetProcAddress");
        
        internal static IntPtr GetAddress(string proc)
        {
            IntPtr p = Marshal.StringToHGlobalAnsi(proc);
            try
            {
                var addr = GetProcAddress(p);
                if (addr == IntPtr.Zero)
                    throw new EntryPointNotFoundException (proc);
                return addr;
            }
            finally
            {
                Marshal.FreeHGlobal(p);
            }
        }
    }
}

