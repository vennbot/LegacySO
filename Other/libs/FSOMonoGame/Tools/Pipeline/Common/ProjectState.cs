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

using System.Collections.Generic;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Tools.Pipeline
{
    /// <summary>
    /// Snapshot of a PipelineProject's state, used for undo/redo.
    /// </summary>
    internal class ProjectState
    {
        public string OutputDir;
        public string IntermediateDir;
        public List<string> References;
        public TargetPlatform Platform;
        public GraphicsProfile Profile;
        public string Config;
        public string OriginalPath;

        /// <summary>
        /// Create a ProjectState storing member values of the passed PipelineProject.
        /// </summary>        
        public static ProjectState Get(PipelineProject proj)
        {
            var state = new ProjectState()
                {
                    OriginalPath = proj.OriginalPath,
                    OutputDir = proj.OutputDir,
                    IntermediateDir = proj.IntermediateDir,
                    References = new List<string>(proj.References),
                    Platform = proj.Platform,
                    Profile = proj.Profile,
                    Config = proj.Config,        
                };

            return state;
        }

        /// <summary>
        /// Set a PipelineProject's member values from this state object.
        /// </summary>
        public void Apply(PipelineProject proj)
        {
            proj.OutputDir = OutputDir;
            proj.IntermediateDir = IntermediateDir;
            proj.References = new List<string>(References);
            proj.Platform = Platform;
            proj.Profile = Profile;
            proj.Config = Config;
        }
    }    
}
