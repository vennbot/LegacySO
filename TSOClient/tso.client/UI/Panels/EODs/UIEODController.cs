
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
using FSO.Client.UI.Framework;
using FSO.SimAntics.NetPlay.EODs.Handlers;
using FSO.SimAntics.NetPlay.Model.Commands;
using System;
using System.Collections.Generic;

namespace FSO.Client.UI.Panels.EODs
{
    public class UIEODController : UIContainer
    {
        public static Dictionary<uint, Type> IDToHandler = new Dictionary<uint, Type>()
        {
            { 0x2a6356a0, typeof(UISignsEOD) },
            { 0x4a5be8ab, typeof(UIDanceFloorEOD) },
            { 0xea47ae39, typeof(UIPizzaMakerEOD) },
            { 0xca418206, typeof(UIPaperChaseEOD) },
            { 0x2b58020b, typeof(UIRackOwnerEOD) },
            { 0xcb492685, typeof(UIRackEOD) },
            { 0x8b300068, typeof(UIDresserEOD) },
            { 0x0949E698, typeof(UIScoreboardEOD) },
            { 0x0A69F29F, typeof(UIPermissionDoorEOD) },
            { 0xCB2819CB, typeof(UISlotsEOD) },
            { 0xAA5E36DC, typeof(UITrunkEOD) },
            { 0x2D642D39, typeof(UIWarGameEOD) },
            { 0xAA65FE9E, typeof(UITimerPluginEOD) },
            { 0x895C1CEB, typeof(UIGameCompDrawACardPluginEOD) },
            { 0x8ADFC7A2, typeof(UIBandEOD) },
            { 0x0B2A6B83, typeof(UIRouletteEOD) },
            { 0x897f82f5, typeof(UISecureTradeEOD) },
            { 0x2B2FC514, typeof(UIBlackjackEOD) },
            { 0x4A245A22, typeof(UITwoPersonJobObjectMazeEOD) },

            { 0xEC55D705, typeof(UINCDanceFloorEOD) },
            { 0x6C5C7555, typeof(UIDJStationEOD) },

            //new for LegacySO
            { 0x00001000, typeof(UINewspaperEOD) },
            { 0x00001001, typeof(UIHoldEmCasinoEOD) },
            { 0x00001003, typeof(UIBulletinEOD) },
            { 0x00001004, typeof(UICooldownEventEOD) },
            { 0x00001005, typeof(UIGameshowBuzzerPlayerEOD) },
            { 0x00001006, typeof(UIGameshowBuzzerHostEOD) },

            //for LegacySO-archive
            { 0x00002000, typeof(UIPropertySelectEOD) },
        };

        //this class is a container so that it can hold EODs without them being active in Live Mode.
        public UILotControl Lot;
        public EODLiveModeOpt DisplayMode;
        public string EODMessage = "";
        public string EODTime = "";

        public UIEOD ActiveEOD;
        public uint ActivePID;

        public UIEODController(UILotControl lot)
        {
            Lot = lot;
        }

        public void OnEODMessage(VMNetEODMessageCmd cmd)
        {
            switch (cmd.EventName)
            {
                case "eod_enter":
                    //attempt to create the EOD UI for the given plugin (live mode will detect this)
                    if (cmd.Binary) return; //???
                    Type handlerType = null;
                    if (IDToHandler.TryGetValue(cmd.PluginID, out handlerType))
                    {
                        ActiveEOD = (UIEOD)Activator.CreateInstance(handlerType, this);
                        ActivePID = cmd.PluginID;
                        Add(ActiveEOD);
                    }
                    break;
                case "eod_leave":
                    if (cmd.Binary || ActiveEOD == null) return; //???
                    DisplayMode = null;
                    Remove(ActiveEOD);
                    ActivePID = 0;
                    ActiveEOD = null;
                    break;
                default:
                    //forward to existing ui
                    if (ActiveEOD == null) return; //uh... what UI?
                    if (cmd.Binary)
                    {
                        EODDirectBinaryEventHandler handle = null;
                        if (ActiveEOD.BinaryHandlers.TryGetValue(cmd.EventName, out handle))
                        {
                            handle(cmd.EventName, cmd.BinData);
                        }
                    } else
                    {
                        EODDirectPlaintextEventHandler handle = null;
                        if (ActiveEOD.PlaintextHandlers.TryGetValue(cmd.EventName, out handle))
                        {
                            handle(cmd.EventName, cmd.TextData);
                        }
                    }
                    break;
            }
        }

        public void ShowEODMode(EODLiveModeOpt mode)
        {
            DisplayMode = mode; //gets picked up by live mode
        }

        public void CloseEOD()
        {
            if (ActiveEOD == null) return; //???
            ShowEODMode(null);
            Remove(ActiveEOD);
            ActiveEOD = null;
            ActivePID = 0;
        }
    }
}
