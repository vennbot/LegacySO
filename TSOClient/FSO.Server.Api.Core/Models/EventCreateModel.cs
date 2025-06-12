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

namespace FSO.Server.Api.Core.Models
{
    public class EventCreateModel
    {
        public string title;
        public string description;
        public DateTime start_day;
        public DateTime end_day;
        public string type;
        public int value;
        public int value2;
        public string mail_subject;
        public string mail_message;
        public int mail_sender;
        public string mail_sender_name;
    }

    public class PresetCreateModel
    {
        public string name;
        public string description;
        public int flags;
        public List<PresetItemModel> items;
    }

    public class PresetItemModel
    {
        public string tuning_type;
        public int tuning_table;
        public int tuning_index;
        public float value;
    }
}
