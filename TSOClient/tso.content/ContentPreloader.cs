
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
using FSO.Client.Utils;
using FSO.Common.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Threading;

namespace FSO.Content
{
    public class ContentPreloader
    {
        public Action<Action> MainContentAction;
        private List<IContentReference> Pending = new List<IContentReference>();
        private int Total;
        private int Completed;
        private GraphicsDevice Graphics;

        public void Add(IContentReference reference)
        {
            Pending.Add(reference);
        }

        public void Add(IEnumerable<IContentReference> reference)
        {
            Pending.AddRange(reference);
        }

        public void Preload(GraphicsDevice gd)
        {
            Graphics = gd;

            Total = Pending.Count + (int)ContentLoadingProgress.Done;
            Completed = 0;

            Thread T = new Thread(new ThreadStart(LoadContent));
            //TODO: This should only be set to speed up debug
            //T.Priority = ThreadPriority.AboveNormal;
            T.Start();

            //LoadContent();
        }

        public double Progress
        {
            get
            {
                return ((double)Completed + (int)Content.LoadProgress) / ((double)Total);
            }
        }

        public bool IsLoading
        {
            get
            {
                return (Completed + (int)Content.LoadProgress) < Total;
            }
        }

        private void LoadContent()
        {
            Pending.Shuffle();

            MainContentAction(() => Completed++);

            while (Pending.Count > 0)
            {
                var item = Pending[0];
                Pending.RemoveAt(0);
                var value = item.GetGeneric();

                /*if(value is ITextureRef)
                {
                    ((ITextureRef)value).Get(Graphics);
                }*/
                
                Completed++;
            }
        }
    }
}
