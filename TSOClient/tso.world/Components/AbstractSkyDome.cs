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
using FSO.Common;
using FSO.Common.Model;
using FSO.Common.Rendering;
using FSO.Common.Utils;
using FSO.Files;
using FSO.LotView.Model;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;

namespace FSO.LotView.Components
{
    public class AbstractSkyDome : IDisposable
    {
        private static string DefaultSkyCol = "Textures/skycol.png";
        private static string FinalSkyCol = "Textures/skycolfinal.png";

        private VertexBuffer Verts;
        private IndexBuffer Indices;
        private Texture2D GradTex;
        private int PrimCount;
        private float LastSkyPos;

        private VertexPositionTexture[] VertexData;
        private int[] IndexData;

        private bool IsFinal;

        public AbstractSkyDome(GraphicsDevice GD, float time)
        {
            float? customSky = DynamicTuning.Global?.GetTuning("city", 0, 2);

            if (!customSky.HasValue || !TryLoadSkyColor(GD, $"Textures/skycol_alt{(int)customSky}.png"))
            {
                TryLoadSkyColor(GD, DefaultSkyCol);
            }

            LoadFinalIfNeeded(GD);

            InitArrays();
            BuildSkyDome(GD, time);
        }

        public void LoadFinalIfNeeded(GraphicsDevice GD)
        {
            bool needsFinal = FinaleUtils.IsFinale();

            if (!IsFinal && needsFinal)
            {
                using (var file = File.OpenRead(Path.Combine(FSOEnvironment.ContentDir, FinalSkyCol)))
                {
                    GradTex = ImageLoader.FromStream(GD, file);
                };

                IsFinal = true;
            }
        }

        private bool TryLoadSkyColor(GraphicsDevice GD, string path)
        {
            try
            {
                using (var file = File.OpenRead(Path.Combine(FSOEnvironment.ContentDir, path)))
                {
                    GradTex = ImageLoader.FromStream(GD, file);
                };
            }
            catch
            {
                return false;
            }

            return true;
        }

        private float[] m_SkyColors = new float[]
        {
            4/8f,
            4/8f,
            4/8f,
            5/8f,
            6/8f, //sunrise
            7/8f,
            8/8f, //peak
            0/8f, //peak
            0/8f,
            0/8f,
            1/8f, //sunset
            2/8f,
            3/8f,
            4/8f,
        };

        public float OutsideSkyP(float time)
        {
            double Progress = (time * (m_SkyColors.Length - 1)) % 1; //interpolation progress (mod 1)
            var sky1 = m_SkyColors [(int)Math.Floor(time * (m_SkyColors.Length - 1))]; //first colour
            var sky2 = m_SkyColors [(int)Math.Floor(time * (m_SkyColors.Length - 1)) + 1]; //second colour
            if (sky1 == 1f && sky2 == 0f) Progress = 0;
            return (float)Progress* sky2 + (1 - (float)Progress) * sky1;
        }

        private float DayOffset = 0.25f;
        private float DayDuration = 0.60f;

        public bool Night(float tod)
        {
            bool night = false;
            if (tod < DayOffset)
            {
                night = true;
            }
            else if (tod > DayOffset + DayDuration)
            {
                night = true;
            }
            return night;
        }

        private void InitArrays()
        {
            var subdivs = 65;

            int vertCount = (subdivs - 1) * (subdivs + 1) + 1;
            int indexCount = ((subdivs - 1) * (subdivs * 6 - 3)) - 3;

            VertexData = new VertexPositionTexture[vertCount];
            IndexData = new int[indexCount];
        }

        public void BuildSkyDome(GraphicsDevice GD, float time)
        {
            LoadFinalIfNeeded(GD);

            //generate sky dome geometry
            var subdivs = 65;
            VertexPositionTexture[] verts = VertexData;
            int[] indices = IndexData;
            var skyCol = new Color(0x00, 0x80, 0xFF, 0xFF);

            float skyPos = OutsideSkyP(time);
            LastSkyPos = time;
            skyPos += 1 / 16f;
            int vertLastStart = 0;
            int vertLastLength = 1;
            var yGap = 1f / GradTex.Height;
            var range = 1 - yGap;
            var topRange = 0.9f * range;
            var btmRange = 0.1f * range;

            int vi = 0;
            int ii = 0;

            verts[vi++] = new VertexPositionTexture(new Vector3(0, 1, 0), new Vector2(skyPos, yGap));

            for (int y = 1; y < subdivs; y++)
            {
                int start = vi;
                var angley = (float)Math.PI * y / ((float)subdivs - 1);
                var radius = (float)Math.Sin(angley);
                var height = Math.Cos(angley);
                //var aheight = (height < -0.6f)?((-0.12f) - height):height;
                //var tpos = (0.9f - (float)Math.Sqrt(Math.Abs(aheight)) * 0.9f);
                
                var tpos = (float)((height > 0) ? (0.9f - Math.Sqrt(height) * topRange) : 0.9f + Math.Sqrt(-height) * btmRange);

                for (int x = 0; x < subdivs + 1; x++)
                {
                    var anglex = (float)Math.PI * x * 2 / (float)subdivs;
                    var colLerp = Math.Min(1, Math.Abs(((y - 2) / (float)subdivs) - 0.60f) * 4);
                    verts[vi++] = new VertexPositionTexture(new Vector3((float)Math.Sin(anglex) * radius, (float)height, (float)Math.Cos(anglex) * radius), new Vector2(skyPos, tpos));
                    if (x < subdivs)
                    {
                        if (y != 1)
                        {
                            indices[ii++] = vertLastStart + x % vertLastLength;
                            indices[ii++] = vertLastStart + (x + 1) % vertLastLength;
                            indices[ii++] = vi - 1;
                        }

                        indices[ii++] = vertLastStart + (x + 1) % vertLastLength;
                        indices[ii++] = vi;
                        indices[ii++] = vi - 1;
                    }
                }
                vertLastStart = start;
                vertLastLength = subdivs + 1;
            }

            if (Verts == null) Verts = new VertexBuffer(GD, typeof(VertexPositionTexture), vi, BufferUsage.None);
            Verts.SetData(verts);
            if (Indices == null)
            {
                Indices = new IndexBuffer(GD, IndexElementSize.ThirtyTwoBits, ii, BufferUsage.None);
                Indices.SetData(indices);
                PrimCount = ii / 3;
            }
        }

