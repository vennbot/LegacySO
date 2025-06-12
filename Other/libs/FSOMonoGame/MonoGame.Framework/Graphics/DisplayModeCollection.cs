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
#region License

/*
MIT License
Copyright © 2006 The Mono.Xna Team

All rights reserved.

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

#endregion License

using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Xna.Framework.Graphics
{
    public class DisplayModeCollection : IEnumerable<DisplayMode>
    {
        private readonly List<DisplayMode> _modes;

        public IEnumerable<DisplayMode> this[SurfaceFormat format]
        {
            get 
            {
                var list = new List<DisplayMode>();
                foreach (var mode in _modes)
                {
                    if (mode.Format == format)
                        list.Add(mode);
                }
                return list;
            }
        }

        public IEnumerator<DisplayMode> GetEnumerator()
        {
            return _modes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _modes.GetEnumerator();
        }
        
        internal DisplayModeCollection(List<DisplayMode> modes) 
        {
            // Sort the modes in a consistent way that happens
            // to match XNA behavior on some graphics devices.

            modes.Sort(delegate(DisplayMode a, DisplayMode b)
            {
                if (a == b) 
                    return 0;
                if (a.Format <= b.Format && a.Width <= b.Width && a.Height <= b.Height) 
                    return -1;
                return 1;
            });

            _modes = modes;
        }
    }
}
