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

namespace Microsoft.Xna.Framework.Content
{
	internal class CurveReader : ContentTypeReader<Curve>
	{
		protected internal override Curve Read(ContentReader input, Curve existingInstance)
		{
			Curve curve = existingInstance;
			if (curve == null)
			{
				curve = new Curve();
			}         
			
			curve.PreLoop = (CurveLoopType)input.ReadInt32();
			curve.PostLoop = (CurveLoopType)input.ReadInt32();
			int num6 = input.ReadInt32();
			
			for (int i = 0; i < num6; i++)
			{
				float position = input.ReadSingle();
				float num4 = input.ReadSingle();
				float tangentIn = input.ReadSingle();
				float tangentOut = input.ReadSingle();
				CurveContinuity continuity = (CurveContinuity)input.ReadInt32();
				curve.Keys.Add(new CurveKey(position, num4, tangentIn, tangentOut, continuity));
			}		
			return curve;         
		}
	}
}

