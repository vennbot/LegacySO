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
using FSO.Files.Formats.IFF;
using FSO.SimAntics.JIT.Translation.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FSO.SimAntics.JIT.Runtime
{
    public class AssemblyStore
    {
        private Dictionary<string, SimAnticsModule> FormattedNameToModule = new Dictionary<string, SimAnticsModule>();

        public void InitAOT()
        {
            var modules = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(t => t.GetTypes())
                .Where(t => t.IsClass && t.IsSubclassOf(typeof(SimAnticsModule)));

            foreach (var module in modules)
            {
                var iffName = module.Namespace.Substring(module.Namespace.LastIndexOf('.') + 1);
                var inst = (SimAnticsModule)Activator.CreateInstance(module);
                inst.Source = Model.ModuleSource.AOT;
                FormattedNameToModule[iffName] = inst;
            }

            Console.WriteLine(FormattedNameToModule.Count + " Modules Loaded.");
        }

        public SimAnticsModule GetModuleFor(IffFile source)
        {
            if (source.CachedJITModule != null) return (SimAnticsModule)source.CachedJITModule;
            var name = CSTranslationContext.FormatName(source.Filename.Substring(0, source.Filename.Length-4));
            SimAnticsModule result;
            if (FormattedNameToModule.TryGetValue(name, out result))
            {
                //TODO: checksum
                if (!result.Inited) result.Init();
                source.CachedJITModule = result;
                return result;
            }
            return null;
        }
    }
}
