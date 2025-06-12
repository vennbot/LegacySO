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
namespace Microsoft.Xna.Framework.Graphics
{
	public class EffectTechnique
	{
        public EffectPassCollection Passes { get; private set; }

        public EffectAnnotationCollection Annotations { get; private set; }

        public string Name { get; private set; }

        internal EffectTechnique(Effect effect, EffectTechnique cloneSource)
        {
            // Share all the immutable types.
            Name = cloneSource.Name;
            Annotations = cloneSource.Annotations;

            // Clone the mutable types.
            Passes = cloneSource.Passes.Clone(effect);
        }

        internal EffectTechnique(Effect effect, string name, EffectPassCollection passes, EffectAnnotationCollection annotations)
        {
            Name = name;
            Passes = passes;
            Annotations = annotations;
        }
    }


}

