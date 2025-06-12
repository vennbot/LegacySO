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
using FSO.Content.Model;
using FSO.Files.HIT;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;

namespace FSO.Content.Interfaces
{
    public interface IAudioProvider
    {
        void Init();

        /// <summary>
        /// Gets a Hitlist using its id.
        /// TSO: file instance id
        /// TS1: hitlist id
        /// </summary>
        /// <param name="InstanceID">The id of the Hitlist.</param>
        /// <returns>A Hitlist instance.</returns>
        Hitlist GetHitlist(uint InstanceID, HITResourceGroup group);

        /// <summary>
        /// Gets a Track using its ID.
        /// </summary>
        /// <param name="value">Track ID</param>
        /// <param name="fallback">(TSO ONLY) Secondary Track ID lookup</param>
        /// <returns>A Track instance.</returns>
        Track GetTrack(uint value, uint fallback, HITResourceGroup group);

        /// <summary>
        /// Gets a sound effect from the sound effects cache.
        /// </summary>
        /// <param name="Patch">A Patch instance containing the file location or ID.</param>
        /// <returns>The sound effect.</returns>
        SoundEffect GetSFX(Patch patch);

        /// <summary>
        /// Gets a Patch instance for the given patch ID.
        /// TSO: Patch ID directly translates to FileID.
        /// TS1: Patch ID lookup to obtain patch.
        /// </summary>
        /// <param name="id">The Patch ID.</param>
        /// <returns>A Patch instance.</returns>
        Patch GetPatch(uint id, HITResourceGroup group);


        /// <summary>
        /// A dictionary of sound events for the HIT VM to call upon. Should be generated when content initializes.
        /// </summary>
        Dictionary<string, HITEventRegistration> Events
        {
            get;
        }

        /// <summary>
        /// A dictionary mapping music station string ids to source directories.
        /// </summary>
        Dictionary<string, string> StationPaths
        {
            get;
        }


        /// <summary>
        /// A dictionary mapping music modes to music station IDs.
        /// </summary>
        Dictionary<int, string> MusicModes
        {
            get;
        }
    }
}
