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
// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using Eto.Drawing;
using Eto.Wpf.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Media.Imaging;

namespace MonoGame.Tools.Pipeline
{
    static partial class Global
    {
        [DllImport("Shell32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        private static extern int ExtractIconExW(string sFile, int iIndex, out IntPtr piLargeVersion, out IntPtr piSmallVersion, int amountIcons);

        private static void PlatformInit()
        {
            var file = ExtractIcon(0).ToBitmap();
            var fileMissing = ExtractIcon(271).ToBitmap();
            var folder = ExtractIcon(4).ToBitmap();
            var folderMissing = ExtractIcon(234).ToBitmap();

            _files["."] = ToEtoImage(file);
            _fileMissing = ToEtoImage(fileMissing);
            _folder = ToEtoImage(folder);
            _folderMissing = ToEtoImage(folderMissing);
        }

        public static System.Drawing.Icon ExtractIcon(int number)
        {
            IntPtr large;
            IntPtr small;
            ExtractIconExW("shell32.dll", number, out large, out small, 1);

            return System.Drawing.Icon.FromHandle(large);
        }

        private static System.Drawing.Bitmap PlatformGetFileIcon(string path)
        {
            return System.Drawing.Icon.ExtractAssociatedIcon(path).ToBitmap();
        }

        private static Bitmap ToEtoImage(System.Drawing.Bitmap bitmap)
        {
            var ret = new BitmapImage();

            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                stream.Position = 0;

                ret.BeginInit();
                ret.DecodePixelHeight = 16;
                ret.DecodePixelWidth = 16;
                ret.StreamSource = stream;
                ret.CacheOption = BitmapCacheOption.OnLoad;
                ret.EndInit();
            }

            return new Bitmap(new BitmapHandler(ret));
        }
    }
}

