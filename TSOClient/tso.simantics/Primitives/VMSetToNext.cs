
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
using System.Linq;
using FSO.SimAntics.Engine;
using FSO.Files.Utils;
using FSO.SimAntics.Engine.Scopes;
using FSO.SimAntics.Engine.Utils;
using Microsoft.Xna.Framework;
using System.IO;
using FSO.LotView.Model;

namespace FSO.SimAntics.Primitives
{

    public class VMSetToNext : VMPrimitiveHandler
    {
        //position steps for object adjacent to object in local
        private static Point[] AdjStep =
        {
            new Point(0, -1),
            new Point(1, 0),
            new Point(0, 1),
            new Point(-1, 0),
        };
        public override VMPrimitiveExitCode Execute(VMStackFrame context, VMPrimitiveOperand args)
        {
            var operand = (VMSetToNextOperand)args;
            var targetValue = VMMemory.GetVariable(context, operand.TargetOwner, operand.TargetData);
            var entities = context.VM.Entities;

            VMEntity Pointer = context.VM.GetObjectById(targetValue);

            //re-evaluation of what this actually does:
            //tries to find the next object id (from the previous) that meets a specific condition.
            //the previous object id is supplied via the target variable
            //
            //we should take the first result with object id > targetValue.

            if (operand.SearchType == VMSetToNextSearchType.PartOfAMultipartTile) {
                var result = MultitilePart(context, Pointer, targetValue);
                if (result == 0) return VMPrimitiveExitCode.GOTO_FALSE;
                VMMemory.SetVariable(context, operand.TargetOwner, operand.TargetData, result);
                return VMPrimitiveExitCode.GOTO_TRUE;
            }
            else if (operand.SearchType == VMSetToNextSearchType.ObjectAdjacentToObjectInLocal)
            {
                var result = AdjToLocal(context, Pointer, operand.Local);
                if (result == null) return VMPrimitiveExitCode.GOTO_FALSE;
                else
                {
                    VMMemory.SetVariable(context, operand.TargetOwner, operand.TargetData, result.ObjectID);
                    return VMPrimitiveExitCode.GOTO_TRUE;
                }
            }
            else if (operand.SearchType == VMSetToNextSearchType.Career)
            {
                var next = Content.Content.Get().Jobs.SetToNext(targetValue);
                if (next < 0) return VMPrimitiveExitCode.GOTO_FALSE;
                VMMemory.SetVariable(context, operand.TargetOwner, operand.TargetData, next);
                return VMPrimitiveExitCode.GOTO_TRUE;
            }
            else if (operand.SearchType == VMSetToNextSearchType.NeighborId)
            {
                var next = Content.Content.Get().Neighborhood.SetToNext(targetValue);
                if (next < 0) return VMPrimitiveExitCode.GOTO_FALSE;
                VMMemory.SetVariable(context, operand.TargetOwner, operand.TargetData, next);
                return VMPrimitiveExitCode.GOTO_TRUE;
            }
            else if (operand.SearchType == VMSetToNextSearchType.NeighborOfType)
            {
                var next = Content.Content.Get().Neighborhood.SetToNext(targetValue, operand.GUID);
                if (next < 0) return VMPrimitiveExitCode.GOTO_FALSE;
                VMMemory.SetVariable(context, operand.TargetOwner, operand.TargetData, next);
                return VMPrimitiveExitCode.GOTO_TRUE;
            } else {

                //if we've cached the search type, use that instead of all objects
                switch (operand.SearchType)
                {
                    case VMSetToNextSearchType.ObjectOnSameTile:
                        if (Pointer == null) return VMPrimitiveExitCode.GOTO_FALSE; // verified in ts1
                        entities = context.VM.Context.ObjectQueries.GetObjectsAt(Pointer.Position); break;
                    case VMSetToNextSearchType.Person:
                    case VMSetToNextSearchType.FamilyMember:
                        entities = context.VM.Context.ObjectQueries.Avatars; break;
                    case VMSetToNextSearchType.ObjectOfType:
                        entities = context.VM.Context.ObjectQueries.GetObjectsByGUID(operand.GUID); break;
                    case VMSetToNextSearchType.ObjectWithCategoryEqualToSP0:
                        entities = context.VM.Context.ObjectQueries.GetObjectsByCategory(context.Args[0]); break;
                    case VMSetToNextSearchType.FSOObjectOfSemiGlobal:
                        var sg = FSO.Content.Content.Get().WorldObjects.Get(operand.GUID).Resource.SemiGlobal;

                        if (sg != null)
                        {
                            string sg_name = sg.Iff.Filename; //retrieve semiglobal's iff filename
                            if (sg_name != null) //sanity check
                            {
                                entities = context.VM.Context.ObjectQueries.GetObjectsBySemiGlobal(sg_name.ToLowerInvariant());
                            }
                            else entities = null;
                        }
                        else entities = null;
                        break;
                    default:
                        break;
                }
                if (entities == null) return VMPrimitiveExitCode.GOTO_FALSE;

                bool loop = (operand.SearchType == VMSetToNextSearchType.ObjectOnSameTile) || (operand.SearchType == VMSetToNextSearchType.FamilyMember);

                var ind = VM.FindNextIndexInObjList(entities, targetValue);
                for (int i=ind; i<entities.Count; i++) //generic search through all objects
                {
                    var temp = entities[i];
                    bool found = false;
                    if (temp.ObjectID > targetValue || loop)
                    {
                        switch (operand.SearchType)
                        { //manual search types
                            case VMSetToNextSearchType.NonPerson:
                                found = (temp is VMGameObject);
                                break;
                            case VMSetToNextSearchType.ClosestHouse:
                                return VMPrimitiveExitCode.GOTO_FALSE;
                                throw new VMSimanticsException("Not implemented!", context);
                            case VMSetToNextSearchType.FamilyMember:
                                found = ((VMAvatar)temp).GetPersonData(Model.VMPersonDataVariable.TS1FamilyNumber) ==
                                        ((VMAvatar)Pointer).GetPersonData(Model.VMPersonDataVariable.TS1FamilyNumber);
                                // context.VM.TS1State.CurrentFamily?.FamilyGUIDs?.Contains(((VMAvatar)temp).Object.OBJ.GUID) ?? false;
                                break;
                            default:
                                //set to next object, or cached search.
                                found = true; break;
                        }
                        /*
                        if (temp.ObjectID <= targetValue && found)
                        {
                            //remember the first element in case we need to loop back to it (set to next tile on same location)
                            if (first == null) first = temp; 
                            found = false;
                        }*/
                    }
                    if (found)
                    {
                        VMMemory.SetVariable(context, operand.TargetOwner, operand.TargetData, temp.ObjectID);
                        return VMPrimitiveExitCode.GOTO_TRUE;
                    }
                }

                if (loop)
                {
                    VMEntity first = entities.FirstOrDefault();
                    if (operand.SearchType == VMSetToNextSearchType.FamilyMember)
                    {
                        short familyNumber = ((VMAvatar)Pointer).GetPersonData(Model.VMPersonDataVariable.TS1FamilyNumber);
                        foreach (VMEntity ent in entities)
                        {
                            if (((VMAvatar)ent).GetPersonData(Model.VMPersonDataVariable.TS1FamilyNumber) == familyNumber)
                            {
                                first = ent;
                                break;
                            }
                        }
                    }
                    if (first == null || !entities.Contains(Pointer)) return VMPrimitiveExitCode.GOTO_FALSE; //no elements of this kind at all.
                    else
                    {
                        VMMemory.SetVariable(context, operand.TargetOwner, operand.TargetData, first.ObjectID); //set to loop, so go back to lowest obj id.
                        return VMPrimitiveExitCode.GOTO_TRUE;
                    }
                    //loop around
                }

            }
            return VMPrimitiveExitCode.GOTO_FALSE; //ran out of objects to test
        }

