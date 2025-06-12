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
using FSO.Client.UI.Framework;
using FSO.Common.Rendering.Framework.Model;
using System.Linq;

namespace FSO.Client.UI.Panels
{
    public class UISortedContainer : UIContainer
    {
        public override void Update(UpdateState state)
        {
            Sort();
            base.Update(state);
        }

        public void Sort()
        {
            Children = Children.OrderBy(x => (x as IZIndexable)?.Z ?? 0).ToList();
        }
    }

    public interface IZIndexable
    {
        float Z { get; set; }
    }
}
