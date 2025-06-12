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
using FSO.SimAntics.Engine;

namespace FSO.IDE.EditorComponent.OperandForms.DataProviders
{
    public abstract class OpTextProvider : OpDataProvider
    {
        public abstract string GetText(EditorScope scope, VMPrimitiveOperand op);
    }

    public class OpStaticTextProvider : OpTextProvider
    {
        private string Text;
        public OpStaticTextProvider(string text)
        {
            Text = text;
        }

        public override string GetText(EditorScope scope, VMPrimitiveOperand op)
        {
            return Text;
        }
    }
}
