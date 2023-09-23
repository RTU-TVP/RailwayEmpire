#region

using System.Collections;
using UnityEngine;

#endregion

namespace Train
{
    static public class TrainMovement
    {
        static public IEnumerator MoveTrain(Transform train, Transform endPoint, float speed)
        {
            Vector3 startPosition = train.position;
            Vector3 endPosition = endPoint.position;

            float journeyLength = Vector3.Distance(startPosition, endPosition);
            float journeyTime = journeyLength / speed;

            float startTime = Time.time;
            float distanceCovered = 0.0f;

            while (distanceCovered < journeyLength)
            {
                float distanceFraction = (Time.time - startTime) / journeyTime;
                train.position = Vector3.Lerp(startPosition, endPosition, distanceFraction);
                distanceCovered = distanceFraction * journeyLength;
                yield return null;
            }
        }
    }
}
