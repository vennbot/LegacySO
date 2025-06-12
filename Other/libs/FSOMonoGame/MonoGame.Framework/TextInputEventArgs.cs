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
using Microsoft.Xna.Framework.Input;

namespace Microsoft.Xna.Framework
{
    /// <summary>
    /// This class is used for the game window's TextInput event as EventArgs.
    /// </summary>
    public class TextInputEventArgs : EventArgs
    {
        char character;
        public TextInputEventArgs(char character, Keys key = Keys.None)
        {
            this.character = character;
            this.Key = key;
        }
        public char Character
        {
            get
            {
                return character;
            }
        }
        public Keys Key {
            get; private set;
        }
    }
}
