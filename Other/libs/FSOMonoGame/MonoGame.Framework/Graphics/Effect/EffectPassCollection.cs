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

namespace Microsoft.Xna.Framework.Graphics
{
    public class EffectPassCollection : IEnumerable<EffectPass>
    {
		private readonly EffectPass[] _passes;

        internal EffectPassCollection(EffectPass [] passes)
        {
            _passes = passes;
        }

        internal EffectPassCollection Clone(Effect effect)
        {
            var passes = new EffectPass[_passes.Length];
            for (var i = 0; i < _passes.Length; i++)
                passes[i] = new EffectPass(effect, _passes[i]);

            return new EffectPassCollection(passes);
        }

        public EffectPass this[int index]
        {
            get { return _passes[index]; }
        }

        public EffectPass this[string name]
        {
            get 
            {
                // TODO: Add a name to pass lookup table.
				foreach (var pass in _passes) 
                {
					if (pass.Name == name)
						return pass;
				}
				return null;
		    }
        }

        public int Count
        {
            get { return _passes.Length; }
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator(_passes);
        }
            
        IEnumerator<EffectPass> IEnumerable<EffectPass>.GetEnumerator()
        {
            return ((IEnumerable<EffectPass>)_passes).GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _passes.GetEnumerator();
        }

        public struct Enumerator : IEnumerator<EffectPass>
        {
            private readonly EffectPass[] _array;
            private int _index;
            private EffectPass _current;

            internal Enumerator(EffectPass[] array)
            {
                _array = array;
                _index = 0;
                _current = null;
            }

            public bool MoveNext()
            {
                if (_index < _array.Length)
                {
                    _current = _array[_index];
                    _index++;
                    return true;
                }
                _index = _array.Length + 1;
                _current = null;
                return false;
            }

            public EffectPass Current
            {
                get { return _current; }
            }

            public void Dispose()
            {

            }

            object System.Collections.IEnumerator.Current
            {
                get
                {
                    if (_index == _array.Length + 1)
                        throw new InvalidOperationException();
                    return Current;
                }
            }

            void System.Collections.IEnumerator.Reset()
            {
                _index = 0;
                _current = null;
            }
        }
    }
}
