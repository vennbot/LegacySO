
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
using System.Collections.Generic;
using System.IO;

namespace FSO.Content.Codecs
{
    public static class SmartCodec
    {
        public static Dictionary<string, IGenericContentCodec> CodecForExtension = new Dictionary<string, IGenericContentCodec>
        {
            {".iff", new IffCodec() },
            {".flr", new IffCodec() },
            {".wll", new IffCodec() },
            {".bcf", new BCFCodec() },
            {".skn", new SKNCodec() },
            {".cmx", new CMXCodec() },
            {".bmf", new BMFCodec() },
            {".cfp", new CFPCodec() },
            {".bmp", new TextureCodec() },
            {".tga", new TextureCodec() },
            {".jpg", new TextureCodec() },
            {".wav", new SFXCodec() },
            {".mp3", new SFXCodec() },
            {".xa", new SFXCodec() },
            {".utk", new SFXCodec() }
        };

        public static object Decode(Stream stream, string extension)
        {
            extension = extension.ToLowerInvariant();
            IGenericContentCodec codec = null;
            if (CodecForExtension.TryGetValue(extension, out codec))
            {
                return codec.GenDecode(stream);
            }
            return null;
        }
    }
}
