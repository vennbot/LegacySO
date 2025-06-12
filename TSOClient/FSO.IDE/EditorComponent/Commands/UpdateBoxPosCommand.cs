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
using FSO.Files.Formats.IFF.Chunks;
using FSO.IDE.EditorComponent.UI;
using Microsoft.Xna.Framework;

namespace FSO.IDE.EditorComponent.Commands
{
    public class UpdateBoxPosCommand : BHAVCommand
    {
        PrimitiveBox Box;
        public Point BeforePos;
        public Point BeforeSize;
        public Point AfterPos;
        public Point AfterSize;

        public UpdateBoxPosCommand(PrimitiveBox box)
        {
            Box = box;
            BeforePos = new Point(box.TreeBox.X, box.TreeBox.Y);
            BeforeSize = new Point(box.TreeBox.Width, box.TreeBox.Height);
            AfterPos = box.Position.ToPoint();
            AfterSize = new Point(box.Width, box.Height);
        }

        public override void Execute(BHAV bhav, UIBHAVEditor editor)
        {
            var tree = editor.GetSavableTree();
            Box.TreeBox.X = (short)AfterPos.X;
            Box.TreeBox.Y = (short)AfterPos.Y;
            Box.TreeBox.Width = (short)AfterSize.X;
            Box.TreeBox.Height = (short)AfterSize.Y;
            Content.Content.Get().Changes.ChunkChanged(tree);
        }

        public override void Undo(BHAV bhav, UIBHAVEditor editor)
        {
            var tree = editor.GetSavableTree();
            Box.TreeBox.X = (short)BeforePos.X;
            Box.TreeBox.Y = (short)BeforePos.Y;
            Box.TreeBox.Width = (short)BeforeSize.X;
            Box.TreeBox.Height = (short)BeforeSize.Y;
            Box.ApplyBoxPositionCentered();
            Content.Content.Get().Changes.ChunkChanged(tree);
        }
    }
}
