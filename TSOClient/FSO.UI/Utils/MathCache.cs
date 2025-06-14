
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

namespace FSO.Client.Utils
{
    /// <summary>
    /// Utility so calculations are only performed once where appropriate
    /// </summary>
    public class MathCache
    {
        private Dictionary<string, object> m_Value = new Dictionary<string, object>();

        public void Invalidate()
        {
            m_Value.Clear();
        }

        public void Invalidate(string id)
        {
            m_Value.Remove(id);
        }

        public TResult Calculate<TResult>(string id, Func<object, TResult> calculator)
        {
            if (!m_Value.ContainsKey(id))
            {
                m_Value.Add(id, calculator.Invoke(null));
            }
            return (TResult)m_Value[id];
        }
    }
}
