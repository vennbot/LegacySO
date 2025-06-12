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
using System.IO;
using Microsoft.Xna.Framework.Content;
#if !WINDOWS || DIRECTX || XNA
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;
#endif
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Tests.ContentPipeline
{
    internal static class AssetTestUtility
    {

        public static Effect LoadEffect(ContentManager content, string name)
        {
#if DIRECTX
            var gd = ((IGraphicsDeviceService) content.ServiceProvider.GetService(typeof(IGraphicsDeviceService))).GraphicsDevice;
            return CompileEffect(gd, Paths.RawEffect(name));
#else
            return content.Load<Effect>(Paths.CompiledEffect(name));
#endif
        }

        public static Effect CompileEffect(GraphicsDevice graphicsDevice, string effectPath)
        {
#if !WINDOWS || DIRECTX || XNA
            var effectProcessor = new EffectProcessor();
            var context = new TestProcessorContext(TargetPlatform.Windows, "notused.xnb");
            var compiledEffect = effectProcessor.Process(new EffectContent
            {
                EffectCode = File.ReadAllText(effectPath),
                Identity = new ContentIdentity(effectPath)
            }, context);

            return new Effect(graphicsDevice, compiledEffect.GetEffectCode());
#else // OpenGL
            throw new NotImplementedException();
#endif
        }
    }
}
