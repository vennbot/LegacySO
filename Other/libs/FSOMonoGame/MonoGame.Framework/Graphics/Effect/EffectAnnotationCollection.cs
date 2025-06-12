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
	public class EffectAnnotationCollection : IEnumerable<EffectAnnotation>
	{
        internal static readonly EffectAnnotationCollection Empty = new EffectAnnotationCollection(new EffectAnnotation[0]);

	    private readonly EffectAnnotation[] _annotations;

        internal EffectAnnotationCollection(EffectAnnotation[] annotations)
        {
            _annotations = annotations;
        }

		public int Count 
        {
			get { return _annotations.Length; }
		}
		
		public EffectAnnotation this[int index]
        {
            get { return _annotations[index]; }
        }
		
		public EffectAnnotation this[string name]
        {
            get 
            {
				foreach (var annotation in _annotations) 
                {
					if (annotation.Name == name)
						return annotation;
				}
				return null;
			}
        }
		
		public IEnumerator<EffectAnnotation> GetEnumerator()
        {
            return ((IEnumerable<EffectAnnotation>)_annotations).GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _annotations.GetEnumerator();
        }
	}
}

