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
using System.IO;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources.Core;

namespace Microsoft.Xna.Framework
{
    partial class TitleContainer
    {
        static internal ResourceContext ResourceContext;
        static internal ResourceMap FileResourceMap;

        static partial void PlatformInit()
        {
            Location = Windows.ApplicationModel.Package.Current.InstalledLocation.Path;

            ResourceContext = new Windows.ApplicationModel.Resources.Core.ResourceContext();
            FileResourceMap = ResourceManager.Current.MainResourceMap.GetSubtree("Files");
        }

        private static async Task<Stream> OpenStreamAsync(string name)
        {
            NamedResource result;

            if (FileResourceMap != null && FileResourceMap.TryGetValue(name, out result))
            {
                var resolved = result.Resolve(ResourceContext);

                try
                {
                    var storageFile = await resolved.GetValueAsFileAsync();
                    var randomAccessStream = await storageFile.OpenReadAsync();
                    return randomAccessStream.AsStreamForRead();
                }
                catch (IOException)
                {
                    // The file must not exist... return a null stream.
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        private static Stream PlatformOpenStream(string safeName)
        {
            return Task.Run(() => OpenStreamAsync(safeName).Result).Result;
        }
    }
}

