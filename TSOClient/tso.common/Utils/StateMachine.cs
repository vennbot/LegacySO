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
using System;
using System.Collections.Generic;

namespace FSO.Common.Utils
{
    public class StateMachine <STATES> where STATES : IConvertible
    {
        public STATES CurrentState { get; internal set; }
        private Dictionary<STATES, List<STATES>> LegalMoves;

        public event Callback<STATES, STATES> OnTransition;

        public StateMachine(STATES startState)
        {
            this.CurrentState = startState;
        }


        public bool TransitionTo(STATES state)
        {
            
            lock (CurrentState)
            {
                if (CurrentState.Equals(state))
                {
                    return true;
                }

                /*if (!LegalMoves.ContainsKey(CurrentState))
                {
                    return false;
                }
                if (!LegalMoves[CurrentState].Contains(state))
                {
                    return false;
                }*/

                var previousState = CurrentState;
                this.CurrentState = state;
                if (OnTransition != null)
                {
                    OnTransition(previousState, CurrentState);
                }
                return true;
            }
        }

        /*public StateMachine<STATES> AllowTransition(STATES from, STATES to)
        {
            if (!LegalMoves.ContainsKey(from))
            {
                LegalMoves.Add(from, new List<STATES>());
            }
            LegalMoves[from].Add(to);
            return this;
        }*/
    }
}
