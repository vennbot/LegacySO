
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
using Microsoft.Xna.Framework;
using System;

namespace FSO.Common.Utils
{
    public static class DirectionUtils
    {

        /// <summary>
        /// Finds the difference between two radian directions.
        /// </summary>
        /// <param name="a">The direction to subtract from.</param>
        /// <param name="b">The direction to subtract.</param>
        public static double Difference(double a, double b) {
            double value = PosMod(b-a, Math.PI*2);
            if (value > Math.PI) value -= Math.PI * 2;
            return value;
        }

        /// <summary>
        /// Normalizes a direction to the range -PI through PI.
        /// </summary>
        /// <param name="dir">The direction to normalize.</param>
        public static double Normalize(double dir)
        {
            dir = PosMod(dir, Math.PI * 2);
            if (dir > Math.PI) dir -= Math.PI * 2;
            return dir;
        }

        /// <summary>
        /// Normalizes a direction in degrees to the range -180 through 180.
        /// </summary>
        /// <param name="dir">The direction to normalize.</param>
        public static double NormalizeDegrees(double dir)
        {
            dir = PosMod(dir, 360);
            if (dir > 180) dir -= 360;
            return dir;
        }

        /// <summary>
        /// Calculates the mathematical modulus of a value.
        /// </summary>
        /// <param name="x">The number to mod.</param>
        /// <param name="x">The factor to use.</param>
        public static double PosMod(double x, double m)
        {
            return (x % m + m) % m;
        }

        private static int[] tab32 = new int[] {
             0,  9,  1, 10, 13, 21,  2, 29,
            11, 14, 16, 18, 22, 25,  3, 30,
             8, 12, 20, 28, 15, 17, 24,  7,
            19, 27, 23,  6, 26,  5,  4, 31
        };

        public static int Log2Int(uint value)
        {
            value |= value >> 1;
            value |= value >> 2;
            value |= value >> 4;
            value |= value >> 8;
            value |= value >> 16;
            return tab32[(uint)(value * 0x07C4ACDD) >> 27];
        }

        public static float QuaternionDistance(Quaternion q1, Quaternion q2)
        {
            var inner = q1.X * q2.X + q1.Y * q2.Y + q1.Z * q2.Z + q1.W * q2.W;
            return (float)Math.Acos(2 * (inner * inner) - 1);
        }
    }
}
