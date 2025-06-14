
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
using System.IO;
using FSO.Files.Utils;
using FSO.Common.Content;

namespace FSO.Vitaboy
{
    /// <summary>
    /// Hand groups collect together the various hand gesture appearances of the game. 
    /// There are always exactly 18 appearances referenced in a hand group (3 skin colors � 2 hands � 3 gestures).
    /// </summary>
    public class HandGroup
    {
        private uint m_Version;
        public bool TS1HandSet;
        public HandSet LightSkin = new HandSet();
        public HandSet MediumSkin = new HandSet();
        public HandSet DarkSkin = new HandSet();

        /// <summary>
        /// Reads a Handgroup from a supplied Stream.
        /// </summary>
        /// <param name="Str">The Stream to read from.</param>
        public void Read(Stream Str)
        {
            using(IoBuffer IOBuf = new IoBuffer(Str))
            {
                m_Version = IOBuf.ReadUInt32();

                LightSkin.RightHand.Idle.FileID = IOBuf.ReadUInt32();
                LightSkin.RightHand.Idle.TypeID = IOBuf.ReadUInt32();
                LightSkin.RightHand.Fist.FileID = IOBuf.ReadUInt32();
                LightSkin.RightHand.Fist.TypeID = IOBuf.ReadUInt32();
                LightSkin.RightHand.Pointing.FileID = IOBuf.ReadUInt32();
                LightSkin.RightHand.Pointing.TypeID = IOBuf.ReadUInt32();

                LightSkin.LeftHand.Idle.FileID = IOBuf.ReadUInt32();
                LightSkin.LeftHand.Idle.TypeID = IOBuf.ReadUInt32();
                LightSkin.LeftHand.Fist.FileID = IOBuf.ReadUInt32();
                LightSkin.LeftHand.Fist.TypeID = IOBuf.ReadUInt32();
                LightSkin.LeftHand.Pointing.FileID = IOBuf.ReadUInt32();
                LightSkin.LeftHand.Pointing.TypeID = IOBuf.ReadUInt32();

                MediumSkin.RightHand.Idle.FileID = IOBuf.ReadUInt32();
                MediumSkin.RightHand.Idle.TypeID = IOBuf.ReadUInt32();
                MediumSkin.RightHand.Fist.FileID = IOBuf.ReadUInt32();
                MediumSkin.RightHand.Fist.TypeID = IOBuf.ReadUInt32();
                MediumSkin.RightHand.Pointing.FileID = IOBuf.ReadUInt32();
                MediumSkin.RightHand.Pointing.TypeID = IOBuf.ReadUInt32();

                MediumSkin.LeftHand.Idle.FileID = IOBuf.ReadUInt32();
                MediumSkin.LeftHand.Idle.TypeID = IOBuf.ReadUInt32();
                MediumSkin.LeftHand.Fist.FileID = IOBuf.ReadUInt32();
                MediumSkin.LeftHand.Fist.TypeID = IOBuf.ReadUInt32();
                MediumSkin.LeftHand.Pointing.FileID = IOBuf.ReadUInt32();
                MediumSkin.LeftHand.Pointing.TypeID = IOBuf.ReadUInt32();

                DarkSkin.RightHand.Idle.FileID = IOBuf.ReadUInt32();
                DarkSkin.RightHand.Idle.TypeID = IOBuf.ReadUInt32();
                DarkSkin.RightHand.Fist.FileID = IOBuf.ReadUInt32();
                DarkSkin.RightHand.Fist.TypeID = IOBuf.ReadUInt32();
                DarkSkin.RightHand.Pointing.FileID = IOBuf.ReadUInt32();
                DarkSkin.RightHand.Pointing.TypeID = IOBuf.ReadUInt32();

                DarkSkin.LeftHand.Idle.FileID = IOBuf.ReadUInt32();
                DarkSkin.LeftHand.Idle.TypeID = IOBuf.ReadUInt32();
                DarkSkin.LeftHand.Fist.FileID = IOBuf.ReadUInt32();
                DarkSkin.LeftHand.Fist.TypeID = IOBuf.ReadUInt32();
                DarkSkin.LeftHand.Pointing.FileID = IOBuf.ReadUInt32();
                DarkSkin.LeftHand.Pointing.TypeID = IOBuf.ReadUInt32();

            }
        }
    }

    /// <summary>
    /// A hand set is a set of hands (left and right) for an appearance (light, dark or medium).
    /// </summary>
    public class HandSet
    {
        public Hand LeftHand = new Hand();
        public Hand RightHand = new Hand();
    }

    /// <summary>
    /// A hand is a collection of gestures.
    /// </summary>
    public class Hand
    {
        public Gesture Idle = new Gesture();
        public Gesture Fist = new Gesture();
        public Gesture Pointing = new Gesture();
    }

    /// <summary>
    /// A gesture points to a purchasable outfit,
    /// which can be used to access the mesh and
    /// texture for the gesture.
    /// </summary>
    public class Gesture
    {
        public uint FileID;
        public uint TypeID;
        public string Name;
        public string TexName;

        public ContentID ID
        {
            get { return new ContentID(TypeID, FileID); }
        }
    }
}
