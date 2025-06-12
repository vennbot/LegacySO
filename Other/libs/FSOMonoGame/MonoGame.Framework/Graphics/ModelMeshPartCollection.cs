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

	public sealed class ModelMeshPartCollection : ReadOnlyCollection<ModelMeshPart>
	{
		public ModelMeshPartCollection(IList<ModelMeshPart> list)
			: base(list)
		{

		}
	}

	//// Summary:
	////     Represents a collection of ModelMeshPart objects.
	//public sealed class ModelMeshPartCollection : ReadOnlyCollection<ModelMeshPart>
	//{
	//    internal ModelMeshPartCollection()
	//        : base(new List<ModelMeshPart>())
	//    {
	//    }

	//    // Summary:
	//    //     Returns a ModelMeshPartCollection.Enumerator that can iterate through a ModelMeshPartCollection.
	//    public ModelMeshPartCollection.Enumerator GetEnumerator() { throw new NotImplementedException(); }

	//    // Summary:
	//    //     Provides the ability to iterate through the bones in an ModelMeshPartCollection.
	//    public struct Enumerator : IEnumerator<ModelMeshPart>, IDisposable, IEnumerator
	//    {

	//        // Summary:
	//        //     Gets the current element in the ModelMeshPartCollection.
	//        public ModelMeshPart Current { get { throw new NotImplementedException(); } }

	//        // Summary:
	//        //     Immediately releases the unmanaged resources used by this object.
	//        public void Dispose() { throw new NotImplementedException(); }
	//        //
	//        // Summary:
	//        //     Advances the enumerator to the next element of the ModelMeshPartCollection.
	//        public bool MoveNext() { throw new NotImplementedException(); }

	//        #region IEnumerator Members

	//        object IEnumerator.Current
	//        {
	//            get { throw new NotImplementedException(); }
	//        }

	//        public void Reset()
	//        {
	//            throw new NotImplementedException();
	//        }

	//        #endregion
	//    }
	//}
}
