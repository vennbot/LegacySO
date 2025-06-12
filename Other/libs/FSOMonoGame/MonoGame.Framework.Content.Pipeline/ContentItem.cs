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

namespace Microsoft.Xna.Framework.Content.Pipeline
{
    /// <summary>
    /// Provides properties that define various aspects of content stored using the intermediate file format of the XNA Framework.
    /// </summary>
    public class ContentItem
    {
        OpaqueDataDictionary opaqueData = new OpaqueDataDictionary();

        /// <summary>
        /// Gets or sets the identity of the content item.
        /// </summary>
        [ContentSerializer(Optional = true)]
        public ContentIdentity Identity { get; set; }

        /// <summary>
        /// Gets or sets the name of the content item.
        /// </summary>
        [ContentSerializer(Optional = true)]
        public string Name { get; set; }

        /// <summary>
        /// Gets the opaque data of the content item.
        /// </summary>
        [ContentSerializer(Optional = true)]
        public OpaqueDataDictionary OpaqueData { get { return opaqueData; } }

        /// <summary>
        /// Initializes a new instance of ContentItem.
        /// </summary>
        public ContentItem()
        {
        }
    }
}
