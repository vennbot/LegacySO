
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
namespace FSO.SimAntics.Engine
{
    public enum VMPrimitiveExitCode : byte
    {
        GOTO_TRUE = 0,
        GOTO_FALSE = 1,
        GOTO_TRUE_NEXT_TICK = 2,
        GOTO_FALSE_NEXT_TICK = 3,
        RETURN_TRUE = 4,
        RETURN_FALSE = 5,
        ERROR = 6,
        CONTINUE_NEXT_TICK = 7,
        CONTINUE = 8, //used for primitives which change the control flow, don't quite return, more or idle yet.
        INTERRUPT = 9, //instantly ends this queue item. Used by Idle for Input with allow push: when any interactions are queued it exits out like this.
        CONTINUE_FUTURE_TICK = 10, //special schedule mode used by idle and idle for input. removes processing for this object for multiple frames.
    }

    public static class VMPrimitiveExitCodeExtensions
    {
        public static VMPrimitiveExitCode AsGotoExitCode(this bool value)
        {
            return value ? VMPrimitiveExitCode.GOTO_TRUE : VMPrimitiveExitCode.GOTO_FALSE;
        }
    }
}
