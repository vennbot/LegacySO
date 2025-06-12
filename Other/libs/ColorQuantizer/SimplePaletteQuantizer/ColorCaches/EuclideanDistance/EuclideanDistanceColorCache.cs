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
using System.Drawing;
using System.Collections.Generic;
using SimplePaletteQuantizer.Helpers;
using SimplePaletteQuantizer.ColorCaches.Common;

namespace SimplePaletteQuantizer.ColorCaches.EuclideanDistance
{
    public class EuclideanDistanceColorCache : BaseColorCache
    {
        #region | Fields |

        private IList<Color> palette;

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
            get { return true; }
        }

        #endregion

        #region | Constructors |

        /// <summary>
        /// Initializes a new instance of the <see cref="EuclideanDistanceColorCache"/> class.
        /// </summary>
        public EuclideanDistanceColorCache()
        {
            ColorModel = ColorModel.RedGreenBlue;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EuclideanDistanceColorCache"/> class.
        /// </summary>
        /// <param name="colorModel">The color model.</param>
        public EuclideanDistanceColorCache(ColorModel colorModel)
        {
            ColorModel = colorModel;
        }

        #endregion

        #region << BaseColorCache >>

        /// <summary>
        /// See <see cref="BaseColorCache.OnCachePalette"/> for more details.
        /// </summary>
        protected override void OnCachePalette(IList<Color> palette)
        {
            this.palette = palette;
        }

        /// <summary>
        /// See <see cref="BaseColorCache.OnGetColorPaletteIndex"/> for more details.
        /// </summary>
        protected override void OnGetColorPaletteIndex(Color color, out Int32 paletteIndex)
        {
            paletteIndex = ColorModelHelper.GetEuclideanDistance(color, ColorModel, palette);
        }

        #endregion
    }
}
