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
using System.Collections.Generic;

namespace Microsoft.Xna.Framework.Content.Pipeline
{
    /// <summary>
    /// Represents a collection of processor parameters, usually for a single processor. This class is primarily designed for internal use or for custom processor developers.
    /// </summary>
    [SerializableAttribute]
    public sealed class ProcessorParameterCollection : ReadOnlyCollection<ProcessorParameter>
    {
        /// <summary>
        /// Constructs a new ProcessorParameterCollection instance.
        /// </summary>
        /// <param name="parameters">The parameters in the collection.</param>
        internal ProcessorParameterCollection(IEnumerable<ProcessorParameter> parameters)
            : base(new List<ProcessorParameter>(parameters))
        {
        }
    }
}
