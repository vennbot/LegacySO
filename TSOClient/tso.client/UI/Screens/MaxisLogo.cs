
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
using System.Timers;
using FSO.Client.UI.Framework;
using FSO.Client.UI.Controls;
using FSO.Client.GameContent;

namespace FSO.Client.UI.Screens
{
    public class MaxisLogo : GameScreen
    {
        private UIImage m_MaxisLogo;
        private UIContainer BackgroundCtnr;
        private Timer m_CheckProgressTimer;

        public MaxisLogo() : base()
        {
            /**
             * Scale the whole screen to 1024
             */
            BackgroundCtnr = new UIContainer();
            BackgroundCtnr.ScaleX = BackgroundCtnr.ScaleY = GlobalSettings.Default.GraphicsWidth / 640.0f;

            /** Background image **/
            m_MaxisLogo = new UIImage(GetTexture((ulong)FileIDs.UIFileIDs.maxislogo));
            BackgroundCtnr.Add(m_MaxisLogo);

            this.Add(BackgroundCtnr);

            m_CheckProgressTimer = new Timer();
            m_CheckProgressTimer.Interval = 5000;
            m_CheckProgressTimer.Elapsed += new ElapsedEventHandler(m_CheckProgressTimer_Elapsed);
            m_CheckProgressTimer.Start();
        }

        private void m_CheckProgressTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            m_CheckProgressTimer.Stop();
            GameFacade.Screens.RemoveCurrent();
            GameFacade.Screens.AddScreen(new LoadingScreen());
        }
    }
}
