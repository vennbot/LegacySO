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
using FSO.SimAntics.JIT.Translation.CSharp;
using System.Collections.Generic;

namespace FSO.SimAntics.JIT.Roslyn
{
    /// <summary>
    /// The Roslyn SimAntics JIT asynchronously loads or compiles SimAntics routines as C# assemblies.
    /// </summary>
    public class RoslynSimanticsJIT
    {
        public RoslynSimanticsContext Context;
        private Dictionary<string, RoslynSimanticsModule> FormattedNameToRoslynModule = new Dictionary<string, RoslynSimanticsModule>();

        public RoslynSimanticsJIT()
        {
            Context = new RoslynSimanticsContext(this);
            Context.Init();
        }

        public RoslynSimanticsModule GetModuleFor(GameIffResource res)
        {
            var source = res.MainIff;
            var name = CSTranslationContext.FormatName(source.Filename.Substring(0, source.Filename.Length - 4));
            RoslynSimanticsModule result;
            lock (FormattedNameToRoslynModule) {
                if (FormattedNameToRoslynModule.TryGetValue(name, out result))
                {
                    return result;
                } else
                {
                    //try to create one!
                    result = new RoslynSimanticsModule(Context, res);
                    FormattedNameToRoslynModule[name] = result;
                    return result;
                }
            }
        }
    }
}
