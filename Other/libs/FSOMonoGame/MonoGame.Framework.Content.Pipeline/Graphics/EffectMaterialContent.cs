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

using Microsoft.Xna.Framework.Content.Pipeline.Processors;

namespace Microsoft.Xna.Framework.Content.Pipeline.Graphics
{
    public class EffectMaterialContent : MaterialContent
    {
        public const string EffectKey = "Effect";
        public const string CompiledEffectKey = "CompiledEffect";

        public ExternalReference<EffectContent> Effect
        {
            get { return GetReferenceTypeProperty<ExternalReference<EffectContent>>(EffectKey); }
            set { SetProperty(EffectKey, value); }
        }

        public ExternalReference<CompiledEffectContent> CompiledEffect
        {
            get { return GetReferenceTypeProperty<ExternalReference<CompiledEffectContent>>(CompiledEffectKey); }
            set { SetProperty(CompiledEffectKey, value); }
        }
    }
}
