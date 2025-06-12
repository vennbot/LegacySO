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
using System;

namespace Mina.Core.Filterchain
{
    /// <summary>
    /// An exception thrown when <see cref="IoFilter.Init()"/> or
    /// <see cref="IoFilter.OnPostAdd(IoFilterChain, String, INextFilter)"/> failed.
    /// </summary>
    [Serializable]
    public class IoFilterLifeCycleException : Exception
    {
        /// <summary>
        /// </summary>
        public IoFilterLifeCycleException()
        { }

        /// <summary>
        /// </summary>
        public IoFilterLifeCycleException(String message)
            : base(message)
        { }

        /// <summary>
        /// </summary>
        public IoFilterLifeCycleException(String message, Exception innerException)
            : base(message, innerException)
        { }

        /// <summary>
        /// </summary>
        protected IoFilterLifeCycleException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }

        /// <summary>
        /// </summary>
        public override void GetObjectData(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
