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

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BlockingRun
{
    public class BlockingRunGame : Game
    {
        public BlockingRunGame()
        {
            new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        private SpriteBatch _spriteBatch;
        private SpriteFont _font;
        protected override void LoadContent()
        {
            base.LoadContent();

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _font = Content.Load<SpriteFont>("SimpleFont");
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);

            _spriteBatch.Begin();
            _spriteBatch.DrawString(_font, "A magical blocking Game.Run!", Vector2.Zero, Color.White);
            _spriteBatch.End();
        }
    }
}
