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
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using Android.Opengl;
using Javax.Microedition.Khronos.Egl;

namespace MonoGame.OpenGL
{
    internal partial class GL
    {
		// internal for Android is not used on other platforms
		// it allows us to use either GLES or Full GL (if the GPU supports it)
		internal delegate bool BindAPIDelegate (RenderApi api);
		internal static BindAPIDelegate BindAPI;

        static partial void LoadPlatformEntryPoints()
        {
            Android.Util.Log.Verbose("GL", "Loading Entry Points");
            var ptr = EntryPointHelper.GetAddress("eglBindAPI", throwIfNotFound: false);
            if (ptr != IntPtr.Zero)
                BindAPI = (BindAPIDelegate)Marshal.GetDelegateForFunctionPointer (ptr, typeof(BindAPIDelegate));
            var supportsFullGL = ptr != IntPtr.Zero && BindAPI (RenderApi.GL);
			if (!supportsFullGL) {
                if (ptr != IntPtr.Zero)
				    BindAPI (RenderApi.ES);
				BoundApi = RenderApi.ES;
			}
            Android.Util.Log.Verbose("GL", "Bound {0}", BoundApi);
        }

        private static IGraphicsContext PlatformCreateContext (IWindowInfo info)
        {
            return null;//new GraphicsContext(info);
        }
    }

    struct GLESVersion
    {
        const int EglContextClientVersion = 0x3098;
        const int EglContextMinorVersion = 0x30fb;

        public int Major;
        public int Minor;

        internal int[] GetAttributes()
        {
            int minor = Minor > -1 ? EglContextMinorVersion : EGL10.EglNone;
            return new int[] { EglContextClientVersion, Major, minor, Minor, EGL10.EglNone };
        }

        public override string ToString()
        {
            return string.Format("{0}.{1}", Major, Minor == -1 ? 0 : Minor);
        }

        internal static IEnumerable<GLESVersion> GetSupportedGLESVersions()
        {
            if (EntryPointHelper.libES3 != IntPtr.Zero)
            {
                yield return new GLESVersion { Major = 3, Minor = 2 };
                yield return new GLESVersion { Major = 3, Minor = 1 };
                yield return new GLESVersion { Major = 3, Minor = 0 };
            }
            if (EntryPointHelper.libES2 != IntPtr.Zero)
            {
                // We pass -1 becuase when requesting a GLES 2.0 context we 
                // dont provide the Minor version.
                yield return new GLESVersion { Major = 2, Minor = -1 };
            }
            yield return new GLESVersion();
        }
    }

	internal static class EntryPointHelper {
		
		internal static IntPtr libES1 = DL.Open("libGLESv1_CM.so");
        internal static IntPtr libES2 = DL.Open("libGLESv2.so");
        internal static IntPtr libES3 = DL.Open("libGLESv3.so");
        internal static IntPtr libGL = DL.Open("libGL.so");

        public static IntPtr GetAddress(String function, bool throwIfNotFound = true)
		{
            IntPtr result = IntPtr.Zero;
            if (GL.BoundApi == GL.RenderApi.ES && libES3 != IntPtr.Zero)
            {
                result = DL.Symbol(libES3, function);
            }
            if (GL.BoundApi == GL.RenderApi.ES && libES2 != IntPtr.Zero)
			{
                result = DL.Symbol(libES2, function);
			}
			else if (GL.BoundApi == GL.RenderApi.GL && libGL != IntPtr.Zero)
			{
				result = DL.Symbol(libGL, function);
			}
            Android.Util.Log.Verbose("GL", "{0} was {1}", function, (result != IntPtr.Zero) ? "Found" : "No Found");
            if (result == IntPtr.Zero && throwIfNotFound)
                throw new EntryPointNotFoundException(string.Format("EntryPoint {0} not found", function));
            return result;
		}
	}
	
	
	internal class DL
	{
		internal enum DLOpenFlags
		{
			Lazy = 0x0001,
			Now = 0x0002,
			Global = 0x0100,
			Local = 0x0000,
		}

		const string lib = "dl";

		[DllImport(lib, EntryPoint = "dlopen")]
		internal static extern IntPtr Open(string filename, DLOpenFlags flags = DLOpenFlags.Lazy);

		[DllImport(lib, EntryPoint = "dlclose")]
		internal static extern int Close(IntPtr handle);

		[DllImport(lib, EntryPoint = "dlsym")]
		internal static extern IntPtr Symbol(IntPtr handle, String name);
	}
}

