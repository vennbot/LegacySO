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
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Dressup
{
    public class Camera
    {
        private Vector3 _position;
        private Vector3 _lookAt;
        private Matrix _viewMatrix;
        private Matrix _projectionMatrix;
        private float _aspectRatio;

        public Camera(Viewport viewport)
        {
            this._aspectRatio = ((float)viewport.Width) / ((float)viewport.Height);
            this._projectionMatrix = Matrix.CreatePerspectiveFieldOfView(
                                        MathHelper.ToRadians(40.0f),
                                        this._aspectRatio,
                                        1.0f,
                                        10000.0f);
        }

        public Vector3 Position
        {
            get { return this._position; }
            set { this._position = value; }
        }
        public Vector3 LookAt
        {
            get { return this._lookAt; }
            set { this._lookAt = value; }
        }
        public Matrix ViewMatrix
        {
            get { return this._viewMatrix; }
            set { _viewMatrix = value; }
        }
        public Matrix ProjectionMatrix
        {
            get { return this._projectionMatrix; }
        }
        public void Update()
        {
            this._viewMatrix =
                Matrix.CreateLookAt(this._position, this._lookAt, Vector3.Up);
        }
    }
}
