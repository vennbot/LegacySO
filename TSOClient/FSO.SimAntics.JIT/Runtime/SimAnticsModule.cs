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
using FSO.SimAntics.JIT.Model;
using System.Collections.Generic;

namespace FSO.SimAntics.JIT.Runtime
{
    public abstract class SimAnticsModule
    {
        //helpers for dynamically finding functions. Statically linked functions work without initing,
        //so no need to init globals and semiglobals for an object instantly. (unless we need to JIT)

        public Dictionary<ushort, object> All = new Dictionary<ushort, object>();
        public Dictionary<ushort, IBHAV> Yielders = new Dictionary<ushort, IBHAV>();
        public Dictionary<ushort, IInlineBHAV> Inline = new Dictionary<ushort, IInlineBHAV>();

        public virtual uint SourceHash => 0;
        public virtual uint SourceSemiglobalHash => 0;
        public virtual uint SourceGlobalHash => 0;

        public virtual uint JITVersion => 0;

        public ModuleSource Source;
        public bool Inited;

        public void Init()
        {
            Inited = true;
            var fields = this.GetType().GetFields();
            foreach (var field in fields)
            {
                var type = field.FieldType;
                if (type == typeof(IBHAV))
                {
                    var id = ushort.Parse(field.Name.Substring(field.Name.LastIndexOf('_') + 1));
                    Yielders.Add(id, (IBHAV)field.GetValue(null));
                }
                else if (type == typeof(IInlineBHAV))
                {
                    var id = ushort.Parse(field.Name.Substring(field.Name.LastIndexOf('_') + 1));
                    var bhav = (IInlineBHAV)field.GetValue(null);
                    Inline.Add(id, bhav);
                }
            }

            foreach (var func in Yielders) All.Add(func.Key, func.Value);
            foreach (var func in Inline) All.Add(func.Key, func.Value);
        }

        public object GetFunction(ushort id)
        {
            object result;
            if (!All.TryGetValue(id, out result)) return null;
            return result;
        }

        public bool? FunctionYields(ushort id)
        {
            var func = GetFunction(id);
            if (func == null) return null;
            return func is IBHAV;
        }
    }
}
