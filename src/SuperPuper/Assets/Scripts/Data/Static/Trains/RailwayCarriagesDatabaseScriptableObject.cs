#region

using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

#endregion

namespace Data.Static.Trains
{
    [CreateAssetMenu(fileName = "RailwayCarriagesDataBase", menuName = "Data/Static/Trains/RailwayCarriagesDataBase")]
    public class RailwayCarriagesDatabaseScriptableObject : ScriptableObject
    {
        [field: SerializeField] private RailwayCarriageScriptableObject[] _railwayCarriages;
        private Dictionary<RailwayCarriageType, RailwayCarriageScriptableObject> _railwayCarriagesDictionary;

        private void OnEnable()
        {
            _railwayCarriagesDictionary = new Dictionary<RailwayCarriageType, RailwayCarriageScriptableObject>();
            foreach (var railwayCarriage in _railwayCarriages)
            {
                _railwayCarriagesDictionary.Add(railwayCarriage.RailwayCarriageType, railwayCarriage);
            }
        }

        public RailwayCarriageScriptableObject GetRandomRailwayCarriage()
        {
            int randomIndex = Random.Range(0, _railwayCarriages.Length);
            return _railwayCarriages[randomIndex];
        }

        public RailwayCarriageScriptableObject GetRailwayCarriage(RailwayCarriageType railwayCarriageType)
        {
            if (_railwayCarriagesDictionary.ContainsKey(railwayCarriageType))
            {
                return _railwayCarriagesDictionary[railwayCarriageType];
            }
            throw new Exception($"RailwayCarriage with type {railwayCarriageType} not found");
        }
    }
}
