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
using System.Collections.Generic;
using System.IO;
using System.Resources;

namespace Microsoft.Xna.Framework.Content
{
    public class ResourceContentManager : ContentManager
    {
        private ResourceManager resource;

        public ResourceContentManager(IServiceProvider servicesProvider, ResourceManager resource)
            : base(servicesProvider)
        {
            if (resource == null)
            {
                throw new ArgumentNullException("resource");
            }
            this.resource = resource;
        }

        protected override System.IO.Stream OpenStream(string assetName)
        {
            object obj = this.resource.GetObject(assetName);
            if (obj == null)
            {
                throw new ContentLoadException("Resource not found");
            }
            if (!(obj is byte[]))
            {
                throw new ContentLoadException("Resource is not in binary format");
            }
            return new MemoryStream(obj as byte[]);
        }
    }
}