        public static short MultitilePart(VMStackFrame context, VMEntity pointer, short targetValue)
        {
            if (pointer == null || (!pointer.MultitileGroup.MultiTile)) return 0; //single part
            else
            {
                var group = pointer.MultitileGroup.Objects;
                bool found = false;
                short bestID = 0;
                short smallestID = 0;
                for (int i = 0; i < group.Count; i++)
                {
                    var temp = group[i];
                    if (temp.ObjectID < smallestID || smallestID == 0) smallestID = temp.ObjectID;
                    if (temp.ObjectID > targetValue)
                    {
                        if ((!found) || (temp.ObjectID < bestID))
                        {
                            found = true;
                            bestID = temp.ObjectID;
                        }
                    }
                }
                if (found) return bestID;
                else return smallestID;
            }
        }

        public static VMEntity AdjToLocal(VMStackFrame context, VMEntity pointer, int local)
        {
            VMEntity anchor = context.VM.GetObjectById((short)context.Locals[local]);
            int ptrDir = -1;
            
            if (pointer != null)
            {
                ptrDir = getAdjDir(anchor, pointer);
                if (ptrDir == 3) return null; //reached end
            }

            //iterate through all following dirs til we find an object
            for (int i = ptrDir + 1; i < 4; i++)
            {
                var off = AdjStep[i];
                var adj = context.VM.Context.ObjectQueries.GetObjectsAt(LotTilePos.FromBigTile(
                    (short)(anchor.Position.TileX + off.X),
                    (short)(anchor.Position.TileY + off.Y),
                    anchor.Position.Level));

                if (adj != null && adj.Count > 0)
                {
                    return adj[0];
                }
            }
            return null;
        }

