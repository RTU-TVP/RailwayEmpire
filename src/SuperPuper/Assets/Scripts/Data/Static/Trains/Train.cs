#region

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#endregion

namespace Data.Static.Trains
{
    [CreateAssetMenu(fileName = "Train", menuName = "Data/Static/Trains/Train", order = 0)]
    public class Train : ScriptableObject
    {
        [field: SerializeField] public RailwayCarriage[] RailwayCarriages { get; private set; }
        public float Lifetime { get; private set; }

        private void OnEnable()
        {
            Lifetime = GetLifetime(RailwayCarriages);
        }

        private static float GetLifetime(IEnumerable<RailwayCarriage> railwayCarriages)
        {
            return railwayCarriages.Sum(railwayCarriage => railwayCarriage.Lifetime);
        }
    }
}
