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
namespace FSO.SimAntics.Model
{
    public enum VMMotive
    {
        HappyLife = 0,
        HappyWeek = 1,
        HappyDay = 2,
        Mood = 3,
        UnusedPhysical = 4,
        Energy = 5,
        Comfort = 6,
        Hunger = 7,
        Hygiene = 8,
        Bladder = 9,
        UnusedMental = 10,
        SleepState = 11,
        UnusedStress = 12,
        Room = 13,
        Social = 14,
        Fun = 15
    }
}
