
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
using FSO.SimAntics.Engine;
using System;

namespace FSO.IDE.EditorComponent.OperandForms
{
    public static class OpUtils
    {
        public static void SetOperandProperty(VMPrimitiveOperand op, string propertyN, object value)
        {
            var property = op.GetType().GetProperty(propertyN);

            object finalType;
            try
            {
                finalType = Enum.ToObject(property.PropertyType, value);
            }
            catch (Exception)
            {
                if (value.GetType() != property.PropertyType && property.PropertyType == typeof(UInt32))
                {
                    finalType = unchecked((uint)Convert.ToInt32(value));
                }
                else
                    finalType = Convert.ChangeType(value, property.PropertyType);
            }

            property.SetValue(op, finalType, new object[0]);
        }

        public static object GetOperandProperty(VMPrimitiveOperand op, string propertyN)
        {
            var property = op.GetType().GetProperty(propertyN);
            return(property.GetValue(op, new object[0]));
        }
    }
}
