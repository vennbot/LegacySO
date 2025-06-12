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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Iffinator.Flash;

namespace Iffinator
{
    public partial class BHAVEdit : Form
    {
        private BHAV m_CurrentBHAV;
        private BHAVAnalyzer m_Analyzer;
        private List<IFFDecode> m_DecodedInstructions = new List<IFFDecode>();

        public BHAVEdit(Iff IffFile, BHAV CurrentBHAV)
        {
            InitializeComponent();

            m_CurrentBHAV = CurrentBHAV;
            m_Analyzer = new BHAVAnalyzer(IffFile);

            foreach (byte[] Instruction in m_CurrentBHAV.Instructions)
            {
                IFFDecode DecodedInstruction = new IFFDecode(Instruction);
                m_Analyzer.DecodeInstruction(ref DecodedInstruction);
                
                m_DecodedInstructions.Add(DecodedInstruction);
                LstInstructions.Items.Add(DecodedInstruction.OutStream.ToString());
            }
        }
    }
}
