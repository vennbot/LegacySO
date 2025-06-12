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

namespace Microsoft.Xna.Framework.Graphics
{
    // TODO: This class needs to be finished!

	public class EffectAnnotation
	{
		internal EffectAnnotation (
			EffectParameterClass class_,
			EffectParameterType type,
			string name,
			int rowCount,
			int columnCount,
			string semantic,
			object data)
		{
			ParameterClass = class_;
			ParameterType = type;
			Name = name;
			RowCount = rowCount;
			ColumnCount = columnCount;
			Semantic = semantic;
		}

		internal EffectAnnotation (EffectParameter parameter)
		{
			ParameterClass = parameter.ParameterClass;
			ParameterType = parameter.ParameterType;
			Name = parameter.Name;
			RowCount = parameter.RowCount;
			ColumnCount = parameter.ColumnCount;
			Semantic = parameter.Semantic;
		}

		public EffectParameterClass ParameterClass {get; private set;}
		public EffectParameterType ParameterType {get; private set;}
		public string Name {get; private set;}
		public int RowCount {get; private set;}
		public int ColumnCount {get; private set;}
		public string Semantic {get; private set;}
	}
}

