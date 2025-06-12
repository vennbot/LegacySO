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
using System.Collections.Generic;

namespace FSO.Client.Utils
{
    public static class CollectionUtils
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static IEnumerable<TResult> Select<TSource, TResult>(this Array items, Func<TSource, TResult> converter)
        {
            var result = new List<TResult>();
            foreach (var item in items)
            {
                result.Add(converter((TSource)item));
            }
            return result;
        }


        public static Dictionary<TKey, TValue> Clone<TKey, TValue>(Dictionary<TKey, TValue> input)
        {
            var result = new Dictionary<TKey, TValue>();
            foreach (var val in input)
            {
                result.Add(val.Key, val.Value);
            }
            return result;
        }


        private static Random RAND = new Random();
        public static T RandomItem<T>(this T[] items)
        {
            var index = RAND.Next(items.Length);
            return items[index];
        }
    }
}