        private static int getAdjDir(VMEntity src, VMEntity dest)
        {
            int diffX = dest.Position.TileX - src.Position.TileX;
            int diffY = dest.Position.TileY - src.Position.TileY;

            return getAdjDir(diffX, diffY);
        }

        private static int getAdjDir(int diffX, int diffY)
        {

            //negative y is anchor
            //positive x is 90 degrees

            return (diffX == 0) ?
                ((diffY < 0) ? 0 : 2) :
                ((diffX < 0) ? 3 : 1);
        }

    }

    public class VMSetToNextOperand : VMPrimitiveOperand
    {
        public uint GUID { get; set; }
        public byte Flags { get; set; }
        public VMVariableScope TargetOwner { get; set; }
        public byte Local { get; set; }
        public byte TargetData { get; set; }
        public VMSetToNextSearchType SearchType {
            get { return (VMSetToNextSearchType)(Flags & 0x7F); } set { Flags = (byte)(0x80 | ((byte)value & 0x7F)); } }

        #region VMPrimitiveOperand Members
        public void Read(byte[] bytes){
            using (var io = IoBuffer.FromBytes(bytes, ByteOrder.LITTLE_ENDIAN)){

                this.GUID = io.ReadUInt32();
                //132 was object of type
                this.Flags = io.ReadByte();
                this.TargetOwner = (VMVariableScope)io.ReadByte();
                this.Local = io.ReadByte();
                this.TargetData = io.ReadByte();

                if ((Flags & 0x80) == 0)
                {
                    //clobber this, we should always set flag for saving.
                    Flags |= 0x80;
                    TargetOwner = VMVariableScope.StackObjectID;
                    TargetData = 0;
                }
            }
        }

        public void Write(byte[] bytes) {
            using (var io = new BinaryWriter(new MemoryStream(bytes)))
            {
                io.Write(GUID);
                io.Write(Flags);
                io.Write((byte)TargetOwner);
                io.Write(Local);
                io.Write(TargetData);
            }
        }
        #endregion
    }

    //Max value is 127
    public enum VMSetToNextSearchType
    {
        Object = 0,
        Person = 1,
        NonPerson = 2,
        PartOfAMultipartTile = 3,
        ObjectOfType = 4,
        NeighborId = 5,
        ObjectWithCategoryEqualToSP0 = 6,
        NeighborOfType = 7,
        ObjectOnSameTile = 8,
        ObjectAdjacentToObjectInLocal = 9,
        Career = 10,
        ClosestHouse = 11,
        FamilyMember = 12, //TS1.5 or higher

        //FSO
        FSOObjectOfSemiGlobal = 100,
    }
}
