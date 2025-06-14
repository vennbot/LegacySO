
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
using FSO.Client.Debug;
using System;
using System.Collections.Generic;
using FSO.SimAntics;
using FSO.Files.Formats.IFF.Chunks;
using FSO.IDE.EditorComponent;
using FSO.Client;
using FSO.Content;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace FSO.IDE
{
    public class IDETester : IDEInjector
    {
        public Dictionary<VMEntity, BHAVEditor> EntToDebugger = new Dictionary<VMEntity, BHAVEditor>();

        public void StartIDE(VM vm)
        {
            if (MainWindow.Instance == null)
            {
                EditorResource.Get().Init(GameFacade.GraphicsDevice);
                var content = Content.Content.Get();
                EditorScope.Behaviour = new Files.Formats.IFF.IffFile(
                    content.TS1 ? Path.Combine(content.TS1BasePath, "GameData/Behavior.iff") : content.GetPath("objectdata/globals/behavior.iff"));
                EditorScope.Globals = content.WorldObjectGlobals.Get("global");
                Program.MainThread = Thread.CurrentThread;

                var t = new Thread(() =>
                {
                    var editor = new MainWindow();
                    editor.SwitchVM(vm);
                    Application.Run(editor);
                });

                //t.SetApartmentState(ApartmentState.STA);
                t.Start();
            }
            else
            {
                var inst = MainWindow.Instance;
                inst.BeginInvoke(new Action(() =>
                {
                    inst.SwitchVM(vm);
                    inst.Show();
                }));
            }
        }

        public void IDEOpenBHAV(BHAV targetBhav, GameObject targetObj)
        {
            new Thread(() =>
            {
                if (MainWindow.Instance == null) return;
                MainWindow.Instance.Invoke(new MainWindowDelegate(() =>
                {
                    MainWindow.Instance.BHAVManager.OpenEditor(targetBhav, targetObj);
                }), null);
            }).Start();
        }

        public void IDEBreakpointHit(VM vm, VMEntity targetEnt)
        {
            new Thread(() =>
            {
                if (MainWindow.Instance == null) return;
                try
                {
                    MainWindow.Instance.Invoke(new MainWindowDelegate(() =>
                    {
                        MainWindow.Instance.BHAVManager.OpenTracer(vm, targetEnt);
                    }), null);
                } catch (Exception)
                {
                    //oops?
                }
            }).Start();
        }

        private delegate void MainWindowDelegate();
    }
}
