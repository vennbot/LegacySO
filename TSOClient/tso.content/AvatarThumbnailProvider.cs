
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
using FSO.Content.Framework;
using FSO.Content.Codecs;
using System.Text.RegularExpressions;
using FSO.Content.Model;

namespace FSO.Content
{
    /// <summary>
    /// Provides access to avatar thumbnail data in FAR3 archives.
    /// </summary>
    public class AvatarThumbnailProvider : TSOAvatarContentProvider<ITextureRef>
    {
        public AvatarThumbnailProvider(Content contentManager) : base(contentManager, new TextureCodec(),
            new Regex(".*/thumbnails/.*\\.dat"),
            new Regex("Avatar/Thumbnails/.*"))
        {
        }
    }
}
