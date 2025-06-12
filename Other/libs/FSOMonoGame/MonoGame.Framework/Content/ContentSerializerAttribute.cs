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

namespace Microsoft.Xna.Framework.Content
{	
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class ContentSerializerAttribute : Attribute
    {
        private string _collectionItemName;

        /// <summary>
        /// Creates an instance of the attribute.
        /// </summary>
        public ContentSerializerAttribute()
        {
            AllowNull = true;
        }

        public bool AllowNull { get; set; }

        /// <summary>
        /// Returns the overriden XML element name or the default "Item".
        /// </summary>
        public string CollectionItemName
        {
            get
            {
                // Return the defaul if unset.
                if (string.IsNullOrEmpty(_collectionItemName))
                    return "Item";

                return _collectionItemName;
            }
            set
            {
                _collectionItemName = value;
            }
        }

        public string ElementName { get; set; }

        public bool FlattenContent { get; set; }

        /// <summary>
        /// Returns true if the default CollectionItemName value was overridden.
        /// </summary>
        public bool HasCollectionItemName
        {
            get
            {
                return !string.IsNullOrEmpty(_collectionItemName);
            }
        }

        public bool Optional { get; set; }

        public bool SharedResource { get; set; }

        public ContentSerializerAttribute Clone()
        {
            var clone = new ContentSerializerAttribute ();
            clone.AllowNull = AllowNull;
            clone._collectionItemName = _collectionItemName;
            clone.ElementName = ElementName;
            clone.FlattenContent = FlattenContent;
            clone.Optional = Optional;
            clone.SharedResource = SharedResource;
            return clone;
        }
    }
} 
