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
using FSO.SimAntics.JIT.Translation.Model;
using System;
using System.Collections.Generic;

namespace FSO.SimAntics.JIT.Translation.Primitives
{
    public class AbstractTranslationPrimitive
    {

        private static HashSet<SharedPrimitives> Yielders = new HashSet<SharedPrimitives>()
        {
            SharedPrimitives.AnimateSim,
            SharedPrimitives.GenericSimsCall,
            SharedPrimitives.Sleep,
            SharedPrimitives.TSOInvokePlugin,
            SharedPrimitives.DialogGlobalStrings,
            SharedPrimitives.DialogPrivateStrings,
            SharedPrimitives.DialogSemiglobalStrings,
            SharedPrimitives.IdleForInput,
            SharedPrimitives.TSOInventoryOperations,
            SharedPrimitives.Reach,
            SharedPrimitives.TSOTransferFunds,
            SharedPrimitives.TS1GosubFoundAction,
            SharedPrimitives.RemoveObjectInstance
        };

        private static Dictionary<SharedPrimitives, PrimitiveReturnType> ReturnTypeMap = new Dictionary<SharedPrimitives, PrimitiveReturnType>()
        {
            //defaults to SimanticsTrueFalse
            { SharedPrimitives.Sleep, PrimitiveReturnType.SimanticsTrue },
            { SharedPrimitives.ChangeSuitAccessory, PrimitiveReturnType.SimanticsTrue }, //verify
            { SharedPrimitives.Refresh, PrimitiveReturnType.SimanticsTrue },
            { SharedPrimitives.RandomNumber, PrimitiveReturnType.SimanticsTrue },

            { SharedPrimitives.Sims1Tutorial, PrimitiveReturnType.SimanticsTrue }, //verify

            { SharedPrimitives.GetDistanceTo, PrimitiveReturnType.SimanticsTrue },
            { SharedPrimitives.GetDirectionTo, PrimitiveReturnType.SimanticsTrue },
            { SharedPrimitives.Breakpoint, PrimitiveReturnType.SimanticsTrue },
            //{ SharedPrimitives.IdleForInput, PrimitiveReturnType.SimanticsTrue },

            { SharedPrimitives.RemoveObjectInstance, PrimitiveReturnType.SimanticsTrue }, 
            { SharedPrimitives.MakeNewCharacter, PrimitiveReturnType.SimanticsTrue }, //may actually be able to return false, but currently does not

            { SharedPrimitives.ShowString, PrimitiveReturnType.SimanticsTrue },
            { SharedPrimitives.PlaySoundEvent, PrimitiveReturnType.SimanticsTrue },

            { SharedPrimitives.SetMotiveChange, PrimitiveReturnType.SimanticsTrue },
            { SharedPrimitives.Find5WorstMotives, PrimitiveReturnType.SimanticsTrue }, //verify
            { SharedPrimitives.UIEffect, PrimitiveReturnType.SimanticsTrue }, //verify
            { SharedPrimitives.SpecialEffect, PrimitiveReturnType.SimanticsTrue },

            { SharedPrimitives.SetBalloonHeadline, PrimitiveReturnType.SimanticsTrue },

            { SharedPrimitives.StopAllSounds, PrimitiveReturnType.SimanticsTrue },
            { SharedPrimitives.NotifyStackObjectOutOfIdle, PrimitiveReturnType.SimanticsTrue },
            { SharedPrimitives.AddChangeActionString, PrimitiveReturnType.SimanticsTrue },
            
            { SharedPrimitives.TS1ManageInventory, PrimitiveReturnType.SimanticsTrueFalse }, 

            //SUBROUTINE CALLING FUNCTIONS
            //technically these "yield" by returning, though if they are not on the "yielders" list then control cannot
            //jump back into them, only to their true and false branches.
            { SharedPrimitives.CreateNewObjectInstance, PrimitiveReturnType.SimanticsSubroutine },
            { SharedPrimitives.TS1GosubFoundAction, PrimitiveReturnType.SimanticsSubroutine },
            { SharedPrimitives.GoToRelativePosition, PrimitiveReturnType.SimanticsSubroutine },
            { SharedPrimitives.GoToRoutingSlot, PrimitiveReturnType.SimanticsSubroutine },
            { SharedPrimitives.IdleForInput, PrimitiveReturnType.SimanticsSubroutine }, //CAN be simantics true if allow push is disabled!
            { SharedPrimitives.LookTowards, PrimitiveReturnType.SimanticsSubroutine },
            { SharedPrimitives.RunFunctionalTree, PrimitiveReturnType.SimanticsSubroutine },
            { SharedPrimitives.RunTreeByName, PrimitiveReturnType.SimanticsSubroutine },
        };

        public SharedPrimitives Primitive;
        public BHAVInstruction Instruction;
        public byte Index;

        public virtual bool CanYield
        {
            get
            {
                return Yielders.Contains(Primitive);
            }
        }

        public virtual PrimitiveReturnType ReturnType
        {
            get
            {
                PrimitiveReturnType result;
                if (!ReturnTypeMap.TryGetValue(Primitive, out result))
                    result = PrimitiveReturnType.SimanticsTrueFalse;
                return result;
            }
        }

        public AbstractTranslationPrimitive(BHAVInstruction instruction, byte index)
        {
            if (instruction.Opcode > 255) Primitive = SharedPrimitives.Subroutine;
            else Primitive = (SharedPrimitives)instruction.Opcode;
            Instruction = instruction;
            Index = index;
        }

        public T GetOperand<T>()
        {
            Type type = typeof(T);
            T result = Activator.CreateInstance<T>();
            type.GetMethod("Read").Invoke(result, new object[] { Instruction.Operand } );
            return result;
        }

        protected List<string> Line(string result)
        {
            return new List<string> { result };
        }

        public virtual List<string> CodeGen(TranslationContext context)
        {
            return Line("/* NOP */");
        }
    }
}
