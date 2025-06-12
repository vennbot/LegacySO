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

using System.Collections.Generic;
using System.Reflection;

namespace MonoGame.Tools.Pipeline
{
    public class UpdatePropertyAction : IProjectAction
    {
        private readonly IView _view;
        private readonly List<object> _objects;
        private readonly PropertyInfo _property;

        private List<object> _values;

        public UpdatePropertyAction(IView view, List<object> objects, PropertyInfo property, object value)
        {
            _view = view;
            _objects = objects;
            _property = property;

            _values = new List<object>();
            for (int i = 0; i < _objects.Count; i++)
                _values.Add(value);
        }

        public bool Do()
        {
            Toggle();
            return true;
        }

        public bool Undo()
        {
            Toggle();
            return true;
        }

        private void Toggle()
        {
            var oldValues = new List<object>();

            for (int i = 0; i < _objects.Count; i++)
            {
                var obj = _objects[i];

                oldValues.Add(_property.GetValue(obj, null));
                _property.SetValue(obj, _values[i], null);
            }

            _view.UpdateProperties();
            _values = oldValues;
        }
    }
}
