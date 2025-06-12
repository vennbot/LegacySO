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
using MonoDevelop.Ide.Templates;
using System.Xml;
using MonoDevelop.Core;
using MonoDevelop.Projects;

namespace MonoDevelop.MonoGame
{
	public class ContentItemTemplate : FileDescriptionTemplate
	{
		SingleFileDescriptionTemplate template;
		FileCopyMode mode = FileCopyMode.None;

		public override string Name { 
			get { return template.Name; } 
		}

		public override void Load (XmlElement filenode, FilePath baseDirectory)
		{
			foreach (XmlNode node in filenode.ChildNodes) {
				if (node is XmlElement) {
					template = CreateTemplate ((XmlElement) node, baseDirectory) as SingleFileDescriptionTemplate;
					if (template == null)
						throw new InvalidOperationException ("Resource templates must contain single-file templates.");
					var attr = node.Attributes ["CopyToOutputDirectory"];
					if (attr != null) {
						Enum.TryParse<FileCopyMode> ( attr.Value, out mode);
					}
					return;
				}
			}
		}

		public override bool AddToProject(SolutionFolderItem policyParent, Project project, string language, string directory, string name)
		{
			ProjectFile file = template.AddFileToProject(policyParent, project, language, directory, name);
			if (file != null)
			{
				file.BuildAction = BuildAction.Content;
				file.CopyToOutputDirectory = mode;
				return true;
			}
			else
				return false;
		}

		public override void Show ()
		{
			template.Show ();
		}
	}
}

