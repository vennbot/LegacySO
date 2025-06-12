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
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using SimsLib.XA;
using SimsLib.UTK;

namespace XaToWav
{
    /// <summary>
    /// This application will automatically convert all *.xa and *.utk files it can find in the directory
    /// it resides in into *.wav files.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            string[] XAFiles = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.xa");
            string[] UTKFiles = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.utk");

            for (int i = 0; i < XAFiles.Length; i++)
            {
                XAFile XA = new XAFile();
                XA.LoadFile(XAFiles[i]);
                XA.DecompressFile();

                BinaryWriter Writer = new BinaryWriter(File.Create(XAFiles[i].Replace(".xa", ".wav")));
                Writer.Write(XA.DecompressedData);
                Writer.Close();
            }
            /*
            UTKFunctions.UTKGenerateTables();

            UTKWrapper UTK = new UTKWrapper();

            unsafe
            {
                for (int i = 0; i < UTKFiles.Length; i++)
                {
                    UTK.LoadUTK(UTKFiles[i]);
                    BinaryWriter Writer = new BinaryWriter(File.Create(UTKFiles[i].Replace(".utk", ".wav")));
                    Writer.Write(UTK.Wav);
                    Writer.Close();
                }
            }
            */
        }
    }
}
