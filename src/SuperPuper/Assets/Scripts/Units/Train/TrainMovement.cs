#region

using System.Collections;
using UnityEngine;

#endregion

namespace Train
{
    static public class TrainMovement
    {
        static public IEnumerator MoveTrain(Transform train, Vector3 stopPointPosition, float speed)
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
        }
    }
}
