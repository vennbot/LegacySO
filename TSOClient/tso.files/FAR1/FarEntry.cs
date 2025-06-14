
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
namespace FSO.Files.FAR1
{
    /// <summary>
    /// Represents an entry in a FAR1 archive.
    /// </summary>
    public class FarEntry
    {
        //Decompressed data size - A 4-byte unsigned integer specifying the uncompressed size of the file.
        public int DataLength;
        //A 4-byte unsigned integer specifying the compressed size of the file; if this and the previous field are the same, 
        //the file is considered uncompressed. (It is the responsibility of the archiver to only store data compressed when 
        //its size is less than the size of the original data.) Note that The Sims 1 does not actually support any form 
        //of compression.
        public int DataLength2;
        //A 4-byte unsigned integer specifying the offset of the file from the beginning of the archive.
        public int DataOffset;
        //A 4-byte unsigned integer specifying the length of the filename field that follows.
        public short FilenameLength;
        //Filename - The name of the archived file; size depends on the previous field.
        public string Filename;
    }
}
