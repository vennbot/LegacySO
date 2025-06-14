
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
using System.Collections.Generic;

namespace FSO.LotView.LMap
{
    public class LightData
    {
        public Vector2 LightPos; //light source position. falloff and shadows direction based off of this.
        public Vector2 LightDir; //direction of light, for outdoors type.
        public LightType LightType; //indoors (point) or outdoors (directional)
        public bool OutdoorsColor; //indoors colour is always white. outdoors is whatever is outdoors.
        public bool DrawingWalls; //currently unused
        public Color LightColor;
        public float FalloffMultiplier = 1f; //multiplier for shadow length, for outdoors type lights
        public float LightIntensity = 1f; //multiplier for light strength
        public int WindowRoom = -1; //defines what room this light sources its intensity from. used for window portals.

        public float Weight = 1; //internal value used for combining lights

        public float LightSize; //the size of the light's impact in 16th tiles.
        public Rectangle LightBounds; //based off of LightPos and LightSize. limits shadow and lightmap render bounds
        public float Height = 2.95f * 3 / 4f;
        public float ShadowMultiplier; //outdoors type only. informs strength of shadows.

        public sbyte Level;
        public ushort Room;

        public LightData() { }

        public LightData(Vector2 pos, bool outdoors, int size, ushort room, sbyte level, Color color)
        {
            LightPos = pos;
            LightType = LightType.ROOM;
            if (outdoors) LightIntensity = 0.8f;
            OutdoorsColor = outdoors;
            LightBounds = new Rectangle((int)pos.X-size, (int)pos.Y-size, size * 2, size * 2);
            LightSize = size;
            Room = room;
            Level = level;
            LightColor = color;
        }

        public LightData(Vector2 pos, bool outdoors, int size, ushort room, sbyte level, Color color, Components.ObjectComponent comp)
            : this(pos, outdoors, size, room, level, color)
        {
            if (comp != null && Common.Utils.GameThread.IsInGameThread())
            {
                var bounds = comp.GetParticleBounds();
                if (bounds != default)
                {
                    var mid = (bounds.Min + bounds.Max) / 2;
                    mid.X = Math.Min(0.35f, Math.Max(-0.35f, mid.X));
                    mid.Z = Math.Min(0.35f, Math.Max(-0.35f, mid.Z));
                    mid = Vector3.Transform(mid, Matrix.CreateRotationY(-comp.RadianDirection));
                    LightPos += new Vector2(mid.X, mid.Z) * 16;
                    Height = Math.Max(mid.Y, bounds.Max.Y - 0.25f) + comp.Position.Z - comp.GetBottomContainer().Position.Z;
                }
            }
        }

        public void UpdateBounds()
        {
            LightBounds = new Rectangle((int)(LightPos.X - LightSize), (int)(LightPos.Y - LightSize), (int)LightSize * 2, (int)LightSize * 2);
        }

        public static void Cluster(List<LightData> lights)
        {
            float mergeDist = (32 * 32);
            for (int i = 0; i < lights.Count; i++)
            {
                var l1 = lights[i];
                for (int j=i+1; j<lights.Count; j++)
                {
                    var l2 = lights[j];
                    if (l1.OutdoorsColor == l2.OutdoorsColor && (l1.LightPos - l2.LightPos).LengthSquared() <= mergeDist)
                    {
                        var newWeight = (l1.Weight + l2.Weight);
                        l1.LightPos = (l1.LightPos * (l1.Weight / newWeight)) + (l2.LightPos * (l2.Weight / newWeight));
                        l1.LightSize = (l1.LightSize + l2.LightSize) * 0.6f;
                        l1.LightIntensity = Math.Min(1.25f, (l1.LightIntensity + l2.LightIntensity) * 0.75f);
                        l1.LightColor = new Color(l1.LightColor.ToVector4()+ l2.LightColor.ToVector4());

                        l1.UpdateBounds();
                        lights.RemoveAt(j--);
                    }
                }
            }
        }
    }

    public enum LightType
    {
        OUTDOORS,
        ROOM,
    }
}
