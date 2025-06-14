
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
using FSO.Files.Formats.IFF;
using System;
using System.Threading;

namespace FSO.Content
{
    public class ResAction
    {
        public delegate void UIResActionDelegate();
        private UIResActionDelegate Action;
        private IffChunk Chunk;
        private AutoResetEvent Signal;
        private bool CausesChange;

        public ResAction(UIResActionDelegate action) : this(action, null, true, null) { }
        public ResAction(UIResActionDelegate action, IffChunk chunk) : this(action, chunk, true, null) { }

        public ResAction(UIResActionDelegate action, IffChunk chunk, bool causesChange) : this(action, chunk, causesChange, null) { }

        public ResAction(UIResActionDelegate action, IffChunk chunk, bool causesChange, AutoResetEvent signal)
        {
            Action = action;
            Chunk = chunk;
            Signal = signal;
            CausesChange = causesChange;
        }

        public void SetSignal(AutoResetEvent signal)
        {
            Signal = signal;
        }

        public void Execute()
        {
            if (Chunk != null)
            {
                lock (Chunk)
                {
                    try
                    {
                        if (CausesChange)
                            Content.Get().Changes.ChunkChanged(Chunk);
                        Action();
                    }
                    catch (Exception)
                    {}
                }
            } else
            {
                try
                { Action(); }
                catch (Exception)
                { }
            }
            if (Signal != null) Signal.Set();
        }
    }
}
