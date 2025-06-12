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

namespace Microsoft.Xna.Framework
{
    public class DrawableGameComponent : GameComponent, IDrawable
    {
        private bool _initialized;
        private int _drawOrder;
        private bool _visible = true;

        public Graphics.GraphicsDevice GraphicsDevice
        {
            get { return this.Game.GraphicsDevice; } 
        }

        public int DrawOrder
        {
            get { return _drawOrder; }
            set
            {
                if (_drawOrder != value)
                {
                    _drawOrder = value;
                    OnDrawOrderChanged(this, EventArgs.Empty);
                }
            }
        }

        public bool Visible
        {
            get { return _visible; }
            set
            {
                if (_visible != value)
                {
                    _visible = value;
                    OnVisibleChanged(this, EventArgs.Empty);
                }
            }
        }

        public event EventHandler<EventArgs> DrawOrderChanged;
        public event EventHandler<EventArgs> VisibleChanged;

        public DrawableGameComponent(Game game)
            : base(game)
        {
        }

        public override void Initialize()
        {
            if (!_initialized)
            {
                _initialized = true;
                LoadContent();
            }
        }

        protected virtual void LoadContent() { }

        protected virtual void UnloadContent () { }

        public virtual void Draw(GameTime gameTime) { }

        protected virtual void OnVisibleChanged(object sender, EventArgs args)
        {
            EventHelpers.Raise(this, VisibleChanged, args);
        }

        protected virtual void OnDrawOrderChanged(object sender, EventArgs args)
        {
            EventHelpers.Raise(this, DrawOrderChanged, args);
        }
    }
}
