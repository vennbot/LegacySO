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
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.Xna.Framework.Graphics
{
	// Summary:
	//     Represents a collection of effects associated with a model.
	public sealed class ModelEffectCollection : ReadOnlyCollection<Effect>
	{
		internal ModelEffectCollection(IList<Effect> list)
			: base(list)
		{

		}

	    internal ModelEffectCollection() : base(new List<Effect>())
	    {
	    }
		
		//ModelMeshPart needs to be able to add to ModelMesh's effects list
		internal void Add(Effect item)
		{
			Items.Add (item);
		}
		internal void Remove(Effect item)
		{
			Items.Remove (item);
		}

	    // Summary:
	    //     Returns a ModelEffectCollection.Enumerator that can iterate through a ModelEffectCollection.
	    public new ModelEffectCollection.Enumerator GetEnumerator()
		{
			return new ModelEffectCollection.Enumerator((List<Effect>)Items);
		}

	    // Summary:
	    //     Provides the ability to iterate through the bones in an ModelEffectCollection.
	    public struct Enumerator : IEnumerator<Effect>, IDisposable, IEnumerator
	    {
			List<Effect>.Enumerator enumerator;
            bool disposed;

			internal Enumerator(List<Effect> list)
			{
				enumerator = list.GetEnumerator();
                disposed = false;
			}

	        // Summary:
	        //     Gets the current element in the ModelEffectCollection.
	        public Effect Current { get { return enumerator.Current; } }

	        // Summary:
	        //     Immediately releases the unmanaged resources used by this object.
	        public void Dispose()
            {
                if (!disposed)
                {
                    enumerator.Dispose();
                    disposed = true;
                }
            }
	        //
	        // Summary:
	        //     Advances the enumerator to the next element of the ModelEffectCollection.
	        public bool MoveNext() { return enumerator.MoveNext(); }

	        #region IEnumerator Members

	        object IEnumerator.Current
	        {
	            get { return Current; }
	        }

	        void IEnumerator.Reset()
	        {
				IEnumerator resetEnumerator = enumerator;
				resetEnumerator.Reset ();
				enumerator = (List<Effect>.Enumerator)resetEnumerator;
	        }

	        #endregion
	    }
	}
}