        public Vector4 FogColor;
        public RasterizerState ClipState = new RasterizerState()
        {
            CullMode = CullMode.None,
            DepthClipEnable = false
        };

        public void Draw(GraphicsDevice gd, Color outsideColor, Matrix view, Matrix projection, float time, WeatherController weather, Vector3 sunVector, float scale)
        {
            var ocolor = outsideColor.ToVector4();
            var effect = WorldContent.GetBE(gd);

            if (LastSkyPos != time) BuildSkyDome(gd, time);

            var color = ocolor - new Vector4(0.35f) * 1.5f + new Vector4(0.35f);
            color.W = 1;
            var wint = Math.Min(1f, weather.WeatherIntensity);

            effect.LightingEnabled = false;
            effect.Texture = GradTex;
            effect.Alpha = (1 - (float)Math.Sqrt(wint) * 0.75f);
            effect.DiffuseColor = Vector3.One;
            effect.AmbientLightColor = Vector3.One;
            //effect.DiffuseColor = new Vector3(Math.Min(1, color.X), Math.Min(1, color.Y), Math.Min(1, color.Z));
            //effect.AmbientLightColor = new Vector3(color.X, color.Y, color.Z);
            effect.VertexColorEnabled = false;
            effect.TextureEnabled = true;

            //var view = view;state.Camera.View;
            view.M41 = 0; view.M42 = 0; view.M43 = 0;
            effect.View = view;
            effect.Projection = projection;// (state.Camera as WorldCamera3D)?.BaseProjection() ?? state.Camera.Projection;
            effect.World = Matrix.CreateScale(5f * scale);
            gd.DepthStencilState = DepthStencilState.None;
            gd.RasterizerState = RasterizerState.CullNone;
            gd.BlendState = BlendState.AlphaBlend;
            gd.SamplerStates[0] = SamplerState.LinearWrap;

            foreach (var pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                gd.Indices = Indices;
                gd.SetVertexBuffer(Verts);

                gd.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, PrimCount);
            }

            gd.BlendState = BlendState.NonPremultiplied;
            var night = Night((float)FinaleUtils.BiasSunTime(time));
            //draw the sun or moon
            var pos = sunVector;
            var z = -pos.X;
            pos.X = pos.Z;
            pos.Z = z;
            var dist = 0.5f + pos.Y * 2;
            dist *= dist;
            dist += 0.5f;
            if (night) dist = 35;
            var sunMat = Matrix.CreateTranslation(0, 0, dist) * Matrix.CreateBillboard(pos, new Vector3(0, 0.4f, 0), Vector3.Up, null);

            var geom = WorldContent.GetTextureVerts(gd);
            effect.World = sunMat;
            effect.VertexColorEnabled = false;
            effect.TextureEnabled = true;
            effect.Texture = (night) ? TextureGenerator.GetMoon(gd) : TextureGenerator.GetSun(gd);
            effect.DiffuseColor = FinaleUtils.BiasSunIntensity(new Vector3(color.X, color.Y, color.Z) * ((night) ? 2f : 0.6f), time);
            gd.BlendState = (night) ? BlendState.NonPremultiplied : BlendState.Additive;

            foreach (var pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                gd.SetVertexBuffer(geom);
                gd.DrawPrimitives(PrimitiveType.TriangleStrip, 0, 2);
            }

            gd.BlendState = BlendState.NonPremultiplied;
            gd.DepthStencilState = DepthStencilState.Default;
            effect.Alpha = 1f;
        }

        public void Dispose()
        {
            Verts?.Dispose();
            Indices?.Dispose();
            GradTex?.Dispose();
        }
    }
}
