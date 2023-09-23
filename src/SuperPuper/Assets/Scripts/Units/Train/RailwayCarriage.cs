#region

using Interactive;
using UnityEngine;

#endregion

namespace Train
{
    static public class RailwayCarriage
    {
        static public void CreateTrain(RailTrack railTrack, Data.Static.Trains.Train train)
        {
            if (train == null) return;

            int count = train.RailwayCarriages.Length;
            Transform oldParent = new GameObject("Train").transform;

            for (int i = 0; i < count; i++)
            {
                oldParent = CreateRailwayCarriage(
                    train.RailwayCarriages[i].Prefab,
                    oldParent,
                    railTrack.StartPoint.position).transform;
            }

            railTrack.SetOccupied(true);
        }

        private static GameObject CreateRailwayCarriage(GameObject prefab, Transform parent, Vector3 startPosition)
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
