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
using System.Globalization;

namespace Microsoft.Xna.Framework.GamerServices
{
	[Serializable()]
	internal class MonoGameLocalGamerProfile
	{
		Guid playerGuid;
		
		internal MonoGameLocalGamerProfile ()
		{
			playerGuid = Guid.NewGuid();
		}

		#region Properties
		
		internal Guid PlayerInternalIdentifier
		{
			get { return playerGuid; }
			set { playerGuid = value; }
		}
		
		internal string DisplayName {
			get;
			set;
		}

		internal string Gamertag {
			get;
			set;
		}


		internal byte[] GamerPicture {
			get;
			set;
		}
		
		internal int GamerScore {
			get;
			set;
		}

		internal GamerZone GamerZone {
			get;
			set;
		}

		internal string Motto {
			get;
			set;
		}

		internal RegionInfo Region {
			get;
			set;
		}

		internal float Reputation {
			get;
			set;
		}

		internal int TitlesPlayed {
			get;
			set;
		}

		internal int TotalAchievements {
			get;
			set;
		}		
		#endregion
	}
}

