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
/*
* 12/12/99		Initial version.	mdm@techie.com
/*-----------------------------------------------------------------------
*  This program is free software; you can redistribute it and/or modify
*  it under the terms of the GNU General Public License as published by
*  the Free Software Foundation; either version 2 of the License, or
*  (at your option) any later version.
*
*  This program is distributed in the hope that it will be useful,
*  but WITHOUT ANY WARRANTY; without even the implied warranty of
*  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
*  GNU General Public License for more details.
*
*  You should have received a copy of the GNU General Public License
*  along with this program; if not, write to the Free Software
*  Foundation, Inc., 675 Mass Ave, Cambridge, MA 02139, USA.
*----------------------------------------------------------------------
*/
namespace javazoom.jl.decoder
{
	using System;
	
	/// <summary> This interface describes all error codes that can be thrown 
	/// in <code>BistreamException</code>s.
	/// 
	/// </summary>
	/// <seealso cref="">BitstreamException
	/// 
	/// </seealso>
	/// <author> 	MDM		12/12/99
	/// @since	0.0.6
	/// 
	/// </author>
	
	internal struct BitstreamErrors_Fields{
		public readonly static int UNKNOWN_ERROR;
		public readonly static int UNKNOWN_SAMPLE_RATE;
		public readonly static int STREAM_ERROR;
		public readonly static int UNEXPECTED_EOF;
		public readonly static int STREAM_EOF;
		public readonly static int BITSTREAM_LAST = 0x1ff;
		static BitstreamErrors_Fields()
		{
			UNKNOWN_ERROR = javazoom.jl.decoder.JavaLayerErrors_Fields.BITSTREAM_ERROR + 0;
			UNKNOWN_SAMPLE_RATE = javazoom.jl.decoder.JavaLayerErrors_Fields.BITSTREAM_ERROR + 1;
			STREAM_ERROR = javazoom.jl.decoder.JavaLayerErrors_Fields.BITSTREAM_ERROR + 2;
			UNEXPECTED_EOF = javazoom.jl.decoder.JavaLayerErrors_Fields.BITSTREAM_ERROR + 3;
			STREAM_EOF = javazoom.jl.decoder.JavaLayerErrors_Fields.BITSTREAM_ERROR + 4;
		}
	}
	internal interface BitstreamErrors : JavaLayerErrors
		{
			//UPGRADE_NOTE: Members of interface 'BitstreamErrors' were extracted into structure 'BitstreamErrors_Fields'. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1045"'
			/// <summary> An undeterminable error occurred. 
			/// </summary>
			/// <summary> The header describes an unknown sample rate.
			/// </summary>
			/// <summary> A problem occurred reading from the stream.
			/// </summary>
			/// <summary> The end of the stream was reached prematurely. 
			/// </summary>
			/// <summary> The end of the stream was reached. 
			/// </summary>
			/// <summary> 
			/// </summary>
		}
}
