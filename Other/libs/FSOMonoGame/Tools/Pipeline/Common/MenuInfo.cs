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

namespace MonoGame.Tools.Pipeline
{
    public class MenuInfo
    {
        public bool New { get; set; }

        public bool Open { get; set; }

        public bool Close { get; set; }

        public bool Import { get; set; }

        public bool Save { get; set; }

        public bool SaveAs { get; set; }

        public bool Exit { get; set; }

        public bool Undo { get; set; }

        public bool Redo { get; set; }

        public bool Add { get; set; }

        public bool Exclude { get; set; }

        public bool Rename { get; set; }

        public bool Delete { get; set; }

        public bool BuildMenu { get; set; }

        public bool Build { get; set; }

        public bool Rebuild { get; set; }

        public bool Clean { get; set; }

        public bool Cancel { get; set; }

        public bool Debug { get; set; }

        public bool OpenItem { get; set; }

        public bool OpenItemWith { get; set; }

        public bool OpenItemLocation { get; set; }

        public bool OpenOutputItemLocation { get; set; }

        public bool CopyAssetPath { get; set; }

        public bool RebuildItem { get; set; }
    }
}
