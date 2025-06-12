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
using System.Collections.Generic;

namespace Microsoft.Xna.Framework.Graphics
{
    public class EffectTechniqueCollection : IEnumerable<EffectTechnique>
    {
		private readonly EffectTechnique[] _techniques;

        public int Count { get { return _techniques.Length; } }

        internal EffectTechniqueCollection(EffectTechnique[] techniques)
        {
            _techniques = techniques;
        }

        internal EffectTechniqueCollection Clone(Effect effect)
        {
            var techniques = new EffectTechnique[_techniques.Length];
            for (var i = 0; i < _techniques.Length; i++)
                techniques[i] = new EffectTechnique(effect, _techniques[i]);

            return new EffectTechniqueCollection(techniques);
        }
        
        public EffectTechnique this[int index]
        {
            get { return _techniques [index]; }
        }

        public EffectTechnique this[string name]
        {
            get 
            {
                // TODO: Add a name to technique lookup table.
				foreach (var technique in _techniques) 
                {
					if (technique.Name == name)
						return technique;
			    }

			    return null;
		    }
        }

        public IEnumerator<EffectTechnique> GetEnumerator()
        {
            return ((IEnumerable<EffectTechnique>)_techniques).GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _techniques.GetEnumerator();
        }
    }
}
