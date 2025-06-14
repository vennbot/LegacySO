
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
using Microsoft.Xna.Framework.Graphics;

namespace FSO.Common.Rendering.Framework
{
    public class _3DLayer : IGraphicsLayer
    {
        public GraphicsDevice Device;
        public List<_3DAbstract> Scenes = new List<_3DAbstract>();
        public List<_3DAbstract> External = new List<_3DAbstract>();

        #region IGraphicsLayer Members

        public void Update(FSO.Common.Rendering.Framework.Model.UpdateState state)
        {
            foreach (var scene in Scenes)
            {
                scene.Update(state);
            }
            foreach (var scene in External)
            {
                scene.Update(state);
            }
        }

        public void PreDraw(Microsoft.Xna.Framework.Graphics.GraphicsDevice device)
        {
            foreach (var scene in Scenes)
            {
                if (scene.Visible) scene.PreDraw(device);
            }
        }

        public void Draw(Microsoft.Xna.Framework.Graphics.GraphicsDevice device)
        {
            foreach (var scene in Scenes)
            {
                if (scene.Visible) scene.Draw(device);
            }
        }

        public void Initialize(Microsoft.Xna.Framework.Graphics.GraphicsDevice device)
        {
            this.Device = device;
            foreach (var scene in Scenes)
            {
                scene.Initialize(this);
            }
            foreach (var scene in External)
            {
                scene.Initialize(this);
            }
        }

        public void Add(_3DAbstract scene)
        {
            Scenes.Add(scene);
            if (this.Device != null)
            {
                scene.Initialize(this);
            }
        }

        public void Remove(_3DAbstract scene)
        {
            Scenes.Remove(scene);
        }

        public void Clear()
        {
            foreach (var scene in Scenes)
            {
                if (scene is IDisposable) ((IDisposable)scene).Dispose();
            }
            Scenes.Clear();
        }

        /// <summary>
        /// Adds a scene to the draw stack. The system will not call
        /// Draw on the scene but it will be initialized and given updates
        /// </summary>
        /// <param name="scene"></param>
        public void AddExternal(_3DAbstract scene){
            External.Add(scene);
            if (this.Device != null)
            {
                scene.Initialize(this);
            }
        }

        public void RemoveExternal(_3DAbstract scene)
        {
            External.Remove(scene);
        }

        #endregion
    }
}
