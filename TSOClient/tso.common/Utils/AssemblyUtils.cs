
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
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FSO.Common.Utils
{
    public class AssemblyUtils
    {
        public static Assembly Entry;
        public static List<Assembly> GetFreeSOLibs()
        {
            var map = new Dictionary<string, Assembly>();
            if (Entry == null) Entry = Assembly.GetEntryAssembly();
            RecurseAssembly(Entry, map);
            return map.Values.ToList();
        }

        private static void RecurseAssembly(Assembly assembly, Dictionary<string, Assembly> map)
        {
            var refs = assembly.GetReferencedAssemblies();
            foreach (var refAsm in refs)
            {
                if ((refAsm.Name.StartsWith("FSO.") || refAsm.Name.Equals("LegacySO") || refAsm.Name.Equals("server")) && !map.ContainsKey(refAsm.Name))
                {
                    var loadedAssembly = Assembly.Load(refAsm);
                    map.Add(refAsm.Name, loadedAssembly);
                    RecurseAssembly(loadedAssembly, map);
                }
            };
        }
    }
}
