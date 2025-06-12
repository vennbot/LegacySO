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
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;


namespace MouseGetStateAndIsMouseVisibleTester
{
    public class Object : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public Vector2 pos;
        protected Texture2D texture;
        private Vector2 v2Temp;
        protected Rectangle recCurrentFrame;
        public int nAlpha;
        public float fRotation;
        public Vector2 vecRotationCenter;
        private List<Rectangle> frames;

        public Object(Game game, ref Texture2D theTexture)
            : base(game)
        {
            texture = theTexture;
            v2Temp = new Vector2();
            fRotation = 0;
            
            pos = new Vector2();
            pos.X = 0;
            pos.Y = 0;

            vecRotationCenter.X = 0;
            vecRotationCenter.Y = 0;

            Frames = new List<Rectangle>();
            Rectangle frame = new Rectangle();

            //Extract the frames from the texture
            frame.X = 0;
            frame.Y = 0;
            frame.Width = texture.Width;
            frame.Height = texture.Height;

            Frames.Add(frame);
        }

        public override void Update(GameTime gameTime)
        {
            recCurrentFrame = frames[0];
            
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            v2Temp.X = pos.X - (texture.Width / 2);
            v2Temp.Y = pos.Y - texture.Height;

            // Get the current spritebatch
            SpriteBatch sBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));

            sBatch.Draw(texture, v2Temp, recCurrentFrame, new Color(255, 255, 255, (byte)nAlpha), fRotation, vecRotationCenter, 1f, SpriteEffects.None, 0);

            base.Draw(gameTime);
        }

        public List<Rectangle> Frames
        {
            get { return frames; }
            set { frames = value; }
        }//Frames
    }
}
