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
using FSO.Client.UI.Panels;
using FSO.Common.DataService;
using FSO.Common.DataService.Model;
using FSO.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FSO.Client.Controllers.Panels
{
    public class BookmarksController : IDisposable
    {
        private Network.Network Network;
        private IClientDataService DataService;
        private UIBookmarks View;
        private Binding<Avatar> Binding;
        private BookmarkType CurrentType = BookmarkType.AVATAR;

        public BookmarksController(UIBookmarks view, IClientDataService dataService, Network.Network network)
        {
            this.Network = network;
            this.DataService = dataService;
            this.View = view;
            this.Binding = new Binding<Avatar>().WithMultiBinding(x => { RefreshResults(); }, "Avatar_BookmarksVec");

            Init();
        }

        private void Init()
        {
            DataService.Get<Avatar>(Network.MyCharacter).ContinueWith(x =>
            {
                Binding.Value = x.Result;
            });
        }

        public void ChangeType(BookmarkType type)
        {
            CurrentType = type;
            RefreshResults();
        }

        public void RefreshResults()
        {
            var list = new List<BookmarkListItem>();
            if(Binding.Value != null && Binding.Value.Avatar_BookmarksVec != null)
            {
                var bookmarks = Binding.Value.Avatar_BookmarksVec.Where(x => x.Bookmark_Type == (byte)CurrentType).ToList();
                var enriched = DataService.EnrichList<BookmarkListItem, Bookmark, Avatar>(bookmarks, x => x.Bookmark_TargetID, (bookmark, avatar) =>
                {
                    return new BookmarkListItem {
                        Avatar = avatar,
                        Bookmark = bookmark
                    };
                });

                list = enriched;
            }

            View.SetResults(list);
        }

        /**
            var list = new List<BookmarkListItem>();

            if(Binding.Value != null && Binding.Value.Avatar_BookmarksVec != null)
            {
                var bookmarks = Binding.Value.Avatar_BookmarksVec;
                var ids = bookmarks.Select(x => x.Bookmark_TargetID);
                var avatars = 
            }**/




        public void Toggle()
        {
            if (View.Visible)
            {
                Close();
            }
            else
            {
                Show();
            }
        }

        public void Close()
        {
            View.Visible = false;
        }

        public void Show()
        {
            RefreshResults();
            View.Parent.Add(View);
            View.Visible = true;
        }

        public void Dispose()
        {
        }
    }
}
