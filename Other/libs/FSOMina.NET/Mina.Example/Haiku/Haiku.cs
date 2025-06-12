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

namespace Mina.Example.Haiku
{
    class Haiku
    {
        private readonly String[] _phrases;

        public Haiku(params String[] lines)
        {
            if (lines == null || lines.Length != 3)
                throw new ArgumentException("Must pass in 3 phrases of text");
            _phrases = lines;
        }

        public String[] Phrases
        {
            get { return _phrases; }
        }

        public override bool Equals(object obj)
        {
            if (Object.ReferenceEquals(obj, this))
                return true;

            Haiku haiku = obj as Haiku;
            if (haiku == null)
                return false;

            if (_phrases.Length != haiku._phrases.Length)
                return false;

            for (int i = 0; i < _phrases.Length; i++)
            {
                if (!String.Equals(_phrases[i], haiku._phrases[i]))
                    return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            int result = 1;

            foreach (String s in _phrases)
                result = 31 * result + (s == null ? 0 : s.GetHashCode());

            return result;
        }

        public override string ToString()
        {
            return "[" + String.Join(", ", _phrases) + "]";
        }
    }
}
