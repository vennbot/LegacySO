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
using Microsoft.Xna.Framework;

namespace MonoGame.Tests {
	partial class FramePixelData {
		public FramePixelData (int width, int height, Color[] data)
		{
			_width = width;
			_height = height;
			_data = data;
		}

		public FramePixelData (int width, int height)
			: this(width, height, new Color[width * height])
		{
		}

		private Color [] _data;
		public Color [] Data {
			get { return _data; }
		}

		private int _width;
		public int Width { get { return _width; } }

		private int _height;
		public int Height { get { return _height; } }
	}
}

