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
using System.Collections.Generic;
using System.IO;
using Eto.Forms;

namespace MonoGame.Tools.Pipeline
{
    partial class DeleteDialog : Dialog<bool>
    {
        private IController _controller;
        private TreeGridItem _treeBase;

        public DeleteDialog(IController controller, List<IProjectItem> items)
        {
            InitializeComponent();

            _controller = controller;
            _treeBase = new TreeGridItem();

            treeView1.Columns.Add(new GridColumn { DataCell = new ImageTextCell(0, 1), AutoSize = true, Resizable = true, Editable = false });

            foreach (var item in items)
            {
                if (item is DirectoryItem)
                    ProcessDirectory(_controller.GetFullPath(item.OriginalPath));
                else
                    Add(_treeBase, item.OriginalPath, false, _controller.GetFullPath(item.OriginalPath));
            }

            treeView1.DataStore = _treeBase;
        }

        private void ProcessDirectory(string directory)
        {
            Add(_treeBase, _controller.GetRelativePath(directory), true, "");

            var dirs = Directory.GetDirectories(directory);
            var files = Directory.GetFiles(directory);

            foreach (var dir in dirs)
                ProcessDirectory(dir);

            foreach (var file in files)
                Add(_treeBase, _controller.GetRelativePath(file), false, file);
        }

        public TreeGridItem GetItem(TreeGridItem root, string text, bool folder, string fullpath)
        {
            var enumerator = root.Children.GetEnumerator();

            while (enumerator.MoveNext())
            {
                var item = enumerator.Current as TreeGridItem;
                var itemtext = item.GetValue(1).ToString();

                if (itemtext == text)
                    return item;
            }

            var ret = new TreeGridItem();
            var icon = folder ? Global.GetEtoDirectoryIcon(true) : Global.GetEtoFileIcon(fullpath, true);
            ret.Values = new object[] { icon, text };
            root.Children.Add(ret);
            root.Expanded = true;

            return ret;
        }

        public void Add(TreeGridItem root, string path, bool folder, string fullpath)
        {
            var split = path.Split(Path.DirectorySeparatorChar);
            var file = split.Length == 1 && !folder;
            var item = GetItem(root, split[0], !file, fullpath);

            if (path.Contains(Path.DirectorySeparatorChar.ToString()))
                Add(item, string.Join(Path.DirectorySeparatorChar.ToString(), split, 1, split.Length - 1), folder, fullpath);
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            Result = true;
            Close();
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
