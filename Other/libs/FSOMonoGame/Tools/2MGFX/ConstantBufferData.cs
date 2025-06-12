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
using System.Collections.Generic;

namespace TwoMGFX
{
    internal partial class ConstantBufferData
    {
        public string Name { get; private set; }

        public int Size { get; private set; }

        public List<int> ParameterIndex { get; private set; }

        public List<int> ParameterOffset { get; private set; }

        public List<EffectObject.d3dx_parameter> Parameters { get; private set; }

        public ConstantBufferData(string name)
        {
            Name = name;

            ParameterIndex = new List<int>();
            ParameterOffset = new List<int>();
            Parameters = new List<EffectObject.d3dx_parameter>();
            Size = 0;
        }

        public bool SameAs(ConstantBufferData other)
        {
            // If the names of the constant buffers don't
            // match then consider them different right off 
            // the bat... even if their parameters are the same.
            if (Name != other.Name)
                return false;

            // Do we have the same count of parameters and size?
            if (    Size != other.Size ||
                    Parameters.Count != other.Parameters.Count)
                return false;
            
            // Compare the parameters themselves.
            for (var i = 0; i < Parameters.Count; i++)
            {
                var p1 = Parameters[i];
                var p2 = other.Parameters[i];

                // Check the importaint bits.
                if (    p1.name != p2.name ||
                        p1.rows != p2.rows ||
                        p1.columns != p2.columns ||
                        p1.class_ != p2.class_ ||
                        p1.type != p2.type ||
                        p1.bufferOffset != p2.bufferOffset)
                    return false;
            }

            // These are equal.
            return true;
        }

    }
}
