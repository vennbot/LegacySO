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
using System.Collections.ObjectModel;
using System.Reflection;

namespace Microsoft.Xna.Framework.Content.Pipeline
{
    /// <summary>
    /// Represents a processor parameter. Processor parameters are automatically discovered by the content pipeline. Therefore, only custom processor developers should use this class directly.
    /// </summary>
    [SerializableAttribute]
    public sealed class ProcessorParameter
    {
        PropertyInfo propInfo;
        ReadOnlyCollection<string> enumValues;

        /// <summary>
        /// Default value of the processor parameter.
        /// </summary>
        public Object DefaultValue { get; set; }

        /// <summary>
        /// Description of the parameter, as specified by the [Description] attribute.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Name of the parameter displayed in the designer, as specified by the [DisplayName] attribute.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets a value indicating whether the parameter is an enumeration.
        /// </summary>
        public bool IsEnum
        {
            get
            {
                return enumValues != null;
            }
        }

        /// <summary>
        /// Available options for enumerated type parameters. For parameters of other types, this value is null.
        /// </summary>
        public ReadOnlyCollection<string> PossibleEnumValues
        {
            get
            {
                return enumValues;
            }
        }

        /// <summary>
        /// Name of the property, as defined in the C# code.
        /// </summary>
        public string PropertyName
        {
            get
            {
                return propInfo.Name;
            }
        }

        /// <summary>
        /// Type of the parameter.
        /// </summary>
        public string PropertyType
        {
            get
            {
                return propInfo.PropertyType.Name;
            }
        }

        /// <summary>
        /// Constructs a ProcessorParameter instance.
        /// </summary>
        /// <param name="propertyInfo">The info for the property.</param>
        internal ProcessorParameter(PropertyInfo propertyInfo)
        {
            propInfo = propertyInfo;
            if (propInfo.PropertyType.IsEnum)
                enumValues = new ReadOnlyCollection<string>(propInfo.PropertyType.GetEnumNames());
        }
    }
}
