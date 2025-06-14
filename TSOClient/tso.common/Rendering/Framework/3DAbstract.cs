
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
using FSO.Common.Rendering.Framework.Camera;
using FSO.Common.Rendering.Framework.Model;

namespace FSO.Common.Rendering.Framework
{
    /// <summary>
    /// Base class for scenes with 3D elements.
    /// </summary>
    public abstract class _3DAbstract : IDisposable
    {
        public ICamera Camera;
        public string ID;
        public bool Visible = true;
        public abstract List<_3DComponent> GetElements();
        public abstract void Add(_3DComponent item);
        public abstract void Update(UpdateState Time);
        public abstract void Draw(GraphicsDevice device);

        protected _3DLayer Parent;
        private EventHandler<EventArgs> ResetEvent;

        public virtual void PreDraw(GraphicsDevice device)
        {   
        }

        public virtual void Initialize(_3DLayer layer)
        {
            Parent = layer;
        }

        /// <summary>
        /// Creates a new _3DAbstract instance.
        /// </summary>
        /// <param name="Device">A GraphicsDevice instance.</param>
        public _3DAbstract(GraphicsDevice Device)
        {
            m_Device = Device;
            ResetEvent = new EventHandler<EventArgs>(m_Device_DeviceReset);
            m_Device.DeviceReset += ResetEvent;
        }

        /// <summary>
        /// Called when m_Device is reset.
        /// </summary>
        private void m_Device_DeviceReset(object sender, EventArgs e)
        {
            DeviceReset(m_Device);
        }

        protected GraphicsDevice m_Device; 

        public abstract void DeviceReset(GraphicsDevice Device);
        public static bool IsInvalidated;





        public object Controller { get; internal set; }
        
        public void SetController(object controller)
        {
            this.Controller = controller;
        }

        public T FindController<T>()
        {
            if(Controller is T)
            {
                return (T)Controller;
            }
            return default(T);
        }

        public virtual void Dispose()
        {
            if (m_Device != null) m_Device.DeviceReset -= ResetEvent;
        }
    }
}
