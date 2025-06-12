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
using System;
using System.Runtime.InteropServices;

namespace TwoMGFX
{
	internal class MarshalHelper
	{
        public static T Unmarshal<T>(IntPtr ptr)
        {
            var type = typeof(T);
            var result = (T)Marshal.PtrToStructure(ptr, type);
            return result;
        }

		public static T[] UnmarshalArray<T>(IntPtr ptr, int count) 
        {
			var type = typeof(T);
            var size = Marshal.SizeOf(type);            
            var ret = new T[count];

            for (int i = 0; i < count; i++)
            {
                var offset = i * size;
				var structPtr = new IntPtr(ptr.ToInt64() + offset);
                ret[i] = (T)Marshal.PtrToStructure(structPtr, type);
            }

			return ret;
		}

        public static byte[] UnmarshalArray(IntPtr ptr, int count)
        {
            var result = new byte[count];
            Marshal.Copy(ptr, result, 0, count);
            return result;
        }	
	}
}

