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
using System.IO;
using System.Threading;

namespace MonoGame.Tools.Pipeline
{
    public partial class PipelineController
    {
        private class FileWatcher : IDisposable
        {
            Thread _thread;

            PipelineController _controller;
            IView _view;

            public FileWatcher(PipelineController controller, IView view)
            {
                _controller = controller;
                _view = view;
            }

            public void Run()
            {
                Stop();

                _thread = new Thread(new ThreadStart(ExistsThread));
                _thread.Start();
            }

            public void Stop()
            {
                if (_thread == null)
                    return;
                
                _thread.Abort();
                _thread = null;
            }

            private void ExistsThread()
            {
                while (true)
                {
                    // Can't lock without major code modifications
                    try
                    {
                        var items = _controller._project.ContentItems.ToArray();

                        foreach (var item in items)
                        {
                            Thread.Sleep(100);

                            if (item.Exists == File.Exists(_controller.GetFullPath(item.OriginalPath)))
                                continue;

                            item.Exists = !item.Exists;
                            _view.Invoke(() => _view.UpdateTreeItem(item));
                        }
                    }
                    catch (ThreadAbortException ex)
                    {
                        return;
                    }
                    catch 
                    {
                    }
                }
            }

            public void Dispose()
            {
                Stop();
            }
        }
    }
}
