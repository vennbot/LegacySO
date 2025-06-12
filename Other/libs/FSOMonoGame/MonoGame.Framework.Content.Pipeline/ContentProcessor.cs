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
// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;

namespace Microsoft.Xna.Framework.Content.Pipeline
{
    /// <summary>
    /// Provides a base class to use when developing custom processor components. All processors must derive from this class.
    /// </summary>
    public abstract class ContentProcessor<TInput, TOutput> : IContentProcessor
    {
        /// <summary>
        /// Initializes a new instance of the ContentProcessor class.
        /// </summary>
        protected ContentProcessor()
        {

        }

        /// <summary>
        /// Processes the specified input data and returns the result.
        /// </summary>
        /// <param name="input">Existing content object being processed.</param>
        /// <param name="context">Contains any required custom process parameters.</param>
        /// <returns>A typed object representing the processed input.</returns>
        public abstract TOutput Process(TInput input, ContentProcessorContext context);

        /// <summary>
        /// Gets the expected object type of the input parameter to IContentProcessor.Process.
        /// </summary>
        Type IContentProcessor.InputType
        {
            get { return typeof(TInput); }
        }

        /// <summary>
        /// Gets the object type returned by IContentProcessor.Process.
        /// </summary>
        Type IContentProcessor.OutputType
        {
            get { return typeof(TOutput); }
        }

        /// <summary>
        /// Processes the specified input data and returns the result.
        /// </summary>
        /// <param name="input">Existing content object being processed.</param>
        /// <param name="context">Contains any required custom process parameters.</param>
        /// <returns>The processed input.</returns>
        object IContentProcessor.Process(object input, ContentProcessorContext context)
        {
            if (input == null)
                throw new ArgumentNullException("input");
            if (context == null)
                throw new ArgumentNullException("context");
            if (!(input is TInput))
                throw new InvalidOperationException("input is not of the expected type");
            return Process((TInput)input, context);
        }
    }
}
