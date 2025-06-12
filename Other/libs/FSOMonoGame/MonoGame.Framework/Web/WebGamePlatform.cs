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
using Microsoft.Xna.Framework.Input;
using XnaKeys = Microsoft.Xna.Framework.Input.Keys;

using JSIL;
using JSIL.Meta;

using MonoGame.Web;

namespace Microsoft.Xna.Framework
{
    using MonoGame.Web;

    public interface IHasCallback
    {
        void Callback();
    }

    class WebGamePlatform : GamePlatform, IHasCallback
    {
        private WebGameWindow _view;

        public WebGamePlatform(Game game)
            : base(game)
        {
            Window = new WebGameWindow(this);

            _view = (WebGameWindow)Window;
        }

        public virtual void Callback()
        {
            this.Game.Tick();
        }
        
        public override void Exit()
        {
        }

        public override void RunLoop()
        {
            throw new InvalidOperationException("You can not run a synchronous loop on the web platform.");
        }

        public override void StartRunLoop()
        {
            ResetWindowBounds();
            _view.window.setInterval((Action)(() => {
                _view.ProcessEvents();
                Game.Tick();
            }), 25);
        }

        public override bool BeforeUpdate(GameTime gameTime)
        {
            return true;
        }

        public override bool BeforeDraw(GameTime gameTime)
        {
            return true;
        }

        public override void EnterFullScreen()
        {
            ResetWindowBounds();
        }

        public override void ExitFullScreen()
        {
            ResetWindowBounds();
        }

        internal void ResetWindowBounds()
        {
            var graphicsDeviceManager = (GraphicsDeviceManager)Game.Services.GetService(typeof(IGraphicsDeviceManager));

            if (graphicsDeviceManager.IsFullScreen)
            {
                
            }
            else
            {
                _view.glcanvas.style.width = graphicsDeviceManager.PreferredBackBufferWidth + "px";
                _view.glcanvas.style.height = graphicsDeviceManager.PreferredBackBufferHeight + "px";
            }
        }

        public override void BeginScreenDeviceChange(bool willBeFullScreen)
        {
        }

        public override void EndScreenDeviceChange(string screenDeviceName, int clientWidth, int clientHeight)
        {
        }

        public override GameRunBehavior DefaultRunBehavior
        {
            get
            {
                return GameRunBehavior.Asynchronous;
            }
        }
    }
}

