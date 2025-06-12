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

using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MonoGame.Framework.Content.Pipeline.Builder
{
    static public class TypeExtensions
    {
        public static Color ToColor(this System.Drawing.Color color)
        {
            return new Color(color.R, color.G, color.B, color.A);
        }

        public static Vector3 ToVector3(this System.Drawing.Color color)
        {
            return new Vector3(color.R / 255.0f, color.G / 255.0f, color.B / 255.0f);
        }

        public static void AddUnique<T>(this List<T> list, T item)
        {
            if (!list.Contains(item))
                list.Add(item);
        }

        public static void AddRangeUnique<T>(this List<T> dstList, List<T> list)
        {
            foreach (var i in list)
            {
                if (!dstList.Contains(i))
                    dstList.Add(i);
            }
        }
    }
}
