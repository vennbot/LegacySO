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
//from https://gist.github.com/Vaikesh/471eb223d0a5ee37944a, for simplicity

using System;
using Java.IO;
using Java.Util.Zip;
using System.IO;

namespace ZipManager
{
    public class Decompress
    {
        String _zipFile;
        String _location;

        public event Action<string> OnContinue;

        public Decompress(String zipFile, String location)
        {
            _zipFile = zipFile;
            _location = location;
            DirChecker("");
        }

        void DirChecker(String dir)
        {
            Directory.CreateDirectory(_location + dir);
        }

        public void UnZip()
        {
            byte[] buffer = new byte[65536];
            var fileInputStream = System.IO.File.OpenRead(_zipFile);
            
            var zipInputStream = new ZipInputStream(fileInputStream);
            ZipEntry zipEntry = null;
            int j = 0;
            int bestRead = 0;
            while ((zipEntry = zipInputStream.NextEntry) != null)
            {
                OnContinue?.Invoke(zipEntry.Name + ", " + bestRead);
                if (zipEntry.IsDirectory)
                {
                    DirChecker(zipEntry.Name);
                }
                else
                {
                    if (System.IO.File.Exists(_location + zipEntry.Name)) System.IO.File.Delete(_location + zipEntry.Name);
                    var foS = new FileOutputStream(_location + zipEntry.Name, true);
                    int read;
                    while ((read = zipInputStream.Read(buffer)) > 0)
                    {
                        if (read > bestRead) bestRead = read;
                        foS.Write(buffer, 0, read);
                    }
                    foS.Close();
                    zipInputStream.CloseEntry();
                }
            }
            zipInputStream.Close();
            fileInputStream.Close();
        }

    }
}
