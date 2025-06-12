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
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using FSO.Common.Rendering.Framework.Model;
using FSO.Common;

namespace FSO.Client.UI.Framework
{
    public class UICachedContainer : UIContainer
    {
        public bool UseMultisample;
        public bool Invalidated;
        protected RenderTarget2D Target;
        public UIContainer DynamicOverlay = new UIContainer();
        public Point BackOffset;
        public Color ClearColor = Color.TransparentBlack;
        public bool UseMip;
        public bool UseZ;
        public bool InternalBefore;

        public UICachedContainer()
        {
            Add(DynamicOverlay);
            InvalidationParent = this;
        }

        public override void PreDraw(UISpriteBatch batch)
        {
            //If our matrix is dirty, recalculate it
            if (_MtxDirty)
            {
                CalculateMatrix();
            }

            if (!Visible)
            {
                return;
            }

            var gd = batch.GraphicsDevice;
            if (Invalidated)
            {
                var size = Size * Scale;
                if (Target == null || (int)size.X != Target.Width || (int)size.Y != Target.Height)
                {
                    Target?.Dispose();
                    Target = new RenderTarget2D(gd, (int)size.X, (int)size.Y, UseMip, SurfaceFormat.Color, (UseZ)?DepthFormat.Depth24:DepthFormat.None, (UseMultisample && !FSOEnvironment.DirectX)?4:0, RenderTargetUsage.PreserveContents);
                }

                lock (Children)
                {
                    foreach (var child in Children)
                    {
                        if (child == DynamicOverlay) continue;
                        child.PreDraw(batch);
                    }
                }

                try { batch.End(); } catch { }
                gd.SetRenderTarget(Target);

                gd.Clear(ClearColor);
                var pos = LocalPoint(0, 0);

                var mat = Microsoft.Xna.Framework.Matrix.CreateTranslation(-(pos.X), -(pos.Y), 0) *
                    Microsoft.Xna.Framework.Matrix.CreateScale(1f) *
                    Microsoft.Xna.Framework.Matrix.CreateTranslation(
                        BackOffset.X * FSOEnvironment.DPIScaleFactor, 
                        BackOffset.Y * FSOEnvironment.DPIScaleFactor, 0);

                batch.BatchMatrixStack.Push(mat);

                batch.Begin(transformMatrix: mat, blendState: BlendState.AlphaBlend, sortMode: SpriteSortMode.Deferred, rasterizerState: RasterizerState.CullNone);
                batch.GraphicsDevice.RasterizerState = RasterizerState.CullNone;
                if (InternalBefore) InternalDraw(batch);
                lock (Children)
                {
                    foreach (var child in Children)
                    {
                        if (child == DynamicOverlay) continue;
                        child.Draw(batch);
                    }
                }
                if (!InternalBefore) InternalDraw(batch);
                batch.BatchMatrixStack.Pop();
                batch.End();
                gd.SetRenderTarget(null);
                Invalidated = false;
            }
            DynamicOverlay.PreDraw(batch);
        }

        public virtual void InternalDraw(UISpriteBatch batch)
        {

        }

        public override Rectangle GetBounds()
        {
            return new Rectangle(0, 0, (int)Size.X, (int)Size.Y);
        }

        public override void Update(UpdateState state)
        {
            BaseUpdate(state);
            lock (Children)
            {
                //todo: why are all these locks here, and what kind of problems might that cause
                foreach (var child in GetChildrenSafe())
                {
                    if (child != DynamicOverlay)
                        child.Update(state);
                }
            }
            DynamicOverlay.Update(state);
        }

        public override void Draw(UISpriteBatch batch)
        {
            if (!Visible) return;
            if (Target != null)
            {
                DrawLocalTexture(batch, Target, null, -BackOffset.ToVector2(), new Vector2(1/(Scale.X), 1/(Scale.Y)));
            }
            DynamicOverlay.Draw(batch);
        }

        private Vector2 _Size;
        public override Vector2 Size
        {
            get
            {
                return _Size;
            }

            set
            {
                Invalidate();
                Invalidated = true;
                _Size = value;
            }
        }

        public override void Removed()
        {
            Target?.Dispose();
            Target = null;
            base.Removed();
        }
    }
}
