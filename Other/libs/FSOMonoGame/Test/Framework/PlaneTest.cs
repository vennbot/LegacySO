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
    public class PlaneTest
    {
        [Test]
        public void TransformByMatrix()
        {
            // Our test plane.
            var plane = Plane.Normalize(new Plane(new Vector3(0, 1, 1), 2.5f));

            // Our matrix.
            var matrix = Matrix.CreateRotationX(MathHelper.PiOver2);

            // Test transform.
            var expectedResult = new Plane(new Vector3(0, -0.7071068f, 0.7071067f), 1.767767f);
            Assert.That(Plane.Transform(plane, matrix), Is.EqualTo(expectedResult).Using(PlaneComparer.Epsilon));
        }

        [Test]
        public void TransformByRefMatrix()
        {
            // Our test plane.
            var plane = Plane.Normalize(new Plane(new Vector3(1, 0.8f, 0.5f), 2.5f));
            var originalPlane = plane;

            // Our matrix.
            var matrix = Matrix.CreateRotationX(MathHelper.PiOver2);
            var originalMatrix = matrix;

            // Test transform.
            Plane result;
            Plane.Transform(ref plane, ref matrix, out result);

            var expectedResult = new Plane(new Vector3(0.7273929f, -0.3636965f, 0.5819144f), 1.818482f);
            Assert.That(result, Is.EqualTo(expectedResult).Using(PlaneComparer.Epsilon));

            Assert.That(plane, Is.EqualTo(originalPlane));
            Assert.That(matrix, Is.EqualTo(originalMatrix));
        }

        [Test]
        public void TransformByQuaternion()
        {
            // Our test plane.
            var plane = Plane.Normalize(new Plane(new Vector3(0, 1, 1), 2.5f));

            // Our quaternion.
            var quaternion = Quaternion.CreateFromAxisAngle(Vector3.UnitX, MathHelper.PiOver2);

            // Test transform.
            var expectedResult = new Plane(new Vector3(0, -0.7071068f, 0.7071067f), 1.767767f);
            Assert.That(Plane.Transform(plane, quaternion), Is.EqualTo(expectedResult).Using(PlaneComparer.Epsilon));
        }

        [Test]
        public void TransformByRefQuaternion()
        {
            // Our test plane.
            var plane = Plane.Normalize(new Plane(new Vector3(1, 0.8f, 0.5f), 2.5f));
            var originalPlane = plane;

            // Our quaternion.
            var quaternion = Quaternion.CreateFromAxisAngle(Vector3.UnitX, MathHelper.PiOver2);
            var originalQuaternion = quaternion;

            // Test transform.
            Plane result;
            Plane.Transform(ref plane, ref quaternion, out result);

            var expectedResult = new Plane(new Vector3(0.7273929f, -0.3636965f, 0.5819144f), 1.818482f);
            Assert.That(result, Is.EqualTo(expectedResult).Using(PlaneComparer.Epsilon));

            Assert.That(plane, Is.EqualTo(originalPlane));
            Assert.That(quaternion, Is.EqualTo(originalQuaternion));
        }

#if !XNA
        [Test]
        public void Deconstruct()
        {
            Plane plane = new Plane(new Vector3(255, 255, 255), float.MaxValue);

            Vector3 normal;
            float d;

            plane.Deconstruct(out normal, out d);

            Assert.AreEqual(normal, plane.Normal);
            Assert.AreEqual(d, plane.D);
        }
#endif
    }
}
