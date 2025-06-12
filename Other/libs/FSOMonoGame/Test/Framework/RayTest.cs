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
using NUnit.Framework;

namespace MonoGame.Tests.Framework
{
    class RayTest
    {
        [Test]
        public void BoundingBoxIntersects()
        {
            // Our test box.
            BoundingBox box;
            box.Min = new Vector3(-10,-20,-30);
            box.Max = new Vector3(10, 20, 30);
            var center = (box.Max + box.Min) * 0.5f;

            // Test misses.
            Assert.IsNull(new Ray(center - Vector3.UnitX * 40, -Vector3.UnitX).Intersects(box));
            Assert.IsNull(new Ray(center + Vector3.UnitX * 40, Vector3.UnitX).Intersects(box));
            Assert.IsNull(new Ray(center - Vector3.UnitY * 40, -Vector3.UnitY).Intersects(box));
            Assert.IsNull(new Ray(center + Vector3.UnitY * 40, Vector3.UnitY).Intersects(box));
            Assert.IsNull(new Ray(center - Vector3.UnitZ * 40, -Vector3.UnitZ).Intersects(box));
            Assert.IsNull(new Ray(center + Vector3.UnitZ * 40, Vector3.UnitZ).Intersects(box));

            // Test middle of each face.
            Assert.AreEqual(30.0f, new Ray(center - Vector3.UnitX * 40, Vector3.UnitX).Intersects(box));
            Assert.AreEqual(30.0f, new Ray(center + Vector3.UnitX * 40, -Vector3.UnitX).Intersects(box));
            Assert.AreEqual(20.0f, new Ray(center - Vector3.UnitY * 40, Vector3.UnitY).Intersects(box));
            Assert.AreEqual(20.0f, new Ray(center + Vector3.UnitY * 40, -Vector3.UnitY).Intersects(box));
            Assert.AreEqual(10.0f, new Ray(center - Vector3.UnitZ * 40, Vector3.UnitZ).Intersects(box));
            Assert.AreEqual(10.0f, new Ray(center + Vector3.UnitZ * 40, -Vector3.UnitZ).Intersects(box));

            // Test the corners along each axis.
            Assert.AreEqual(10.0f, new Ray(box.Min - Vector3.UnitX * 10, Vector3.UnitX).Intersects(box));
            Assert.AreEqual(10.0f, new Ray(box.Min - Vector3.UnitY * 10, Vector3.UnitY).Intersects(box));
            Assert.AreEqual(10.0f, new Ray(box.Min - Vector3.UnitZ * 10, Vector3.UnitZ).Intersects(box));
            Assert.AreEqual(10.0f, new Ray(box.Max + Vector3.UnitX * 10, -Vector3.UnitX).Intersects(box));
            Assert.AreEqual(10.0f, new Ray(box.Max + Vector3.UnitY * 10, -Vector3.UnitY).Intersects(box));
            Assert.AreEqual(10.0f, new Ray(box.Max + Vector3.UnitZ * 10, -Vector3.UnitZ).Intersects(box));

            // Test inside out.
            Assert.AreEqual(0.0f, new Ray(center, Vector3.UnitX).Intersects(box));
            Assert.AreEqual(0.0f, new Ray(center, -Vector3.UnitX).Intersects(box));
            Assert.AreEqual(0.0f, new Ray(center, Vector3.UnitY).Intersects(box));
            Assert.AreEqual(0.0f, new Ray(center, -Vector3.UnitY).Intersects(box));
            Assert.AreEqual(0.0f, new Ray(center, Vector3.UnitZ).Intersects(box));
            Assert.AreEqual(0.0f, new Ray(center, -Vector3.UnitZ).Intersects(box));
        }

#if !XNA
        [Test]
        public void Deconstruct()
        {
            Ray ray = new Ray(Vector3.Backward, Vector3.Right);

            Vector3 position, direction;

            ray.Deconstruct(out position, out direction);

            Assert.AreEqual(position, ray.Position);
            Assert.AreEqual(direction, ray.Direction);
        }
#endif
    }
}
