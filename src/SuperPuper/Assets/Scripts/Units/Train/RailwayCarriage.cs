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

        public static GameObject CreateTrain(Data.Static.Trains.Train train, RailTrackManager railTrackManager)
        {
            int count = train.RailwayCarriages.Length;
            Transform oldParent = new GameObject("Train").transform;
            oldParent.position = railTrackManager._railTracks[0].StopPoint.position;
            oldParent.rotation = Quaternion.Euler(0, 0, 0);

            for (int i = 0; i < count; i++)
            {
                var go = CreateRailwayCarriage(
                    train.RailwayCarriages[i].Prefab,
                    oldParent,
                    i);

                go.GetComponent<RailwayCarriageManager>().CreatedScreen(
                    railTrackManager._vagonButtonsScreen,
                    railTrackManager.trainConfigurationScriptableObject.ScreenPosition,
                    railTrackManager.trainConfigurationScriptableObject.ScreenRotation,
                    railTrackManager);
            }

            return oldParent.gameObject;
        }


        private static GameObject CreateRailwayCarriage(GameObject prefab, Transform parent, int count)
        {
            var railwayCarriage = Object.Instantiate(prefab, parent);
            railwayCarriage.transform.position = parent.position - new Vector3(16.5f * count, 0, 0);
            railwayCarriage.transform.rotation = Quaternion.Euler(0, 90, 0);
            return railwayCarriage;
        }
    }
}
