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
namespace Mina.Filter.Logging
{
    /// <summary>
    /// Defines a logging level.
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// Not log any information
        /// </summary>
        None,
        /// <summary>
        /// Logs messages on the ERROR level.
        /// </summary>
        Error,
        /// <summary>
        /// Logs messages on the WARN level.
        /// </summary>
        Warn,
        /// <summary>
        /// Logs messages on the INFO level.
        /// </summary>
        Info,
        /// <summary>
        /// Logs messages on the DEBUG level.
        /// </summary>
        Debug,
        /// <summary>
        /// Logs messages on the TRACE level.
        /// </summary>
        Trace
    }
}
