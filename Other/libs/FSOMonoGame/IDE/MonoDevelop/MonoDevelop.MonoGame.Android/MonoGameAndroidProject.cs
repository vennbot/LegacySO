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

namespace MonoDevelop.MonoGame.Android
{
	public class MonoGameAndroidProject : MonoDevelop.MonoDroid.MonoDroidProject
	{
		public MonoGameAndroidProject () : base()
		{
		}

		public override MonoDevelop.Projects.SolutionItemConfiguration CreateConfiguration (string name)
		{
			return base.CreateConfiguration (name);
		}

		protected override void PopulateSupportFileList (MonoDevelop.Projects.FileCopySet list, MonoDevelop.Projects.ConfigurationSelector configuration)
		{
			base.PopulateSupportFileList (list, configuration);
		}

		[Obsolete]
		public override string ProjectType {
			get {
				return "MonoGameAndroid";
			}
		}
	}
}

