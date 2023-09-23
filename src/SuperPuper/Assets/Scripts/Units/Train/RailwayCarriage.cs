#region

using Interactive;
using UnityEngine;

#endregion

namespace Train
{
    static public class RailwayCarriage
    {
        static public GameObject CreateTrain(Vector3 startPointPosition, Data.Static.Trains.Train train)
        {
            if (train == null) return null;

            int count = train.RailwayCarriages.Length;
            Transform oldParent = new GameObject("Train").transform;
            oldParent.position = startPointPosition;

            var tempParent = oldParent;
            for (int i = 0; i < count; i++)
            {
                tempParent = CreateRailwayCarriage(
                    train.RailwayCarriages[i].Prefab,
                    tempParent).transform;
            }

            return oldParent.gameObject;
        }

        private static GameObject CreateRailwayCarriage(GameObject prefab, Transform parent, Vector3 startPosition = default)
        {
            GameObject railwayCarriage = Object.Instantiate(prefab, startPosition, Quaternion.identity, parent);
            railwayCarriage.transform.localPosition = new Vector3(
                startPosition.x - railwayCarriage.GetComponent<MeshRenderer>().bounds.size.x,
                startPosition.y,
                startPosition.z);

            railwayCarriage.AddComponent<InteractiveObject>();
            return railwayCarriage;
        }
    }
}
