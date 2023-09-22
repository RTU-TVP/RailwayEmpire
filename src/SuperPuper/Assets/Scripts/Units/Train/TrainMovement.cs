using System.Collections;
using UnityEngine;

namespace Train
{
	public static class TrainMovement
	{
		public static IEnumerator MoveTrain(Transform train, Transform endPoint, float speed)
		{
			var startPosition = train.position;
			var endPosition = endPoint.position;

			var journeyLength = Vector3.Distance(startPosition, endPosition);
			var journeyTime = journeyLength / speed;

			var startTime = Time.time;
			var distanceCovered = 0.0f;

			while (distanceCovered < journeyLength)
			{
				var distanceFraction = (Time.time - startTime) / journeyTime;
				train.position = Vector3.Lerp(startPosition, endPosition, distanceFraction);
				distanceCovered = distanceFraction * journeyLength;
				yield return null;
			}
		}
	}
}