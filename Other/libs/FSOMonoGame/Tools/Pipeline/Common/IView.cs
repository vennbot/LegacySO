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
using System.Diagnostics;

namespace MonoGame.Tools.Pipeline
{
    public enum AskResult
    {
        Yes,
        No,
        Cancel
    }

    public enum CopyAction
    {
        Copy,
        Link,
        Skip
    }

    public interface IView
    {
        void Attach(IController controller);

        void Invoke(Action action);

        AskResult AskSaveOrCancel();

        bool AskSaveName(ref string filePath, string title);

        bool AskOpenProject(out string projectFilePath);

        bool AskImportProject(out string projectFilePath);

        void ShowError(string title, string message);

        void ShowMessage(string message);

        bool ShowDeleteDialog(List<IProjectItem> items);

        bool ShowEditDialog(string title, string text, string oldname, bool file, out string newname);

        void BeginTreeUpdate();

        void SetTreeRoot(IProjectItem item);

        void AddTreeItem(IProjectItem item);

        void RemoveTreeItem(IProjectItem item);

        void UpdateTreeItem(IProjectItem item);

        void EndTreeUpdate();

        void UpdateProperties();

        void OutputAppend(string text);

        void OutputClear();

        bool ChooseContentFile(string initialDirectory, out List<string> files);  

        bool ChooseContentFolder(string initialDirectory, out string folder);

        bool ChooseItemTemplate(string folder, out ContentItemTemplate template, out string name);

        bool CopyOrLinkFile(string file, bool exists, out CopyAction action, out bool applyforall);

        bool CopyOrLinkFolder(string folder, bool exists, out CopyAction action, out bool applyforall);

        Process CreateProcess(string exe, string commands);

        void UpdateCommands(MenuInfo info);

        void UpdateRecentList(List<string> recentList);

        void SetClipboard(string text);
    }
}
