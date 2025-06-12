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
using System.Text.RegularExpressions;

namespace Mina.Example.Haiku
{
    class PhraseUtilities
    {
        public static int CountSyllablesInPhrase(String phrase)
        {
            int syllables = 0;

            Regex regex = new Regex("[^\\w-]+");

            foreach (String word in regex.Split(phrase))
            {
                if (word.Length > 0)
                {
                    syllables += CountSyllablesInWord(word.ToLower());
                }
            }

            return syllables;
        }

        static int CountSyllablesInWord(String word)
        {
            char[] chars = word.ToCharArray();
            int syllables = 0;
            bool lastWasVowel = false;

            for (int i = 0; i < chars.Length; i++)
            {
                char c = chars[i];
                if (IsVowel(c))
                {
                    if (!lastWasVowel
                            || (i > 0 && IsE(chars, i - 1) && IsO(chars, i)))
                    {
                        ++syllables;
                        lastWasVowel = true;
                    }
                }
                else
                {
                    lastWasVowel = false;
                }
            }

            if (word.EndsWith("oned") || word.EndsWith("ne")
                    || word.EndsWith("ide") || word.EndsWith("ve")
                    || word.EndsWith("fe") || word.EndsWith("nes")
                    || word.EndsWith("mes"))
            {
                --syllables;
            }

            return syllables;
        }

        static bool IsE(char[] chars, int position)
        {
            return IsCharacter(chars, position, 'e');
        }

        static bool IsCharacter(char[] chars, int position, char c)
        {
            return chars[position] == c;
        }

        static bool IsO(char[] chars, int position)
        {
            return IsCharacter(chars, position, 'o');
        }

        static bool IsVowel(char c)
        {
            return c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u'
                    || c == 'y';
        }
    }
}
