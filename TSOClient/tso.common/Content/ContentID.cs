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
namespace FSO.Common.Content
{
    /// <summary>
    /// Represents the ID of a content resource.
    /// Consists of two parts: TypeID (uint) and FileID (uint).
    /// </summary>
    public class ContentID
    {
        public uint TypeID;
        public uint FileID;
        public string FileName;
        private long v;


        /// <summary>
        /// Creates a new ContentID instance.
        /// </summary>
        /// <param name="typeID">The TypeID of the content resource.</param>
        /// <param name="fileID">The FileID of the content resource.</param>
        public ContentID(uint typeID, uint fileID)
        {
            this.TypeID = typeID;
            this.FileID = fileID;
        }

        public ContentID(string name)
        {
            this.FileName = name;
        }

        public ContentID(long v)
        {
            this.TypeID = (uint)v;
            this.FileID = (uint)(v >> 32);
        }

        public ulong Shift()
        {
            var fileIDLong = ((ulong)FileID) << 32;
            return fileIDLong | TypeID;
        }
    }
}
