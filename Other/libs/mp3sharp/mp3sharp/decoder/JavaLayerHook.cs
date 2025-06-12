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
	/// <summary> The <code>JavaLayerHooks</code> class allows developers to change
	/// the way the JavaLayer library uses Resources. 
	/// </summary>
	
	internal interface JavaLayerHook
		{
			/// <summary> Retrieves the named resource. This allows resources to be
			/// obtained without specifying how they are retrieved. 
			/// </summary>
			System.IO.Stream getResourceAsStream(System.String name);
		}
}
