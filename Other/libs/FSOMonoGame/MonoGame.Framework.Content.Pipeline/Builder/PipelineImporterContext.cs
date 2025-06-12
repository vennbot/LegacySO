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

using Microsoft.Xna.Framework.Content.Pipeline;

namespace MonoGame.Framework.Content.Pipeline.Builder
{
    public class PipelineImporterContext : ContentImporterContext
    {
        private readonly PipelineManager _manager;

        public PipelineImporterContext(PipelineManager manager)
        {
            _manager = manager;
        }

        public override string IntermediateDirectory { get { return _manager.IntermediateDirectory; } }
        public override string OutputDirectory { get { return _manager.OutputDirectory; } }
        public override ContentBuildLogger Logger { get { return _manager.Logger; } }

        public override void AddDependency(string filename)
        {            
        }
    }
}
