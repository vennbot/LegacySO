
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
using FSO.Files.Formats.IFF.Chunks;
using FSO.SimAntics.JIT.Translation.Model;
using FSO.SimAntics.JIT.Translation.Primitives;
using System;
using System.Collections.Generic;

namespace FSO.SimAntics.JIT.Translation
{
    public class AbstractTranslationPrimitives
    {
        protected Dictionary<SharedPrimitives, Type> Map = new Dictionary<SharedPrimitives, Type>();

        protected virtual AbstractTranslationPrimitive GetFallbackPrim(BHAVInstruction instruction, byte index)
        {
            return new AbstractTranslationPrimitive(instruction, index);
        }

        public AbstractTranslationPrimitive GetPrimitive(BHAVInstruction instruction, byte index)
        {
            Type primType;
            var prim = (SharedPrimitives)Math.Min((ushort)255, instruction.Opcode);

            if (!Map.TryGetValue(prim, out primType))
                return GetFallbackPrim(instruction, index);
            else
                return (AbstractTranslationPrimitive)Activator.CreateInstance(primType, new object[] { instruction, index });
        }
    }
}
