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
namespace FSO.SimAntics.JIT.Model
{
    public enum ModuleSource
    {

        /// <summary>
        /// Generated from a previous run of the game, perhaps in another system, and precompiled into one assembly.
        /// Assembly should contain modules for most object/global iffs in the game, but not necessarily all.
        /// Modules contain a checksum of the source IFF "execution relevant resources", eg. BHAV and BCON.
        /// If this mismatches the runtime, AOT is ignored and we fallback on interpreter or generating an assembly with JIT.
        /// </summary>
        AOT,

        /// <summary>
        /// Generated while the game is running, perhaps cached from a previous run. Compiled with CSharpCodeProvider.
        /// Only available on platforms with Roslyn and that support JIT. (mobile and .NET core are currently out)
        /// A JIT assembly should only contain one module; for the iff it was produced for.
        /// Always takes priority over an AOT module, as it may include recent changes to an object or global.
        /// </summary>
        JIT,
    }
}
