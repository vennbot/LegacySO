
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
using FSO.Files.Formats.IFF.Chunks;
using FSO.SimAntics.Engine;

namespace FSO.SimAntics.JIT.Runtime
{
    public class VMAOTTranslator : VMTranslator
    {
        private AssemblyStore Assemblies;
        public VMAOTTranslator(AssemblyStore assemblies)
        {
            Assemblies = assemblies;
        }

        public override VMRoutine Assemble(BHAV bhav, GameIffResource res)
        {
            VMRoutine routine;
            var assembly = Assemblies.GetModuleFor(bhav.ChunkParent);
            object func = assembly?.GetFunction(bhav.ChunkID);
            if (func != null)
            {
                if (func is IBHAV)
                {
                    routine = new VMAOTRoutine((IBHAV)func);
                }
                else
                {
                    routine = new VMAOTInlineRoutine((IInlineBHAV)func);
                }
            }
            else
            {
                routine = new VMRoutine();
            }
            PopulateRoutineFields(bhav, routine);
            return routine;
        }
    }
}
