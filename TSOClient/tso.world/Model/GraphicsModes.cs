
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

namespace FSO.LotView.Model
{
    [Flags]
    public enum ComponentRenderMode
    {
        _2D = 1,
        _3D = 2,
        Both = 3
    }

    public enum CameraRenderMode
    {
        _2D = 1,
        _2DRotate = 2, //2d camera, but must render with 3d instead
        _3D = 3
    }

    public enum GlobalGraphicsMode
    {
        Full2D, // only use 3d objects when 
        Hybrid2D, // load 2d and 3d objects, use 3d arch
        Full3D // do not load 2d dgrp
    }

    public static class ComponentRenderModeExtensions
    {
        public static bool IsSet(this ComponentRenderMode mode, ComponentRenderMode flag)
        {
            return (mode & flag) == flag;
        }
    }
}
