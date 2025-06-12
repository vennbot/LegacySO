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
// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using System.Collections.Generic;

namespace Microsoft.Xna.Framework.Media
{
	public sealed class MediaQueue
	{
        List<Song> songs = new List<Song>();
		private int _activeSongIndex = -1;
		private Random random = new Random();

		public MediaQueue()
		{
			
		}
		
		public Song ActiveSong
		{
			get
			{
				if (songs.Count == 0 || _activeSongIndex < 0)
					return null;
				
				return songs[_activeSongIndex];
			}
		}
		
		public int ActiveSongIndex
		{
		    get
		    {
		        return _activeSongIndex;
		    }
		    set
		    {
		        _activeSongIndex = value;
		    }
		}

        internal int Count
        {
            get
            {
                return songs.Count;
            }
        }

        public Song this[int index]
        {
            get
            {
                return songs[index];
            }
        }

        internal IEnumerable<Song> Songs
        {
            get
            {
                return songs;
            }
        }

		internal Song GetNextSong(int direction, bool shuffle)
		{
			if (shuffle)
				_activeSongIndex = random.Next(songs.Count);
			else			
				_activeSongIndex = (int)MathHelper.Clamp(_activeSongIndex + direction, 0, songs.Count - 1);
			
			return songs[_activeSongIndex];
		}
		
		internal void Clear()
		{
			Song song;
			for(; songs.Count > 0; )
			{
				song = songs[0];
#if !DIRECTX
				song.Stop();
#endif
				songs.Remove(song);
			}	
		}

#if !DIRECTX
        internal void SetVolume(float volume)
        {
            int count = songs.Count;
            for (int i = 0; i < count; ++i)
                songs[i].Volume = volume;
        }
#endif

        internal void Add(Song song)
        {
            songs.Add(song);
        }

#if !DIRECTX
        internal void Stop()
        {
            int count = songs.Count;
            for (int i = 0; i < count; ++i)
                songs[i].Stop();
        }
#endif
	}
}

