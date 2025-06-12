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
using System.IO;
using System.Diagnostics;
using MonoDevelop.Ide.Gui;

namespace MonoGame.IDE.VisualStudioForMac {
	public class PipelineDisplayBinding : IExternalDisplayBinding {
		#region IExternalDisplayBinding implementation
		public MonoDevelop.Ide.Desktop.DesktopApplication GetApplication (MonoDevelop.Core.FilePath fileName, string mimeType, MonoDevelop.Projects.Project ownerProject)
		{
			return new PipelineDesktopApplication (fileName.FullPath, ownerProject);
		}
		#endregion
		#region IDisplayBinding implementation
		public bool CanHandle (MonoDevelop.Core.FilePath fileName, string mimeType, MonoDevelop.Projects.Project ownerProject)
		{
			return mimeType == "text/x-mgcb";
		}
		public bool CanUseAsDefault {
			get {
				return true;
			}
		}
		#endregion

	}

	class PipelineDesktopApplication : MonoDevelop.Ide.Desktop.DesktopApplication {
		MonoDevelop.Projects.Project project;
		string filename;

		public PipelineDesktopApplication (string filename, MonoDevelop.Projects.Project ownerProject)
			: base ("MonoGamePipelineTool", "MonoGame Pipeline Tool", true)
		{
			this.project = ownerProject;
			this.filename = filename;
		}

		public override void Launch (params string [] files)
		{
			var process = new Process ();
			if (Environment.OSVersion.Platform == PlatformID.Win32NT) {
				process.StartInfo.FileName = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.ProgramFilesX86), @"MSBuild\MonoGame\v3.0\Tools", "Pipeline.exe");
				process.StartInfo.Arguments = string.Format ("\"{0}\"", filename);
			} else {
				if (Directory.Exists ("/Applications/Pipeline.app")) {
					process.StartInfo.FileName = "open";
					process.StartInfo.EnvironmentVariables.Add ("MONOGAME_PIPELINE_PROJECT", Path.GetFullPath (filename));
					process.StartInfo.Arguments = string.Format ("-b com.monogame.pipeline --args \"{0}\"", Path.GetFullPath (filename));
				} else {
					// figure out linix 
					process.StartInfo.FileName = "monogame-pipeline-tool";
					process.StartInfo.Arguments = string.Format ("\"{0}\"", filename);
				}
			}
			process.StartInfo.CreateNoWindow = true;
			process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
			process.StartInfo.UseShellExecute = false;

			// Fire off the process.
			process.Start ();
		}
	}
}
