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

namespace FSO.Common.DataService.Framework.Attributes
{
    /// <summary>
    /// The original TSO game is C++. It seems to me like TSO used to have some fixed size arrays in the data model and kept the
    /// ram alive for them. Instead of removing an item from an array it would null a specific field in the child and expect
    /// the code to not count it. This does not work well in a fully object based system so  these decorations help define
    /// the intended behavior for the DataService to handle.
    /// 
    /// If this decoration is on an attribute and the value is set to null (0) the parent object is deleted (removed from arrays)
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    public class Key : Attribute
    {
        public Key()
        {
        }
    }
}
