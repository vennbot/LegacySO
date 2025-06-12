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
using System.Text;

namespace Iffinator.Flash
{
    /// <summary>
    /// Class for generating unique random numbers within a given range. Removes numbers from list
    /// once chosen from an instance of an object so that it is impossible for them to be selected again.
    /// From: http://cognize2k.wordpress.com/2008/03/26/unique-random-number-generator-class-c-net/
    /// </summary>
    public class UniqueRandom
    {
        private Random m_random = new Random();
        private int m_newRandomNumber = 0;
        private List<int> RemainingNumbers;
        
        // Constructor
        public UniqueRandom(int requiredRangeLow, int requiredRangeHi)
        {
            // Get the range
            int range = (requiredRangeHi - requiredRangeLow);
            // Initialise array that will hold the numbers within the range
            int[] rangeNumbersArr = new int[range + 1];
            
            // Assign array element values within range
            for (int count = 0; count < rangeNumbersArr.Length; count++)
            {
                rangeNumbersArr[count] = requiredRangeLow + count;
            }

            // Initialize the List and populate with values from rangeNumbersArr
            RemainingNumbers = new List<int>();
            RemainingNumbers.AddRange(rangeNumbersArr);
        }

        /// <summary>
        /// This method returns a random integer within the given range. Each call produces a new random number
        /// </summary>
        /// <returns></returns>
        public int NewRandomNumber()
        {
            if (RemainingNumbers.Count != 0)
            {
                // Select random number from list
                int index = m_random.Next(0, RemainingNumbers.Count);
                m_newRandomNumber = RemainingNumbers[index];
                // Remove selected number from Remaining Numbers List
                RemainingNumbers.RemoveAt(index);
                
                return m_newRandomNumber;
            }
            else
            {
                throw new System.InvalidOperationException("All numbers in the range have now been used." + 
                "Cannot continue selecting random numbers from a list with no members.");
            }
        }
    }
}
