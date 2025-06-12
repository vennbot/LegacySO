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

using System;
using System.Collections.ObjectModel;

namespace Microsoft.Xna.Framework
{
    public sealed class GameComponentCollection : Collection<IGameComponent>
    {
        /// <summary>
        /// Event that is triggered when a <see cref="GameComponent"/> is added
        /// to this <see cref="GameComponentCollection"/>.
        /// </summary>
        public event EventHandler<GameComponentCollectionEventArgs> ComponentAdded;

        /// <summary>
        /// Event that is triggered when a <see cref="GameComponent"/> is removed
        /// from this <see cref="GameComponentCollection"/>.
        /// </summary>
        public event EventHandler<GameComponentCollectionEventArgs> ComponentRemoved;

        /// <summary>
        /// Removes every <see cref="GameComponent"/> from this <see cref="GameComponentCollection"/>.
        /// Triggers <see cref="OnComponentRemoved"/> once for each <see cref="GameComponent"/> removed.
        /// </summary>
        protected override void ClearItems()
        {
            for (int i = 0; i < base.Count; i++)
            {
                this.OnComponentRemoved(new GameComponentCollectionEventArgs(base[i]));
            }
            base.ClearItems();
        }

        protected override void InsertItem(int index, IGameComponent item)
        {
            if (base.IndexOf(item) != -1)
            {
                throw new ArgumentException("Cannot Add Same Component Multiple Times");
            }
            base.InsertItem(index, item);
            if (item != null)
            {
                this.OnComponentAdded(new GameComponentCollectionEventArgs(item));
            }
        }

        private void OnComponentAdded(GameComponentCollectionEventArgs eventArgs)
        {
            EventHelpers.Raise(this, ComponentAdded, eventArgs);
        }

        private void OnComponentRemoved(GameComponentCollectionEventArgs eventArgs)
        {
            EventHelpers.Raise(this, ComponentRemoved, eventArgs);
        }

        protected override void RemoveItem(int index)
        {
            IGameComponent gameComponent = base[index];
            base.RemoveItem(index);
            if (gameComponent != null)
            {
                this.OnComponentRemoved(new GameComponentCollectionEventArgs(gameComponent));
            }
        }

        protected override void SetItem(int index, IGameComponent item)
        {
            throw new NotSupportedException();
        }
    }
}

