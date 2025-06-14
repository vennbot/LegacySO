
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
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace FSO.SimAntics.JIT.Roslyn
{
    public class RoslynSimanticsContext
    {
        public string CacheDirectory = "Content/JITCache/";
        public bool Debug = false;
        private Task<RoslynSimanticsModule> Globals;
        private Dictionary<string, Task<RoslynSimanticsModule>> Semiglobals;

        private Task<RoslynSimanticsModule> GlobalLoadTask;
        private RoslynSimanticsJIT Parent;

        public RoslynSimanticsContext(RoslynSimanticsJIT parent)
        {
            Parent = parent;
        }

        private async Task<RoslynSimanticsModule> LoadModule(GameIffResource res)
        {
            var module = Parent.GetModuleFor(res);
            await module.GetModuleAsync();
            return module;
        }

        public void Init()
        {
            Directory.CreateDirectory(CacheDirectory);
        }

        public Task<RoslynSimanticsModule> GetGlobal()
        {
            if (Globals != null) return Globals;
            lock (this)
            {
                if (Globals != null) return Globals;
                Globals = LoadModule(Content.Content.Get().WorldObjectGlobals.Get("global").Resource);
                return Globals;
            }
        }

        public Task<RoslynSimanticsModule> GetSemiglobal(GameGlobalResource res)
        {
            lock (Semiglobals)
            {
                Task<RoslynSimanticsModule> result;
                if (!Semiglobals.TryGetValue(res.MainIff.Filename, out result))
                {
                    // need to load it.
                    Semiglobals[res.MainIff.Filename] = LoadModule(res);
                }
                return result;
            }
        }
    }
}
