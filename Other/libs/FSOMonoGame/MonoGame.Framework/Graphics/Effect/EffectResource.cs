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

using Microsoft.Xna.Framework.Utilities;
using System.IO;

namespace Microsoft.Xna.Framework.Graphics
{
    /// <summary>
    /// Internal helper for accessing the bytecode for stock effects.
    /// </summary>
    internal partial class EffectResource
    {
        public static readonly EffectResource AlphaTestEffect = new EffectResource(AlphaTestEffectName);
        public static readonly EffectResource BasicEffect = new EffectResource(BasicEffectName);
        public static readonly EffectResource DualTextureEffect = new EffectResource(DualTextureEffectName);
        public static readonly EffectResource EnvironmentMapEffect = new EffectResource(EnvironmentMapEffectName);
        public static readonly EffectResource SkinnedEffect = new EffectResource(SkinnedEffectName);
        public static readonly EffectResource SpriteEffect = new EffectResource(SpriteEffectName);

        private readonly object _locker = new object();
        private readonly string _name;
        private volatile byte[] _bytecode;

        private EffectResource(string name)
        {
            _name = name;
        }

        public byte[] Bytecode
        {
            get
            {
                if (_bytecode == null)
                {
                    lock (_locker)
                    {
                        if (_bytecode != null)
                            return _bytecode;

                        var assembly = ReflectionHelpers.GetAssembly(typeof(EffectResource));

                        var stream = assembly.GetManifestResourceStream(_name);
                        using (var ms = new MemoryStream())
                        {
                            stream.CopyTo(ms);
                            _bytecode = ms.ToArray();
                        }
                    }
                }

                return _bytecode;
            }
        }
    }
}
