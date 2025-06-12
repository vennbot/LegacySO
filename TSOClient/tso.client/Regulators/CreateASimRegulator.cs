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
using FSO.Server.Clients;
using FSO.Server.Clients.Framework;
using FSO.Server.Protocol.Electron.Packets;
using FSO.Server.Protocol.Voltron.Packets;
using Ninject;

namespace FSO.Client.Regulators
{
    public class CreateASimRegulator : AbstractRegulator, IAriesMessageSubscriber
    {
        private AriesClient City;

        public CreateASimRegulator([Named("City")] AriesClient cityClient)
        {
            this.City = cityClient;
            this.City.AddSubscriber(this);

            AddState("Idle")
                .Default()
                .Transition()
                .OnData(typeof(RSGZWrapperPDU))
                    .TransitionTo("CreateSim");

            AddState("CreateSim").OnlyTransitionFrom("Idle");
            AddState("Waiting")
                .OnlyTransitionFrom("CreateSim")
                .OnData(typeof(CreateASimResponse))
                .TransitionTo("ProcessResponse");

            AddState("ProcessResponse").OnlyTransitionFrom("Waiting");
            AddState("Error").OnlyTransitionFrom("ProcessResponse");
            AddState("Success").OnlyTransitionFrom("ProcessResponse");
        }

        ~CreateASimRegulator(){
            this.City.RemoveSubscriber(this);
        }

        public void CreateSim(RSGZWrapperPDU packet){
            this.AsyncProcessMessage(packet);
        }

        protected override void OnAfterTransition(RegulatorState oldState, RegulatorState newState, object data)
        {
        }

        protected override void OnBeforeTransition(RegulatorState oldState, RegulatorState newState, object data)
        {
            switch (newState.Name)
            {
                case "CreateSim":
                    var packet = (RSGZWrapperPDU)data;
                    City.Write(packet);
                    AsyncTransition("Waiting");
                    break;
                case "ProcessResponse":
                    var response = (CreateASimResponse)data;
                    if(response.Status == CreateASimStatus.SUCCESS){
                        AsyncTransition("Success", data);
                    }else{
                        AsyncTransition("Error", data);
                    }
                    break;
                case "Error":
                    AsyncReset();
                    break;
                case "Success":
                    AsyncReset();
                    break;
            }
        }
        
        public void MessageReceived(AriesClient client, object message)
        {
            if(message is CreateASimResponse){
                AsyncProcessMessage(message);
            }
        }
    }
}
