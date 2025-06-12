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

namespace Mina.Core.Filterchain
{
    /// <summary>
    /// Represents a name-filter pair that an <see cref="IChain&lt;TFilter, TNextFilter&gt;"/> contains.
    /// </summary>
    public interface IEntry<TFilter, TNextFilter>
    {
        /// <summary>
        /// Gets the name of the filter.
        /// </summary>
        String Name { get; }
        /// <summary>
        /// Gets the filter.
        /// </summary>
        TFilter Filter { get; }
        /// <summary>
        /// Gets the <typeparamref name="TNextFilter"/> of the filter.
        /// </summary>
        TNextFilter NextFilter { get; }
        /// <summary>
        /// Adds the specified filter with the specified name just before this entry.
        /// </summary>
        void AddBefore(String name, TFilter filter);
        /// <summary>
        /// Adds the specified filter with the specified name just after this entry.
        /// </summary>
        void AddAfter(String name, TFilter filter);
        /// <summary>
        /// Replace the filter of this entry with the specified new filter.
        /// </summary>
        void Replace(TFilter newFilter);
        /// <summary>
        /// Removes this entry from the chain it belongs to.
        /// </summary>
        void Remove();
    }
}
