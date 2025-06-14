
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
using FSO.Content;
using FSO.Files.Formats.IFF;
using FSO.Files.Formats.IFF.Chunks;
using FSO.IDE.ResourceBrowser;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FSO.IDE.Managers
{
    public class IffEditManager
    {
        public Dictionary<IffFile, IffResWindow> ResourceWindow = new Dictionary<IffFile, IffResWindow>();

        public IffResWindow OpenResourceWindow(GameObject obj)
        {
            if (obj == null) return null;
            if (ResourceWindow.ContainsKey(obj.Resource.MainIff))
            {
                var resWindow = ResourceWindow[obj.Resource.MainIff];
                var form = (Form)resWindow;
                if (form.WindowState == FormWindowState.Minimized) form.WindowState = FormWindowState.Normal;
                resWindow.Activate();
                resWindow.SetTargetObject(obj);
                if (resWindow is ObjectWindow) ((ObjectWindow)resWindow).RegenObjMeta(((ObjectWindow)resWindow).ActiveIff);
                return resWindow;
            }
            //straight up spawn an object window
            var window = new ObjectWindow(obj.Resource, obj);
            window.Show();
            window.Activate();
            ResourceWindow.Add(obj.Resource.MainIff, window);
            return window;
        }

        public IffResWindow OpenResourceWindow(GameIffResource res, GameObject target)
        {
            if (ResourceWindow.ContainsKey(res.MainIff))
            {
                var resWindow = ResourceWindow[res.MainIff];
                var form = (Form)resWindow;
                if (form.WindowState == FormWindowState.Minimized) form.WindowState = FormWindowState.Normal;
                resWindow.Activate();
                resWindow.SetTargetObject(target);
                return resWindow;
            }
            //detect if object, spawn iff res if not.
            //WARNING: if OBJD missing or present in files it should not be, bad things will happen!

            IffResWindow window;
            var objs = res.List<OBJD>();
            if (objs != null && objs.Count > 0 && res is GameObjectResource)
            {
                window = new ObjectWindow(res, (target == null) ? Content.Content.Get().WorldObjects.Get(objs[0].GUID) : target);
            }
            else
            {
                window = new IffResourceViewer(res.MainIff.Filename, res, target);
            }

            ResourceWindow.Add(res.MainIff, window);
            window.Show();
            window.Activate();
            return window;
        }

        public void CloseResourceWindow(GameIffResource res)
        {
            ResourceWindow.Remove(res.MainIff);
        }
    }

    public interface IffResWindow
    {
        void SetTargetObject(GameObject obj);
        void Activate();
        void Close();
        void Show();
    }
}
