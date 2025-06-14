
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
namespace FSO.Files.HIT
{
    public enum HITArgs
    {
        kArgsNormal = 0,
        kArgsVolPan = 1,
        kArgsIdVolPan = 2,
        kArgsXYZ = 3
    }

    public enum HITControlGroups
    {
        kGroupSFX = 1,
        kGroupMusic = 2,
        kGroupVox = 3
    }

    public enum HITDuckingPriorities
    {
        duckpri_unknown1 = 32,
        duckpri_unknown2 = 5000,
        duckpri_always = 0x0,
        duckpri_low = 0x1,
        duckpri_normal = 0x14,
        duckpri_high = 0x1e,
        duckpri_higher = 0x28,
        duckpri_evenhigher = 0x32,
        duckpri_never = 0x64
    }

    public enum HITEvents
    {
        kSoundobPlay = 1,
        kSoundobStop = 2,
        kSoundobKill = 3,
        kSoundobUpdate = 4,
        kSoundobSetVolume = 5,
        kSoundobSetPitch = 6,
        kSoundobSetPan = 7,
        kSoundobSetPosition = 8,
        kSoundobSetFxType = 9,
        kSoundobSetFxLevel = 10,
        kSoundobPause = 11,
        kSoundobUnpause = 12,
        kSoundobLoad = 13,
        kSoundobUnload = 14,
        kSoundobCache = 15,
        kSoundobUncache = 16,
        kSoundobCancelNote = 19,
        kKillAll = 20,
        kPause = 21,
        kUnpause = 22,
        kKillInstance = 23,
        kTurnOnTV = 30,
        kTurnOffTV = 31,
        kUpdateSourceVolPan = 32,
        kSetMusicMode = 36,
        kPlayPiano = 43,
        debugeventson = 44,
        debugeventsoff = 45,
        debugsampleson = 46,
        debugsamplesoff = 47,
        debugtrackson = 48,
        debugtracksoff = 49
    }

    public enum HITPerson
    {
        Instance = 0x0,
        Gender = 0x1
    }
}
