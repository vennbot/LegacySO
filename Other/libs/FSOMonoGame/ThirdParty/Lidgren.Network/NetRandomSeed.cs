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
using System.Threading;

namespace Lidgren.Network
{
	/// <summary>
	/// Class for generating random seeds
	/// </summary>
	public static class NetRandomSeed
	{
		private static int m_seedIncrement = -1640531527;

		/// <summary>
		/// Generates a 32 bit random seed
		/// </summary>
		[CLSCompliant(false)]
		public static uint GetUInt32()
		{
			ulong seed = GetUInt64();
			uint low = (uint)seed;
			uint high = (uint)(seed >> 32);
			return low ^ high;
		}

		/// <summary>
		/// Generates a 64 bit random seed
		/// </summary>
		[CLSCompliant(false)]
		public static ulong GetUInt64()
		{
#if !__ANDROID__ && !IOS && !UNITY_WEBPLAYER && !UNITY_ANDROID && !UNITY_IPHONE
			ulong seed = (ulong)System.Diagnostics.Stopwatch.GetTimestamp();
			seed ^= (ulong)Environment.WorkingSet;
			ulong s2 = (ulong)Interlocked.Increment(ref m_seedIncrement);
			s2 |= (((ulong)Guid.NewGuid().GetHashCode()) << 32);
			seed ^= s2;
#else
			ulong seed = (ulong)Environment.TickCount;
			seed |= (((ulong)(new object().GetHashCode())) << 32);
			ulong s2 = (ulong)Guid.NewGuid().GetHashCode();
			s2 |= (((ulong)Interlocked.Increment(ref m_seedIncrement)) << 32);
			seed ^= s2;
#endif
			return seed;
		}
	}
}
