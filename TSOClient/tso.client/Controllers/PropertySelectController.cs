
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
using FSO.Client.UI.Panels;
using FSO.Client.UI.Panels.EODs;
using FSO.Common.DatabaseService;
using FSO.Common.DatabaseService.Model;
using FSO.Common.DataService;
using FSO.Common.DataService.Model;
using FSO.Common.Utils;
using System.Linq;

namespace FSO.Client.Controllers
{
    public class PropertySelectController
    {
        private UIPropertySelectEOD View;
        private IClientDataService DataService;
        private IDatabaseService DatabaseService;

        public PropertySelectController(UIPropertySelectEOD view, IClientDataService dataService, IDatabaseService database)
        {
            this.View = view;
            this.DataService = dataService;
            this.DatabaseService = database;
        }

        public void Search(string query, bool exact)
        {
            if (!DatabaseService.IsConnected)
            {
                View.SetResults(new System.Collections.Generic.List<GizmoLotSearchResult>()
                {
                    new GizmoLotSearchResult()
                    {
                        Result = new SearchResponseItem()
                        {
                            EntityId = 12345678,
                            Name = "The first lot"
                        }
                    },
                    new GizmoLotSearchResult()
                    {
                        Result = new SearchResponseItem()
                        {
                            EntityId = 87654321,
                            Name = "The second lot"
                        }
                    }
                });

                return;
            }

            DatabaseService.Search(new SearchRequest { Query = query, Type = SearchType.LOTS }, exact)
                .ContinueWith(x =>
                {
                    GameThread.InUpdate(() =>
                    {
                        object[] ids = x.Result.Items.Select(y => (object)y.EntityId).ToArray();
                        {
                            var results = x.Result.Items.Select(q =>
                            {
                                return new GizmoLotSearchResult() { Result = q };
                            }).ToList();

                            if (ids.Length > 0)
                            {
                                var lots = DataService.GetMany<Lot>(ids).Result;
                                foreach (var item in lots)
                                {
                                    results.First(f => f.Result.EntityId == item.Id).Lot = item;
                                }
                            }

                            View.SetResults(results);
                        }
                    });
                });
        }
    }
}
