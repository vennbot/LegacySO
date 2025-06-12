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
using System.Collections.ObjectModel;

namespace Microsoft.Xna.Framework.Content.Pipeline.Graphics
{
    /// <summary>
    /// Provides methods for maintaining a mipmap chain.
    /// </summary>
    public sealed class MipmapChainCollection : Collection<MipmapChain>
    {
        private readonly bool _fixedSize;

        private const string CannotResizeError = "Cannot resize MipmapChainCollection. This type of texture has a fixed number of faces.";

        internal MipmapChainCollection(int count, bool fixedSize)
        {
            for (var i = 0; i < count; i++)
                Add(new MipmapChain());

            _fixedSize = fixedSize;
        }

        protected override void ClearItems()
        {
            if (_fixedSize)
                throw new NotSupportedException(CannotResizeError);

            base.ClearItems();
        }

        protected override void RemoveItem(int index)
        {
            if (_fixedSize)
                throw new NotSupportedException(CannotResizeError);

            base.RemoveItem(index);
        }

        protected override void InsertItem(int index, MipmapChain item)
        {
            if (_fixedSize)
                throw new NotSupportedException(CannotResizeError);

            base.InsertItem(index, item);
        }
    }
}
