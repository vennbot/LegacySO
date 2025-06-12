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
namespace javazoom.jl.decoder
{
	using System;
	
	/// <summary> Work in progress.
	/// </summary>
	
	internal interface Control
		{
			bool Playing
			{
				get;
				
			}
			bool RandomAccess
			{
				get;
				
			}
			/// <summary> Retrieves the current position.
			/// </summary>
			/// <summary> 
			/// </summary>
			double Position
			{
				get;
				
				set;
				
			}
			/// <summary> Starts playback of the media presented by this control.
			/// </summary>
			void  start();
			/// <summary> Stops playback of the media presented by this control.
			/// </summary>
			void  stop();
			void  pause();
		}
}
