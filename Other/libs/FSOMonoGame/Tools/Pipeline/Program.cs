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
// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using Eto;
using Eto.Forms;

namespace MonoGame.Tools.Pipeline
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Styles.Load();

            var app = new Application(Platform.Detect);
            app.Style = "PipelineTool";

            var win = new MainWindow();
            var controller = PipelineController.Create(win);

#if LINUX
            Global.Application.AddWindow(win.ToNative() as Gtk.Window);
#endif

            string project = null;

            if (Global.Unix && !Global.Linux)
                project = Environment.GetEnvironmentVariable("MONOGAME_PIPELINE_PROJECT");
            else if (args != null && args.Length > 0)
                project = string.Join(" ", args);

            if (!string.IsNullOrEmpty(project))
                controller.OpenProject(project);

            app.Run(win);
        }
    }
}
