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
using ObjCRuntime;
using System.Security;
using OpenGLES;

namespace MonoGame.OpenGL
{
    internal partial class GL
	{
        
        static partial void LoadPlatformEntryPoints()
		{
			BoundApi = RenderApi.ES;
		}

        private static IGraphicsContext PlatformCreateContext (IWindowInfo info)
        {
            return new GraphicsContext ();
        }

		internal static class EntryPointHelper {
			
			static IntPtr GL = IntPtr.Zero;

			static EntryPointHelper () 
			{
				try {
					GL = Dlfcn.dlopen("/System/Library/Frameworks/OpenGLES.framework/OpenGLES", 0);
				} catch (Exception ex) {
					System.Diagnostics.Debug.WriteLine (ex.ToString());
				}
			}

			public static IntPtr GetAddress(String function)
			{
				if (GL == IntPtr.Zero)
					return IntPtr.Zero;

				return Dlfcn.dlsym (GL, function);
			}
		}
	}

    public class GraphicsContext : IGraphicsContext
    {
        public GraphicsContext ()
        {
            Context = new EAGLContext (EAGLRenderingAPI.OpenGLES3);
        }

        public bool IsCurrent {
            get {
                return EAGLContext.CurrentContext == this.Context;
            }
        }

        public bool IsDisposed {
            get {
                return this.Context == null;
            }
        }

        public int SwapInterval {
            get {
                throw new NotImplementedException ();
            }

            set {
                throw new NotImplementedException ();
            }
        }

        public void Dispose ()
        {
            if (this.Context != null) {
                this.Context.Dispose ();
            }
            this.Context = null;
        }

        public void MakeCurrent (IWindowInfo info)
        {
            if (!EAGLContext.SetCurrentContext (this.Context)) {
                throw new InvalidOperationException ("Unable to change current EAGLContext.");
            }
        }

        public void SwapBuffers ()
        {
            if (!this.Context.PresentRenderBuffer (36161u)) {
                throw new InvalidOperationException ("EAGLContext.PresentRenderbuffer failed.");
            }
        }

        internal EAGLContext Context { get; private set; }
    }
}

