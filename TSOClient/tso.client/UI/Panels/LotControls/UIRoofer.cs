
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
using System.Collections.Generic;
using FSO.Common.Rendering.Framework.Model;
using FSO.SimAntics;
using FSO.SimAntics.NetPlay.Model.Commands;

namespace FSO.Client.UI.Panels.LotControls
{
    public class UIRoofer : UICustomLotControl
    {
        public UIRoofer(VM vm, LotView.World world, UILotControl parent, List<int> parameters)
        {
            vm.SendCommand(new VMNetSetRoofCmd()
            {
                Pitch = vm.Context.Architecture.RoofPitch,
                Style = (uint)parameters[0]
            });
        }
        public void MouseDown(UpdateState state)
        {
            return;
        }

        public void MouseUp(UpdateState state)
        {
            return;
        }

        public void Release()
        {
            return;
        }

        public void Update(UpdateState state, bool scrolled)
        {
            return;
        }
    }
}
