#region

using System.Collections;
using UnityEngine;
using UnityEngine.Events;

#endregion

namespace Units.Train
{
    static public class Train
    {
        public static IEnumerator MoveTrain(Transform train, Vector3 stopPointPosition, float speed, UnityAction onTrainArrived = null)
        {
            Vector3 startPosition = train.position;

            float journeyLength = Vector3.Distance(startPosition, stopPointPosition);
            float journeyTime = journeyLength / speed;

            float startTime = Time.time;
            float distanceCovered = 0.0f;

            while (distanceCovered < journeyLength)
            {
                float distanceFraction = (Time.time - startTime) / journeyTime;
                train.position = Vector3.Lerp(startPosition, stopPointPosition, distanceFraction);
                distanceCovered = distanceFraction * journeyLength;
                yield return null;
            }

            onTrainArrived?.Invoke();
        }

        public static GameObject CreateTrain(Data.Static.Trains.Train train, Vector3 startPoint, RailsTracksManager railsTracksManager)
        {
            int count = train.RailwayCarriages.Length;
            Transform oldParent = new GameObject("Train").transform;
            oldParent.position = startPoint;
            oldParent.rotation = Quaternion.Euler(0, 0, 0);

            for (int i = 0; i < count; i++)
            {
                var go = RailwayCarriage.CreateRailwayCarriage(
                    train.RailwayCarriages[i].Prefab,
                    oldParent,
                    i);

                go.GetComponent<RailwayCarriageManager>().CreatedScreen(railsTracksManager.trainConfigurationScriptableObject.ScreenPosition,
                    railsTracksManager.trainConfigurationScriptableObject.ScreenRotation);
            }

            return oldParent.gameObject;
        }
    }
}
