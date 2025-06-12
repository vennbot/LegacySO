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
using Microsoft.Xna.Framework.Content.Pipeline;
using System;
using System.Collections.Generic;

namespace MonoGame.Tests.ContentPipeline
{
    class TestImporterContext : ContentImporterContext
    {
        readonly string _intermediateDirectory;
        readonly string _outputDirectory;
        readonly TestContentBuildLogger _logger;
        List<string> _dependencies;

        public TestImporterContext(string intermediateDirectory, string outputDirectory)
        {
            _intermediateDirectory = intermediateDirectory;
            _outputDirectory = outputDirectory;
            _logger = new TestContentBuildLogger();
            _dependencies = new List<string>();
        }

        public List<string> Dependencies
        {
            get { return _dependencies; }
        }

        public override string IntermediateDirectory
        {
            get { return _intermediateDirectory; }
        }

        public override ContentBuildLogger Logger
        {
            get { return _logger; }
        }

        public override string OutputDirectory
        {
            get { return _outputDirectory; }
        }

        public override void AddDependency(string filename)
        {
            _dependencies.Add(filename);
        }
    }
}
