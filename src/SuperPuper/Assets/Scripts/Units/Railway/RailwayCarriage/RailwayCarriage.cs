#region

using Data.Static.Trains;
using UI;
using UnityEngine;

#endregion

namespace Units.Train
{
    static public class RailwayCarriage
    {
        public static Data.Static.Trains.Train GenerationTrain(RailwayCarriagesDatabaseScriptableObject railwayCarriagesDatabaseScriptableObject)
        {
            int count = Random.Range(3, 5);

            var railwayCarriages = new RailwayCarriageScriptableObject[count];
            railwayCarriages[0] = railwayCarriagesDatabaseScriptableObject.GetRailwayCarriage(RailwayCarriageType.Locomotive);
            for (int i = 1; i < count; i++)
            {
                railwayCarriages[i] = railwayCarriagesDatabaseScriptableObject.GetRandomRailwayCarriage();
                if (railwayCarriages[i].RailwayCarriageType == RailwayCarriageType.Locomotive)
                {
                    i--;
                }
            }

            return new Data.Static.Trains.Train(railwayCarriages);
        }

        public static GameObject CreateRailwayCarriage(GameObject prefab, Transform parent, int count)
        {
            var railwayCarriage = Object.Instantiate(prefab, parent);
            railwayCarriage.transform.position = parent.position - new Vector3(16.5f * count, 0, 0);
            railwayCarriage.transform.rotation = Quaternion.Euler(0, 90, 0);
            return railwayCarriage;
        }
    }
}
