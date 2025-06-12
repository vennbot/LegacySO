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
namespace FSO.Common.Enum
{
    public enum LotCategory
    {
        none = 0,
        money = 1,
        offbeat = 2,
        romance = 3,
        services = 4,
        shopping = 5,
        skills = 6,
        welcome = 7,
        games = 8,
        entertainment = 9,
        residence = 10,
        community = 11, //cannot be set by users

        recent = 255 //for filter searches
    }
}
