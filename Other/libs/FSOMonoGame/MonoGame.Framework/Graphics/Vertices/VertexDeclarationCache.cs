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
namespace Microsoft.Xna.Framework.Graphics
{
    /// <summary>
    /// Helper class which ensures we only lookup a vertex 
    /// declaration for a particular type once.
    /// </summary>
    /// <typeparam name="T">A vertex structure which implements IVertexType.</typeparam>
    internal class VertexDeclarationCache<T>
        where T : struct, IVertexType
    {
        static private VertexDeclaration _cached;

        static public VertexDeclaration VertexDeclaration
        {
            get
            {
                if (_cached == null)
                    _cached = VertexDeclaration.FromType(typeof(T));

                return _cached;
            }
        }
    }
}
