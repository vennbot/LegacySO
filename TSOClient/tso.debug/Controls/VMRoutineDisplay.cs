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
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FSO.SimAntics;
using System.Collections;
using FSO.SimAntics.Engine;

namespace FSO.Debug.Controls
{
    public partial class VMRoutineDisplay : UserControl
    {
        public VMRoutineDisplay()
        {
            InitializeComponent();
        }

        private void InvalidateRoutine()
        {
            IList<VMInstructionDisplay> items = new List<VMInstructionDisplay>();
            if (_Routine != null)
            {
                foreach (var inst in _Routine.Instructions){
                    items.Add(new VMInstructionDisplay(inst));
                }
            }
            grid.DataSource = items;
        }

        private VMRoutine _Routine;
        public VMRoutine Routine
        {
            get{
                return _Routine;
            }
            set{
                _Routine = value;
                InvalidateRoutine();
            }
        }
    }

    public class VMInstructionDisplay
    {
        private VMInstruction Instruction;
        private VMPrimitiveRegistration Primitive;
        public VMInstructionDisplay(VMInstruction inst){
            //Routine no longer has an instance of VM.
            //this.Primitive = inst.Function.VM.Context.Primitives[inst.Opcode];
            //vm.Context.GetPrimitive(inst.Opcode)
            this.Instruction = inst;
        }


        public string Index {
            get{
                return Instruction.Index.ToString();
            }
        }

        public object Margin
        {
            get
            {
                return null;
            }
        }

        public string Opcode
        {
            get {
                if (this.Primitive != null)
                {
                    return this.Primitive.Name;
                }
                return this.Instruction.Opcode.ToString();
            }
        }


        public string Operand
        {
            get
            {
                if (this.Instruction.Operand != null){
                    return this.Instruction.Operand.ToString();
                }
                return "";
            }
        }
    }
}
