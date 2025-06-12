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
using FSO.Common.DataService;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FSO.Client.Network
{
    public class TopicSubscription : ITopicSubscription
    {
        private ClientDataService DataService;
        private List<ITopic> Topics;

        public TopicSubscription(ClientDataService ds)
        {
            DataService = ds;
        }

        public void Dispose()
        {
            DataService.DiscardTopicSubscription(this);
        }

        public List<ITopic> GetTopics()
        {
            return Topics;
        }

        public void Poll()
        {
            if(Topics != null && Topics.Count > 0){
                DataService.RequestTopics(Topics);
            }
        }

        public void Set(List<ITopic> topics)
        {
            var newTopics = new List<ITopic>();

            if(Topics != null){
                foreach(var item in topics)
                {
                    var existing = Topics.FirstOrDefault(x => x.Equals(item));
                    if(existing == null){
                        newTopics.Add(item);
                    }
                }
            }else{
                newTopics = topics;
            }

            Topics = topics;
            if(newTopics.Count > 0){
                DataService.RequestTopics(newTopics);
            }
        }
    }


    public interface ITopicSubscription : IDisposable
    {
        void Set(List<ITopic> topics);
        void Poll();
    }
}
