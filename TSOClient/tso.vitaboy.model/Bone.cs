
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
using Microsoft.Xna.Framework;

namespace FSO.Vitaboy
{
    /// <summary>
    /// Bones are used to animate characters. They hold rotation and translation data.
    /// </summary>
    public class Bone
    {
        public int Unknown;
        public string Name;
        public string ParentName;

        public bool HasProps;
        public List<PropertyListItem> Properties = new List<PropertyListItem>();

        public Vector3 Translation;
        public Quaternion Rotation;

        public int CanTranslate;
        public int CanRotate;
        public int CanBlend;
        public int Index;

        public float WiggleValue;
        public float WigglePower;

        public Bone[] Children;

        //Dummy & debug
        public Vector3 AbsolutePosition;
        public Matrix AbsoluteMatrix;

        /// <summary>
        /// Clones this bone.
        /// </summary>
        /// <returns>A Bone instance with the same values as this one.</returns>
        public Bone Clone()
        {
            var result = new Bone
            {
                Unknown = this.Unknown,
                Name = this.Name,
                ParentName = this.ParentName,
                HasProps = this.HasProps,
                Properties = this.Properties,
                Translation = this.Translation,
                Rotation = this.Rotation,
                CanTranslate = this.CanTranslate,
                CanRotate = this.CanRotate,
                CanBlend = this.CanBlend,
                WiggleValue = this.WiggleValue,
                WigglePower = this.WigglePower,
                Index = this.Index
            };
            return result;
        }
    }

    /// <summary>
    /// An item in a skeleton's Property List.
    /// </summary>
    public class PropertyListItem
    {
        public List<KeyValuePair<string, string>> KeyPairs = new List<KeyValuePair<string, string>>();
    }
}
