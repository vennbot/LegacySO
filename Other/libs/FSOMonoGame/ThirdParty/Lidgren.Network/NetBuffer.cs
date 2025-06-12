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
using System.Collections.Generic;
using System.Reflection;

namespace Lidgren.Network
{
	public partial class NetBuffer
	{
		/// <summary>
		/// Number of bytes to overallocate for each message to avoid resizing
		/// </summary>
		protected const int c_overAllocateAmount = 4;

		private static readonly Dictionary<Type, MethodInfo> s_readMethods;
		private static readonly Dictionary<Type, MethodInfo> s_writeMethods;

		internal byte[] m_data;
		internal int m_bitLength;
		internal int m_readPosition;

		/// <summary>
		/// Gets or sets the internal data buffer
		/// </summary>
		public byte[] Data
		{
			get { return m_data; }
			set { m_data = value; }
		}

		/// <summary>
		/// Gets or sets the length of the used portion of the buffer in bytes
		/// </summary>
		public int LengthBytes
		{
			get { return ((m_bitLength + 7) >> 3); }
			set
			{
				m_bitLength = value * 8;
				InternalEnsureBufferSize(m_bitLength);
			}
		}

		/// <summary>
		/// Gets or sets the length of the used portion of the buffer in bits
		/// </summary>
		public int LengthBits
		{
			get { return m_bitLength; }
			set
			{
				m_bitLength = value;
				InternalEnsureBufferSize(m_bitLength);
			}
		}

		/// <summary>
		/// Gets or sets the read position in the buffer, in bits (not bytes)
		/// </summary>
		public long Position
		{
			get { return (long)m_readPosition; }
			set { m_readPosition = (int)value; }
		}

		/// <summary>
		/// Gets the position in the buffer in bytes; note that the bits of the first returned byte may already have been read - check the Position property to make sure.
		/// </summary>
		public int PositionInBytes
		{
			get { return (int)(m_readPosition / 8); }
		}
		
		static NetBuffer()
		{
			s_readMethods = new Dictionary<Type, MethodInfo>();
			MethodInfo[] methods = typeof(NetIncomingMessage).GetMethods(BindingFlags.Instance | BindingFlags.Public);
			foreach (MethodInfo mi in methods)
			{
				if (mi.GetParameters().Length == 0 && mi.Name.StartsWith("Read", StringComparison.InvariantCulture) && mi.Name.Substring(4) == mi.ReturnType.Name)
				{
					s_readMethods[mi.ReturnType] = mi;
				}
			}

			s_writeMethods = new Dictionary<Type, MethodInfo>();
			methods = typeof(NetOutgoingMessage).GetMethods(BindingFlags.Instance | BindingFlags.Public);
			foreach (MethodInfo mi in methods)
			{
				if (mi.Name.Equals("Write", StringComparison.InvariantCulture))
				{
					ParameterInfo[] pis = mi.GetParameters();
					if (pis.Length == 1)
						s_writeMethods[pis[0].ParameterType] = mi;
				}
			}
		}
	}
}
