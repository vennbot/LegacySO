
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

namespace FSO.Files.Formats.IFF
{
    public class IffRuntimeInfo
    {
        public IffRuntimeState State;
        public IffUseCase UseCase;
        public string Filename;
        public string Path;
        public bool Dirty;
        public List<IffFile> Patches = new List<IffFile>();
    }

    public enum IffRuntimeState
    {
        ReadOnly, //orignal game iff
        PIFFPatch, //replacement patch
        PIFFClone, //clone of original object
        Standalone //standalone, mutable iff
    }

    public enum IffUseCase
    {
        Global,
        Object,
        ObjectSprites,
    }
}
