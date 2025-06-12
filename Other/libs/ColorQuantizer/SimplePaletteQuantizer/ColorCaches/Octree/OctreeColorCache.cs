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
using System.Drawing;
using System.Linq;
using SimplePaletteQuantizer.ColorCaches.Common;
using SimplePaletteQuantizer.Helpers;

namespace SimplePaletteQuantizer.ColorCaches.Octree
{
    public class OctreeColorCache : BaseColorCache
    {
        #region | Fields |

        private OctreeCacheNode root;

        #endregion

        #region | Properties |

        /// <summary>
        /// Gets a value indicating whether this instance is color model supported.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is color model supported; otherwise, <c>false</c>.
        /// </value>
        public override Boolean IsColorModelSupported
        {
            get { return false; }
        }

        #endregion

        #region | Constructors |

        /// <summary>
        /// Initializes a new instance of the <see cref="OctreeColorCache"/> class.
        /// </summary>
        public OctreeColorCache()
        {
            ColorModel = ColorModel.RedGreenBlue;
            root = new OctreeCacheNode();
        }

        #endregion

        #region << BaseColorCache >>

        /// <summary>
        /// See <see cref="BaseColorCache.Prepare"/> for more details.
        /// </summary>
        public override void Prepare()
        {
            base.Prepare();
            root = new OctreeCacheNode();
        }

        /// <summary>
        /// See <see cref="BaseColorCache.OnCachePalette"/> for more details.
        /// </summary>
        protected override void OnCachePalette(IList<Color> palette)
        {
            Int32 index = 0;

            foreach (Color color in palette)
            {
                root.AddColor(color, index++, 0);
            }
        }

        /// <summary>
        /// See <see cref="BaseColorCache.OnGetColorPaletteIndex"/> for more details.
        /// </summary>
        protected override void OnGetColorPaletteIndex(Color color, out Int32 paletteIndex)
        {
            Dictionary<Int32, Color> candidates = root.GetPaletteIndex(color, 0);

            paletteIndex = 0;
            Int32 index = 0;
            Int32 colorIndex = ColorModelHelper.GetEuclideanDistance(color, ColorModel, candidates.Values.ToList());

            foreach (Int32 colorPaletteIndex in candidates.Keys)
            {
                if (index == colorIndex)
                {
                    paletteIndex = colorPaletteIndex;
                    break;
                }

                index++;
            }
        }

        #endregion
    }
}
