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

using MonoGame.OpenGL;

namespace Microsoft.Xna.Framework.Graphics
{
    partial class OcclusionQuery
    {
        private int glQueryId = -1;

        private void PlatformConstruct()
        {
            GL.GenQueries(1, out glQueryId);
            GraphicsExtensions.CheckGLError();
        }

        private void PlatformBegin()
        {
            GL.BeginQuery(QueryTarget.SamplesPassed, glQueryId);
            GraphicsExtensions.CheckGLError();
        }

        private void PlatformEnd()
        {
            GL.EndQuery(QueryTarget.SamplesPassed);
            GraphicsExtensions.CheckGLError();
        }

        private bool PlatformGetResult(out int pixelCount)
        {
            int resultReady = 0;
            GL.GetQueryObject(glQueryId, GetQueryObjectParam.QueryResultAvailable, out resultReady);
            GraphicsExtensions.CheckGLError();

            if (resultReady == 0)
            {
                pixelCount = 0;
                return false;
            }

            GL.GetQueryObject(glQueryId, GetQueryObjectParam.QueryResult, out pixelCount);
            GraphicsExtensions.CheckGLError();

            return true;
        }

        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                if (glQueryId > -1)
                {
                    GraphicsDevice.DisposeQuery(glQueryId);
                    glQueryId = -1;
                }
            }

            base.Dispose(disposing);
        }
    }
}

