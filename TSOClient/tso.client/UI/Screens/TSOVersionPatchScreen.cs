
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
using FSO.Client.UI.Controls;
using FSO.Client.UI.Framework;
using FSO.Client.UI.Panels;
using FSO.Common;
using FSO.Common.Rendering.Framework.Model;
using FSO.Common.Utils;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using TSOVersionPatcher;

namespace FSO.Client.UI.Screens
{
    public class TSOVersionPatchScreen : UIScreen
    {
        private UISetupBackground Background;
        private string Version;
        private bool CanUpdate;
        private string Patchable = "1.1239.1.0";
        private bool Updating;

        public TSOVersionPatchScreen() : base()
        {
            Background = new UISetupBackground();
            Add(Background);

            Version = Content.Content.Get().VersionString;
            CanUpdate = Version == Patchable;

            GameThread.NextUpdate((state) =>
            {
                UIAlert.Alert(GameFacade.Strings.GetString("f101", "14"),
                    GameFacade.Strings.GetString("f101", "15", new string[] {
                        Version, GameFacade.Strings.GetString("f101", CanUpdate ? "16" : "17")
                    }),
                    true);
            });
        }

        public override void Update(UpdateState state)
        {
            if (!Updating)
            {
                if (Children.Count == 1)
                {
                    if (CanUpdate)
                    {
                        BeginUpdate();
                    }
                    else
                    {
                        GameFacade.Game.Exit();
                    }
                }
                else if (state.CtrlDown && state.ShiftDown && state.NewKeys.Contains(Microsoft.Xna.Framework.Input.Keys.C))
                {
                    FSOFacade.Controller.StartLoading();
                }
            }
            base.Update(state);
        }

        public void BeginUpdate()
        {
            Updating = true;
            var progress = new UILoginProgress()
            {
                Caption = GameFacade.Strings.GetString("f101", "18")
            };
            GlobalShowDialog(progress, true);

            var file = File.Open("Content/Patch/1239toNI.tsop", FileMode.Open, FileAccess.Read, FileShare.Read);
            TSOp patch = new TSOp(file);

            var content = Content.Content.Get();
            var patchPath = Path.GetFullPath(Path.Combine(content.BasePath, "../"));
            Task.Run(() => patch.Apply(patchPath, patchPath, (string message, float pct) =>
            {
                GameThread.InUpdate(() =>
                {
                    if (pct == -1)
                    {
                        UIScreen.GlobalShowAlert(new UIAlertOptions
                        {
                            Title = GameFacade.Strings.GetString("f101", "19"),
                            Message = GameFacade.Strings.GetString("f101", "20", new string[] { message }),
                            Buttons = UIAlertButton.Ok(y =>
                            {
                                RestartGame();
                            })
                        }, true);
                    }
                    else
                    {
                        progress.Progress = pct * 100;
                        progress.ProgressCaption = message;
                    }
                });
            })).ContinueWith((task) =>
            {
                GameThread.InUpdate(() =>
                {
                    UIScreen.RemoveDialog(progress);
                    UIScreen.GlobalShowAlert(new UIAlertOptions
                    {
                        Title = GameFacade.Strings.GetString("f101", "3"),
                        Message = GameFacade.Strings.GetString("f101", "13"),
                        Buttons = UIAlertButton.Ok(y =>
                        {
                            RestartGame();
                        })
                    }, true);
                });
            });
        }

        public void RestartGame()
        {
            try
            {
                if (FSOEnvironment.Linux)
                {
                    System.Diagnostics.Process.Start("mono", "LegacySO.exe " + FSOEnvironment.Args);
                }
                else
                {
                    var args = new ProcessStartInfo(".\\LegacySO.exe", FSOEnvironment.Args);
                    try
                    {

                        System.Diagnostics.Process.Start(args);
                    }
                    catch (Exception)
                    {
                        args.FileName = "LegacySO.exe";
                        System.Diagnostics.Process.Start(args);
                    }
                }
            } catch
            {

            }

            GameFacade.Kill();
        }
    }
}
