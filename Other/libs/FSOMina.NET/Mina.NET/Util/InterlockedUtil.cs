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
namespace Mina.Util
{
    /// <summary>
    /// Interlock Utils
    /// </summary>
    public static class InterlockedUtil
    {

        /*****************************************************************
         * CompareExchange<T>
         * 
         * if mina.net.unity3d run in ios environment
         * can't cross compiler for System.Threading.Interlocked.CompareExchange<T>
         * because System.Threading.Interlocked.CompareExchange<T> use JIT compiler.
         *****************************************************************/
        public static T CompareExchange<T>(ref T location1, T value, T comparand) where T : class
        {
#if UNITY
            var result = location1;// return value
            if (location1 == comparand)
                location1 = value;
            return result;
#else
            return System.Threading.Interlocked.CompareExchange(ref location1, value, comparand);
#endif
        }

        /// <summary>
        /// <seealso cref="System.Threading.Interlocked.CompareExchange(ref int, int, int)"/>
        /// </summary>
        public static int CompareExchange(ref int location1, int value, int comparand)
        {
            return System.Threading.Interlocked.CompareExchange(ref location1, value, comparand);
        }

        /// <summary>
        /// <seealso cref="System.Threading.Interlocked.Decrement(ref int)"/>
        /// </summary>
        public static int Decrement(ref int location)
        {
            return System.Threading.Interlocked.Decrement(ref location);
        }

        /// <summary>
        /// <seealso cref="System.Threading.Interlocked.Increment(ref int)"/>
        /// </summary>
        public static int Increment(ref int location)
        {
            return System.Threading.Interlocked.Increment(ref location);
        }

        /// <summary>
        /// <seealso cref="System.Threading.Interlocked.Add(ref int, int)"/>
        /// </summary>
        public static int Add(ref int location1, int value)
        {
            return System.Threading.Interlocked.Add(ref location1, value);
        }
    }
}
