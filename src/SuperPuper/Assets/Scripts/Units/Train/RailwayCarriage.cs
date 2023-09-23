#region

using Data.Static.Trains;
using Interactive;
using UnityEngine;

#endregion

namespace Train
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

        public static GameObject CreateTrain(Vector3 startPointPosition, Data.Static.Trains.Train train)
        {
            if (train == null) return null;

            int count = train.RailwayCarriages.Length;
            Transform oldParent = new GameObject("Train").transform;
            oldParent.position = startPointPosition;
            oldParent.rotation = Quaternion.Euler(0, 0, 0);

            var tempParent = oldParent;
            for (int i = 0; i < count; i++)
            {
                tempParent = CreateRailwayCarriage(
                    train.RailwayCarriages[i].Prefab,
                    tempParent).transform;
            }

            return oldParent.gameObject;
        }

        private static GameObject CreateRailwayCarriage(GameObject prefab, Transform parent)
        {
            GameObject railwayCarriage;

            railwayCarriage = Object.Instantiate(prefab, parent);
            railwayCarriage.transform.position = parent.position - new Vector3(16.5f, 0, 0);
            railwayCarriage.transform.rotation = Quaternion.Euler(0, 90, 0);

            Debug.Log(railwayCarriage.GetComponentInChildren<MeshRenderer>().bounds.size.x);
            return railwayCarriage;
        }
    }
}
