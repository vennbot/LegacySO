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
using FSO.Common;
using FSO.LotView.Components;
using System.Collections.Generic;

namespace FSO.LotView.Model
{
    public class TimedDynamicStaticLayers
    {
        public HashSet<ObjectComponent>[] Schedule;

        public HashSet<ObjectComponent> StaticObjects = new HashSet<ObjectComponent>();
        public HashSet<ObjectComponent> DynamicObjects = new HashSet<ObjectComponent>();

        private int Timer = 0;
        private int CurrentRing = 0;
        private int HalfSeconds;
        private bool Dirty;

        public TimedDynamicStaticLayers(int halfSeconds)
        {
            HalfSeconds = halfSeconds;
            Schedule = new HashSet<ObjectComponent>[halfSeconds];
            for (int i = 0; i < halfSeconds; i++)
            {
                Schedule[i] = new HashSet<ObjectComponent>();
            }
        }

        public bool Update()
        {
            if (++Timer >= FSOEnvironment.RefreshRate / 2)
            {
                CurrentRing = (CurrentRing + 1) % HalfSeconds;
                var toClear = Schedule[CurrentRing];
                var newEntry = new List<ObjectComponent>();
                foreach (var obj in toClear)
                {
                    //move objects in the dynamic layer back to static
                    if (!obj.ForceDynamic)
                    {
                        MoveToStatic(obj);
                    }
                    else
                    {
                        //stay in dynamic, and reschedule the check.
                        newEntry.Add(obj);
                    }
                }
                toClear.Clear();
                foreach (var item in newEntry) toClear.Add(item);
                Timer = 0;
            }
            var dirty = Dirty;
            Dirty = false;
            return dirty;
        }

        public void RegisterObject(ObjectComponent obj)
        {
            if (obj.RenderInfo.Layer == WorldObjectRenderLayer.STATIC)
                StaticObjects.Add(obj);
            else
                EnsureDynamic(obj);
        }

        public void UnregisterObject(ObjectComponent obj)
        {
            StaticObjects.Remove(obj);
            DynamicObjects.Remove(obj);
            var ring = obj.RenderInfo.DynamicRemoveCycle;
            Schedule[ring].Remove(obj);
        }

        public void MoveToStatic(ObjectComponent obj)
        {
            if (obj.RenderInfo.Layer == WorldObjectRenderLayer.STATIC) return;
            obj.RenderInfo.Layer = WorldObjectRenderLayer.STATIC;
            DynamicObjects.Remove(obj);
            StaticObjects.Add(obj);
            Dirty = true;
        }

        public void EnsureDynamic(ObjectComponent obj)
        {
            if (obj.RenderInfo.Layer == WorldObjectRenderLayer.DYNAMIC)
            {
                var ring = obj.RenderInfo.DynamicRemoveCycle;
                if (ring != CurrentRing)
                {
                    Schedule[ring].Remove(obj);
                    Schedule[CurrentRing].Add(obj);
                    obj.RenderInfo.DynamicRemoveCycle = CurrentRing; //reset timer for moving this object back to static
                }
                return;
            }
            obj.RenderInfo.Layer = WorldObjectRenderLayer.DYNAMIC;
            StaticObjects.Remove(obj);
            DynamicObjects.Add(obj);
            Dirty = true;

            Schedule[CurrentRing].Add(obj);
            obj.RenderInfo.DynamicRemoveCycle = CurrentRing;
        }
    }
}
