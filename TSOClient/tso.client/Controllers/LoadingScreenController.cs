
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
using FSO.Client.UI.Screens;
using FSO.Common.Content;
using FSO.Common.Utils.Cache;
using FSO.Content;
using FSO.SimAntics;
using System;

namespace FSO.Client.Controllers
{
    public class LoadingScreenController
    {
        public ContentPreloader Loader;

        public LoadingScreenController(LoadingScreen view, Content.Content content, ICache cache)
        {
            Loader = new ContentPreloader();

            Loader.MainContentAction = (Action donePart) =>
            {
                VMContext.InitVMConfig(false);
                FSO.Content.Content.Init(GlobalSettings.Default.StartupPath, GameFacade.GraphicsDevice);
            };

            /** Init cache **/
            Loader.Add(new CacheInit((FileSystemCache)cache));

            /*
            // UI Textures
            Loader.Add(content.UIGraphics.List());
            //Sim stuff
            Loader.Add(content.AvatarOutfits.List());
            Loader.Add(content.AvatarAppearances.List());
            Loader.Add(content.AvatarPurchasables.List());
            Loader.Add(content.AvatarThumbnails.List());
            */
        }

        public void Preload()
        {
            Loader.Preload(GameFacade.GraphicsDevice);
        }
    }

    /// <summary>
    /// Not really content, but allows us to keep the loading UI going until init is done
    /// </summary>
    public class CacheInit : IContentReference
    {
        private FileSystemCache Cache;

        public CacheInit(FileSystemCache cache)
        {
            this.Cache = cache;
        }

        public object GetGeneric()
        {
            Cache.Init();
            return Cache;
        }

        public object GetThrowawayGeneric()
        {
            throw new NotImplementedException();
        }
    }
}
