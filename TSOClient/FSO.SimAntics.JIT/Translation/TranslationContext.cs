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
using FSO.Content;
using FSO.Files.Formats.IFF;
using FSO.Files.Formats.IFF.Chunks;
using FSO.SimAntics.JIT.Runtime;
using System.Collections.Generic;

namespace FSO.SimAntics.JIT.Translation
{
    public class TranslationContext : IBHAVInfo
    {
        public bool TS1 = true;
        public AbstractTranslationPrimitives Primitives;

        public SimAnticsModule GlobalModule;
        public SimAnticsModule SemiGlobalModule;

        public TranslationContext GlobalContext;
        public TranslationContext SemiGlobalContext;

        public GameIffResource GlobalRes;
        public GameIffResource SemiGlobalRes;
        public GameIffResource ObjectRes;

        public Dictionary<ushort, StructuredBHAV> BHAVInfo = new Dictionary<ushort, StructuredBHAV>();

        public IffFile CurrentFile;
        public BHAV CurrentBHAV;

        public bool BHAVYields(ushort id)
        {
            //in previously compiled context
            StructuredBHAV newBHAV;
            if (BHAVInfo.TryGetValue(id, out newBHAV))
            {
                return newBHAV.Yields;
            }
            else
            {
                return true; //if we don't know if it yields, err on the side of caution.
            }
        }
    }
}
