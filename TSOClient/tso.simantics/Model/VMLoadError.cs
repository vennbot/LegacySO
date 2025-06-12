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
namespace FSO.SimAntics.Model
{
    public class VMLoadError
    {
        public VMLoadErrorCode Code;
        public ushort SubjectID; //object id (not guid)
        public string SubjectName; //object GUID, wall filename, floor filename


        public VMLoadError(VMLoadErrorCode code, string subject)
        {
            Code = code;
            SubjectName = subject;
        }

        public VMLoadError(VMLoadErrorCode code, string subject, ushort subjectID) : this(code, subject)
        {
            SubjectID = subjectID;
        }

        public override string ToString()
        {
            if (SubjectID != 0) return Code.ToString() + ": " + SubjectName + " on item with ID " + SubjectID;
            else return Code.ToString() + ": " + SubjectName;
        }
    }

    public enum VMLoadErrorCode
    {
        MISSING_OBJECT,
        MISSING_WALL,
        MISSING_FLOOR,
        MISSING_ROOF,
        MISSING_ANIM,
        MISSING_MESH,
        MISSING_TEXTURE,
        INVALID_SCRIPT_STATE,
        UPGRADE,
        FATAL,

        UNKNOWN_ERROR
    }
}
