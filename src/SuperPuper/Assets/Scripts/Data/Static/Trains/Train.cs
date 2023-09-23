#region

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#endregion

namespace Data.Static.Trains
{
    public class Train
    {
        public Train(RailwayCarriageScriptableObject[] railwayCarriages)
        {
            RailwayCarriages = railwayCarriages;
            Lifetime = GetLifetime(railwayCarriages);
        }

        public RailwayCarriageScriptableObject[] RailwayCarriages { get; private set; }
        public float Lifetime { get; private set; }
        
        private static float GetLifetime(IEnumerable<RailwayCarriageScriptableObject> railwayCarriages)
        {
            return railwayCarriages.Sum(railwayCarriage => railwayCarriage.Lifetime);
        }
    }
}
