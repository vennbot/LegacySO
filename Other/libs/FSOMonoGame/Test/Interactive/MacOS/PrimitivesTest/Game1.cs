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
#region File Description
//-----------------------------------------------------------------------------
// Game1.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;

namespace DrawUserPrimitivesWindows
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        Matrix worldMatrix;
        Matrix viewMatrix;
        Matrix projectionMatrix;

        BasicEffect basicEffect;
        VertexDeclaration vertexDeclaration;
        VertexPositionColor[] pointList;
        VertexBuffer vertexBuffer;

        //int points = 8;
		int points = 8;
        short[] lineListIndices;
        short[] lineStripIndices;
        short[] triangleListIndices;
        short[] triangleStripIndices;

        enum PrimType
        {
            LineList,
            LineStrip,
            TriangleList,
            TriangleStrip
        };
        PrimType typeToDraw = PrimType.LineList;

        RasterizerState rasterizerState;

        GamePadState currentGamePadState;
        GamePadState lastGamePadState;

        KeyboardState currentKeyboardState;
        KeyboardState lastKeyboardState;

        GraphicsDeviceManager graphics;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            InitializeTransform();
            InitializeEffect();
            InitializePoints();
            InitializeLineList();
            InitializeLineStrip();
            InitializeTriangleList();
            InitializeTriangleStrip();

            rasterizerState = new RasterizerState();
            rasterizerState.FillMode = FillMode.WireFrame;
            rasterizerState.CullMode = CullMode.None;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
		void DumpMatrix(String desc , Matrix mat) {
			Console.WriteLine("\n\n" + desc +"\n==============="+ "\n M11 - " + mat.M11
				+ "\n M12 - " + mat.M12
				+ "\n M13 - " + mat.M13
				+ "\n M14 - " + mat.M14

				+ "\n M21 - " + mat.M21
				+ "\n M22 - " + mat.M22
				+ "\n M23 - " + mat.M23
				+ "\n M24 - " + mat.M24

				+ "\n M31 - " + mat.M31
				+ "\n M32 - " + mat.M32
				+ "\n M33 - " + mat.M33
				+ "\n M34 - " + mat.M34

				+ "\n M41 - " + mat.M41
				+ "\n M42 - " + mat.M42
				+ "\n M43 - " + mat.M43
				+ "\n M44 - " + mat.M44

				+ "\n Backward - " + mat.Backward
				+ "\n Down - " + mat.Down
				+ "\n Forward - " + mat.Forward
				+ "\n Left - " + mat.Left
				+ "\n Right - " + mat.Right
				+ "\n Translation - " + mat.Translation
				+ "\n Up - " + mat.Up
				);
		}
        /// <summary>
        /// Initializes the transforms used by the game.
        /// </summary>
        private void InitializeTransform()
        {

            viewMatrix = Matrix.CreateLookAt(
                new Vector3(0.0f, 0.0f, 1.0f),
                Vector3.Zero,
                Vector3.Up
                );
			//DumpMatrix("viewMatrix" , viewMatrix);
            projectionMatrix = Matrix.CreateOrthographicOffCenter(
                0,
                (float)GraphicsDevice.Viewport.Width,
                (float)GraphicsDevice.Viewport.Height,
                0,
                1.0f, 1000.0f);
			//DumpMatrix("projectionMatrix" , projectionMatrix);
        }

        /// <summary>
        /// Initializes the effect (loading, parameter setting, and technique selection)
        /// used by the game.
        /// </summary>
        private void InitializeEffect()
        {

            vertexDeclaration = new VertexDeclaration(new VertexElement[]
                {
                    new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
                    new VertexElement(12, VertexElementFormat.Color, VertexElementUsage.Color, 0)
                }
            );

            basicEffect = new BasicEffect(GraphicsDevice);
            basicEffect.VertexColorEnabled = true;

            worldMatrix = Matrix.CreateTranslation(GraphicsDevice.Viewport.Width / 2f - 150,
                GraphicsDevice.Viewport.Height / 2f - 50, 0);
			//DumpMatrix("worldMatrix" , worldMatrix);
            basicEffect.World = worldMatrix;
            basicEffect.View = viewMatrix;
            basicEffect.Projection = projectionMatrix;
        }

        /// <summary>
        /// Initializes the point list.
        /// </summary>
        private void InitializePoints()
        {
            pointList = new VertexPositionColor[points];

            for (int x = 0; x < points / 2; x++)
            {
                for (int y = 0; y < 2; y++)
                {
                    pointList[(x * 2) + y] = new VertexPositionColor(
                        new Vector3(x * 100, y * 100, 0), Color.White);
                }
            }

            // Initialize the vertex buffer, allocating memory for each vertex.
            vertexBuffer = new VertexBuffer(graphics.GraphicsDevice, vertexDeclaration,
                points, BufferUsage.None);

            // Set the vertex buffer data to the array of vertices.
            vertexBuffer.SetData<VertexPositionColor>(pointList);
        }
		 
        /// <summary>
        /// Initializes the line list.
        /// </summary>
        private void InitializeLineList()
        {
            // Initialize an array of indices of type short.
            lineListIndices = new short[(points * 2) - 2];

            // Populate the array with references to indices in the vertex buffer
            for (int i = 0; i < points - 1; i++)
            {
                lineListIndices[i * 2] = (short)(i);
                lineListIndices[(i * 2) + 1] = (short)(i + 1);
            }

        }

        /// <summary>
        /// Initializes the line strip.
        /// </summary>
        private void InitializeLineStrip()
        {
            // Initialize an array of indices of type short.
            lineStripIndices = new short[points];

            // Populate the array with references to indices in the vertex buffer.
            for (int i = 0; i < points; i++)
            {
                lineStripIndices[i] = (short)(i);
            }

        }

        /// <summary>
        /// Initializes the triangle list.
        /// </summary>
        private void InitializeTriangleList()
        {
            int width = 4;
            int height = 2;

            triangleListIndices = new short[(width - 1) * (height - 1) * 6];

            for (int x = 0; x < width - 1; x++)
            {
                for (int y = 0; y < height - 1; y++)
                {
                    triangleListIndices[(x + y * (width - 1)) * 6] = (short)(2 * x);
                    triangleListIndices[(x + y * (width - 1)) * 6 + 1] = (short)(2 * x + 1);
                    triangleListIndices[(x + y * (width - 1)) * 6 + 2] = (short)(2 * x + 2);

                    triangleListIndices[(x + y * (width - 1)) * 6 + 3] = (short)(2 * x + 2);
                    triangleListIndices[(x + y * (width - 1)) * 6 + 4] = (short)(2 * x + 1);
                    triangleListIndices[(x + y * (width - 1)) * 6 + 5] = (short)(2 * x + 3);
                }
            }
        }

        /// <summary>
        /// Initializes the triangle strip.
        /// </summary>
        private void InitializeTriangleStrip()
        {
            // Initialize an array of indices of type short.
            triangleStripIndices = new short[points];

            // Populate the array with references to indices in the vertex buffer.
            for (int i = 0; i < points; i++)
            {
                triangleStripIndices[i] = (short)i;
            }
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

#if MAC || WINDOWS
		if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();
#endif

            CheckGamePadInput();
            CheckKeyboardInput();

            base.Update(gameTime);
        }

        /// <summary>
        /// Determines which primitive to draw based on input from the keyboard
        /// or game pad.
        /// </summary>
        private void CheckGamePadInput()
        {
            lastGamePadState = currentGamePadState;
            currentGamePadState = GamePad.GetState(PlayerIndex.One);

            if (((currentGamePadState.Buttons.A == ButtonState.Pressed)) &&
                 (lastGamePadState.Buttons.A == ButtonState.Released))
            {
                typeToDraw++;

                if (typeToDraw > PrimType.TriangleStrip)
                    typeToDraw = 0;
            }
        }

        /// <summary>
        /// Determines which primitive to draw based on input from the keyboard
        /// or game pad.
        /// </summary>
        private void CheckKeyboardInput()
        {
            lastKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            if (currentKeyboardState.IsKeyDown(Keys.A) &&
                 lastKeyboardState.IsKeyUp(Keys.A))
            {
                typeToDraw++;

                if (typeToDraw > PrimType.TriangleStrip)
                    typeToDraw = 0;
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.SteelBlue);

            // The effect is a compiled effect created and compiled elsewhere
            // in the application.

            foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();

                switch (typeToDraw)
                {
                    case PrimType.LineList:
                        DrawLineList();
                        break;
                    case PrimType.LineStrip:
                        DrawLineStrip();
                        break;
                    case PrimType.TriangleList:
                        GraphicsDevice.RasterizerState = rasterizerState;
                        DrawTriangleList();
                        break;
                    case PrimType.TriangleStrip:
                        GraphicsDevice.RasterizerState = rasterizerState;
                        DrawTriangleStrip();
                        break;
                }

            }

            base.Draw(gameTime);
        }


        /// <summary>
        /// Draws the line list.
        /// </summary>
        private void DrawLineList()
        {
            GraphicsDevice.DrawUserIndexedPrimitives<VertexPositionColor>(
                PrimitiveType.LineList,
                pointList,
                0,  // vertex buffer offset to add to each element of the index buffer
                8,  // number of vertices in pointList
                lineListIndices,  // the index buffer
                0,  // first index element to read
                7   // number of primitives to draw
            );
        }

        /// <summary>
        /// Draws the line strip.
        /// </summary>
        private void DrawLineStrip()
        {
            for (int i = 0; i < pointList.Length; i++)
                pointList[i].Color = Color.Red;

            GraphicsDevice.DrawUserIndexedPrimitives<VertexPositionColor>(
                PrimitiveType.LineStrip,
                pointList,
                0,   // vertex buffer offset to add to each element of the index buffer
                8,   // number of vertices to draw
                lineStripIndices,
                0,   // first index element to read
                7    // number of primitives to draw
            );
            for (int i = 0; i < pointList.Length; i++)
                pointList[i].Color = Color.White;

        }

        /// <summary>
        /// Draws the triangle list.
        /// </summary>
        private void DrawTriangleList()
        {
            GraphicsDevice.DrawUserIndexedPrimitives<VertexPositionColor>(
                PrimitiveType.TriangleList,
                pointList,
                0,   // vertex buffer offset to add to each element of the index buffer
                8,   // number of vertices to draw
                triangleListIndices,
                0,   // first index element to read
                6    // number of primitives to draw
            );
        }

        /// <summary>
        /// Draws the triangle strip.
        /// </summary>
        private void DrawTriangleStrip()
        {
            for (int i = 0; i < pointList.Length; i++)
                pointList[i].Color = Color.Red;

            GraphicsDevice.DrawUserIndexedPrimitives<VertexPositionColor>(
                PrimitiveType.TriangleStrip,
                pointList,
                0,  // vertex buffer offset to add to each element of the index buffer
                8,  // number of vertices to draw
                triangleStripIndices,
                0,  // first index element to read
                6   // number of primitives to draw
            );
            for (int i = 0; i < pointList.Length; i++)
                pointList[i].Color = Color.White;

        }
    }
}
