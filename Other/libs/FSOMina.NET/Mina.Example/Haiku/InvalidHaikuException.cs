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
    class InvalidHaikuException : Exception
    {
        private readonly int position;
        private readonly String phrase;
        private readonly int syllableCount;
        private readonly int expectedSyllableCount;

        public InvalidHaikuException(int position, String phrase,
                int syllableCount, int expectedSyllableCount)
            : base("phrase " + position + ", '" + phrase + "' had " + syllableCount
                        + " syllables, not " + expectedSyllableCount)
        {
            this.position = position;
            this.phrase = phrase;
            this.syllableCount = syllableCount;
            this.expectedSyllableCount = expectedSyllableCount;
        }

        public int ExpectedSyllableCount
        {
            get { return expectedSyllableCount; }
        }

        public String Phrase
        {
            get { return phrase; }
        }

        public int SyllableCount
        {
            get { return syllableCount; }
        }

        public int PhrasePositio
        {
            get { return position; }
        }
    }
}
