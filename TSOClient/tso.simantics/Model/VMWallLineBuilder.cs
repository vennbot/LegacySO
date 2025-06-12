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
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace FSO.SimAntics.Model
{
    public class VMWallLineBuilder
    {
        //x = a&0xFFF;
        //y = (a>>12)&0xFFF;
        //type = (a>>24);
        public Dictionary<uint, Vector2[]> LinesByLocation = new Dictionary<uint, Vector2[]>();
        public List<Vector2[]> Lines = new List<Vector2[]>();

        public void AddLine(int obsX, int obsY, int type)
        {
            //types:
            //0: line across y (x static)
            //1: line across x (y static)
            //2: diag (low y to high y, high x to low x)
            //3: diag (low x,y to high x,y)

            uint myID = (uint)(obsX | (obsY << 12) | (type << 24));
            if (LinesByLocation.ContainsKey(myID)) return;

            Vector2[] prev;
            Vector2[] post;
            Vector2[] line = null;
            switch (type)
            {
                case 0:
                    LinesByLocation.TryGetValue((uint)(obsX | ((obsY - 16) << 12) | (type << 24)), out prev);
                    LinesByLocation.TryGetValue((uint)(obsX | ((obsY + 16) << 12) | (type << 24)), out post);
                    if (prev != null)
                    {
                        prev[1].Y += 16;
                        line = prev;
                    } else if (post != null)
                    {
                        post[0].Y -= 16;
                        line = post;
                    } else
                    {
                        line = new Vector2[] { new Vector2(obsX, obsY), new Vector2(obsX, obsY + 16) };
                        Lines.Add(line);
                    }
                    break;
                case 1:
                    LinesByLocation.TryGetValue((uint)((obsX - 16) | (obsY << 12) | (type << 24)), out prev);
                    LinesByLocation.TryGetValue((uint)((obsX + 16) | (obsY << 12) | (type << 24)), out post);
                    if (prev != null)
                    {
                        prev[1].X += 16;
                        line = prev;
                    }
                    else if (post != null)
                    {
                        post[0].X -= 16;
                        line = post;
                    }
                    else
                    {
                        line = new Vector2[] { new Vector2(obsX, obsY), new Vector2(obsX + 16, obsY) };
                        Lines.Add(line);
                    }
                    break;
                case 2:
                    LinesByLocation.TryGetValue((uint)((obsX - 16) | ((obsY + 16) << 12) | (type << 24)), out prev);
                    LinesByLocation.TryGetValue((uint)((obsX + 16) | ((obsY - 16) << 12) | (type << 24)), out post);
                    if (prev != null)
                    {
                        prev[1].Y -= 16;
                        prev[1].X += 16;
                        line = prev;
                    }
                    else if (post != null)
                    {
                        post[0].Y += 16;
                        post[0].X -= 16;
                        line = post;
                    }
                    else
                    {
                        line = new Vector2[] { new Vector2(obsX, obsY + 16), new Vector2(obsX + 16, obsY) };
                        Lines.Add(line);
                    }
                    break;
                case 3:
                    LinesByLocation.TryGetValue((uint)((obsX - 16) | ((obsY - 16) << 12) | (type << 24)), out prev);
                    LinesByLocation.TryGetValue((uint)((obsX + 16) | ((obsY + 16) << 12) | (type << 24)), out post);
                    if (prev != null)
                    {
                        prev[1].Y += 16;
                        prev[1].X += 16;
                        line = prev;
                    }
                    else if (post != null)
                    {
                        post[0].Y -= 16;
                        post[0].X -= 16;
                        line = post;
                    }
                    else
                    {
                        line = new Vector2[] { new Vector2(obsX, obsY), new Vector2(obsX + 16, obsY + 16) };
                        Lines.Add(line);
                    }
                    break;
            }
            LinesByLocation[myID] = line;

        }
    }
}
