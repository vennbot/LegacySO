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
namespace Mina.Example.SumUp.Codec
{
    static class Constants
    {
        public static readonly int TYPE_LEN = 2;

        public static readonly int SEQUENCE_LEN = 4;

        public static readonly int HEADER_LEN = TYPE_LEN + SEQUENCE_LEN;

        public static readonly int BODY_LEN = 12;

        public static readonly int RESULT = 0;

        public static readonly int ADD = 1;

        public static readonly int RESULT_CODE_LEN = 2;

        public static readonly int RESULT_VALUE_LEN = 4;

        public static readonly int ADD_BODY_LEN = 4;

        public static readonly int RESULT_OK = 0;

        public static readonly int RESULT_ERROR = 1;
    }
}
