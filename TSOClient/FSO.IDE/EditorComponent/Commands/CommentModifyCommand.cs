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

namespace FSO.IDE.EditorComponent.Commands
{
    public class CommentModifyCommand : BHAVCommand
    {
        public PrimitiveBox Box;
        public string OldComment;
        public string NewComment;

        public CommentModifyCommand(PrimitiveBox box, string value)
        {
            Box = box;
            OldComment = box.TreeBox.Comment;
            NewComment = value;
        }

        public void NotifyGotos()
        {
            foreach (var prim in Box.Master.Primitives)
            {
                if (prim.Type == TREEBoxType.Goto)
                {
                    if (prim.TreeBox.TruePointer == Box.TreeBox.InternalID) prim.UpdateGotoLabel();
                }
            }
        }

        public override void Execute(BHAV bhav, UIBHAVEditor editor)
        {
            var tree = editor.GetSavableTree();
            Box.SetComment(NewComment);
            Box.ApplyBoxPositionCentered();
            NotifyGotos();
            Content.Content.Get().Changes.ChunkChanged(tree);
        }

        public override void Undo(BHAV bhav, UIBHAVEditor editor)
        {
            var tree = editor.GetSavableTree();
            Box.SetComment(OldComment);
            Box.ApplyBoxPositionCentered();
            NotifyGotos();
            Content.Content.Get().Changes.ChunkChanged(tree);
        }
    }
}
