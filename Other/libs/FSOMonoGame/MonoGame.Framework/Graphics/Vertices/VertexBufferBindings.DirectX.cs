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


namespace Microsoft.Xna.Framework.Graphics
{
    partial class VertexBufferBindings
    {
        /// <summary>
        /// Creates an <see cref="ImmutableVertexInputLayout"/> that can be used as a key in the
        /// <see cref="InputLayoutCache"/>.
        /// </summary>
        /// <returns>The <see cref="ImmutableVertexInputLayout"/>.</returns>
        public ImmutableVertexInputLayout ToImmutable()
        {
            int count = Count;

            var vertexDeclarations = new VertexDeclaration[count];
            Array.Copy(VertexDeclarations, vertexDeclarations, count);

            var instanceFrequencies = new int[count];
            Array.Copy(InstanceFrequencies, instanceFrequencies, count);

            return new ImmutableVertexInputLayout(vertexDeclarations, instanceFrequencies);
        }
    }
}
