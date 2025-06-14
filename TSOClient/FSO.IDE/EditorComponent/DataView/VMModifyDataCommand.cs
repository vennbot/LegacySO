
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
using FSO.SimAntics;
using FSO.SimAntics.Engine;
using FSO.SimAntics.Model;

namespace FSO.IDE.EditorComponent.DataView
{
    public class VMModifyDataCommand
    {
        public VMExtDataType Type;
        public int ID;
        public int Value;

        public VMEntity Object;
        public VMStackFrame Frame;

        public VMModifyDataCommand(VMExtDataType type, int id, int value, VMEntity obj)
        {
            Type = type;
            ID = id;
            Value = value;
            Object = obj;
        }

        public VMModifyDataCommand(VMExtDataType type, int id, int value, VMEntity obj, VMStackFrame stack) 
            : this(type, id, value, obj)
        {
            Frame = stack;
        }

        public void Execute()
        {
            switch (Type)
            {
                case VMExtDataType.StackObject:
                    Frame.StackObject = Frame.VM.GetObjectById((short)Value);
                    break;
                case VMExtDataType.Parameter:
                    Frame.Args[ID] = (short)Value;
                    break;
                case VMExtDataType.Local:
                    Frame.Locals[ID] = (short)Value;
                    break;
                case VMExtDataType.Attributes:
                    Object.SetAttribute((ushort)ID, (short)Value);
                    break;
                case VMExtDataType.Temp:
                    Object.Thread.TempRegisters[ID] = (short)Value;
                    break;
                case VMExtDataType.TempXL:
                    Object.Thread.TempXL[ID] = Value;
                    break;
                case VMExtDataType.ObjectData:
                    Object.SetValue((VMStackObjectVariable)ID, (short)Value);
                    break;
                case VMExtDataType.PersonData:
                    ((VMAvatar)Object).SetPersonData((VMPersonDataVariable)ID, (short)Value);
                    break;
                case VMExtDataType.Motives:
                    ((VMAvatar)Object).SetMotiveData((VMMotive)ID, (short)Value);
                    break;
                case VMExtDataType.Globals:
                    //maybe make this less unbelievably ugly
                    Object.Thread.Context.VM.SetGlobalValue((ushort)ID, (short)Value);
                    break;
            }
        }
    }
}
