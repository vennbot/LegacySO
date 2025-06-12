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

using Microsoft.Xna.Framework.Content.Pipeline.Processors;

namespace Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler
{
    [ContentTypeWriter]
    class ModelWriter : BuiltInContentWriter<ModelContent>
    {
        protected internal override void Write(ContentWriter output, ModelContent value)
        {
            WriteBones(output, value.Bones);

            output.Write((uint)value.Meshes.Count);
            foreach (var mesh in value.Meshes)
            {
                output.WriteObject(mesh.Name);
                WriteBoneReference(output, mesh.ParentBone, value.Bones);
                output.Write(mesh.BoundingSphere);
                output.WriteObject(mesh.Tag);

                output.Write((uint)mesh.MeshParts.Count);
                foreach (var part in mesh.MeshParts)
                {
                    output.Write((uint)part.VertexOffset);
                    output.Write((uint)part.NumVertices);
                    output.Write((uint)part.StartIndex);
                    output.Write((uint)part.PrimitiveCount);
                    output.WriteObject(part.Tag);

                    output.WriteSharedResource(part.VertexBuffer);
                    output.WriteSharedResource(part.IndexBuffer);
                    output.WriteSharedResource(part.Material);
                }
            }

            WriteBoneReference(output, value.Root, value.Bones);
            output.WriteObject(value.Tag);
        }

        private void WriteBones(ContentWriter output, ModelBoneContentCollection bones)
        {
            output.Write((uint)bones.Count);

            // Bone properties
            foreach (var bone in bones)
            {
                output.WriteObject(bone.Name);
                output.Write(bone.Transform);
            }

            // Hierarchy
            foreach (var bone in bones)
            {
                WriteBoneReference(output, bone.Parent, bones);

                output.Write((uint)bone.Children.Count);
                foreach (var child in bone.Children)
                    WriteBoneReference(output, child, bones);
            }
        }

        private void WriteBoneReference(ContentWriter output, ModelBoneContent bone, ModelBoneContentCollection bones)
        {
            var boneCount = bones != null ? bones.Count : 0;
            var boneId = bone != null
                             ? bone.Index + 1
                             : 0;

            if (boneCount < 255)
                output.Write((byte)boneId);
            else
                output.Write((uint)boneId);
        }
    }
}
