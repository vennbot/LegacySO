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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FSO.Client.UI.Framework;
using FSO.Client.Utils;
using FSO.Common.Rendering.Framework;

namespace FSO.Client.Debug
{
    public partial class TSOSceneInspector : Form
    {
        private Dictionary<TreeNode, object> ItemMap;

        public TSOSceneInspector()
        {
            ItemMap = new Dictionary<TreeNode, object>();
            InitializeComponent();
            RefreshUITree();
        }

        private void refreshBtn_Click(object sender, EventArgs e)
        {
            RefreshUITree();
        }

        private void RefreshUITree()
        {
            ItemMap.Clear();

            uiTree.Nodes.Clear();
            foreach (var scene in GameFacade.Scenes.Scenes)
            {
                var node = new TreeNode(scene.ToString());
                var cameraNode = node.Nodes.Add("Camera");
                ItemMap.Add(cameraNode, scene.Camera);

                ItemMap.Add(node, scene);
                node.Nodes.AddRange(ExploreScene(scene).ToArray());
                uiTree.Nodes.Add(node);
            }

            //foreach (var scene in GameFacade.Scenes.ExternalScenes)
            //{
            //    var node = new TreeNode(scene.ToString());

            //    ItemMap.Add(node, scene);
            //    node.Nodes.AddRange(ExploreScene(scene).ToArray());
            //    uiTree.Nodes.Add(node);
            //}

            
            

            //var rootNode = new TreeNode("Scenes");
            //ItemMap[rootNode] = GameFacade.Screens.CurrentUIScreen;
            //rootNode.Nodes.AddRange(nodes.ToArray());

            //uiTree.Nodes.Add(rootNode);
        }

        private List<TreeNode> ExploreScene(_3DAbstract container)
        {
            var result = new List<TreeNode>();

            foreach (var child in container.GetElements())
            {
                var node = new TreeNode(child.ToString());
                ItemMap.Add(node, child);
                result.Add(node);
            }

            return result;
        }

        private void uiTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (ItemMap.ContainsKey(e.Node))
            {
                var value = ItemMap[e.Node];
                propertyGrid1.SelectedObject = value;
            }
        }

    }
}
