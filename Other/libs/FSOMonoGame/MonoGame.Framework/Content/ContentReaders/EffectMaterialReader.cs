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
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using Microsoft.Xna.Framework.Utilities;

namespace Microsoft.Xna.Framework.Content
{
	internal class EffectMaterialReader : ContentTypeReader<EffectMaterial>
	{
		protected internal override EffectMaterial Read (ContentReader input, EffectMaterial existingInstance)
		{
			var effect = input.ReadExternalReference<Effect> ();
			var effectMaterial = new EffectMaterial (effect);

			var dict = input.ReadObject<Dictionary<string, object>> ();

			foreach (KeyValuePair<string, object> item in dict) {
				var parameter = effectMaterial.Parameters [item.Key];
				if (parameter != null) {

					Type itemType = item.Value.GetType();

					if (ReflectionHelpers.IsAssignableFromType(typeof(Texture), itemType)) {
						parameter.SetValue ((Texture)item.Value);
					}
					else if (ReflectionHelpers.IsAssignableFromType(typeof(int), itemType)) {
						parameter.SetValue((int) item.Value);
					}
					else if (ReflectionHelpers.IsAssignableFromType(typeof(bool), itemType)) {
						parameter.SetValue((bool) item.Value);
					}
					else if (ReflectionHelpers.IsAssignableFromType(typeof(float), itemType)) {
						parameter.SetValue((float) item.Value);
					}
					else if (ReflectionHelpers.IsAssignableFromType(typeof(float []), itemType)) {
						parameter.SetValue((float[]) item.Value);
					}
					else if (ReflectionHelpers.IsAssignableFromType(typeof(Vector2), itemType)) {
						parameter.SetValue((Vector2) item.Value);
					}
					else if (ReflectionHelpers.IsAssignableFromType(typeof(Vector2 []), itemType)) {
						parameter.SetValue((Vector2 []) item.Value);
					}
					else if (ReflectionHelpers.IsAssignableFromType(typeof(Vector3), itemType)) {
						parameter.SetValue((Vector3) item.Value);
					}
					else if (ReflectionHelpers.IsAssignableFromType(typeof(Vector3 []), itemType)) {
						parameter.SetValue((Vector3 []) item.Value);
					}
					else if (ReflectionHelpers.IsAssignableFromType(typeof(Vector4), itemType)) {
						parameter.SetValue((Vector4) item.Value);
					}
					else if (ReflectionHelpers.IsAssignableFromType(typeof(Vector4 []), itemType)) {
						parameter.SetValue((Vector4 []) item.Value);
					}
					else if (ReflectionHelpers.IsAssignableFromType(typeof(Matrix), itemType)) {
						parameter.SetValue((Matrix) item.Value);
					}
					else if (ReflectionHelpers.IsAssignableFromType(typeof(Matrix []), itemType)) {
						parameter.SetValue((Matrix[]) item.Value);
					}
					else if (ReflectionHelpers.IsAssignableFromType(typeof(Quaternion), itemType)) {
						parameter.SetValue((Quaternion) item.Value);
					}
					else {
						throw new NotSupportedException ("Parameter type is not supported");
					}
				} else {
					Debug.WriteLine ("No parameter " + item.Key);
				}
			}

			return effectMaterial;
		}
	}
}
