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
* 1/12/99		Initial version.	mdm@techie.com
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
	
	/// <summary> This interface provides constants describing the error
	/// codes used by the Decoder to indicate errors. 
	/// 
	/// </summary>
	/// <author> 	MDM
	/// 
	/// </author>
	
	internal struct DecoderErrors_Fields{
		public readonly static int UNKNOWN_ERROR;
		public readonly static int UNSUPPORTED_LAYER;
		static DecoderErrors_Fields()
		{
			UNKNOWN_ERROR = javazoom.jl.decoder.JavaLayerErrors_Fields.DECODER_ERROR + 0;
			UNSUPPORTED_LAYER = javazoom.jl.decoder.JavaLayerErrors_Fields.DECODER_ERROR + 1;
		}
	}
	internal interface DecoderErrors : JavaLayerErrors
		{
			//UPGRADE_NOTE: Members of interface 'DecoderErrors' were extracted into structure 'DecoderErrors_Fields'. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1045"'
			/// <summary> Layer not supported by the decoder. 
			/// </summary>
		}
}
