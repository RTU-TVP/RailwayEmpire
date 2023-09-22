using Interactive;
using UnityEngine;

namespace Train
{
	public static class RailwayCarriage
	{
		private static GameObject CreateRailwayCarriage(GameObject prefab, Transform parent, Vector3 startPosition)
		{
			var railwayCarriage = Object.Instantiate(prefab, startPosition, Quaternion.identity, parent);
			railwayCarriage.transform.localPosition = new Vector3(
				startPosition.x - railwayCarriage.GetComponent<MeshRenderer>().bounds.size.x,
				startPosition.y,
				startPosition.z);

			railwayCarriage.AddComponent<InteractiveTrain>();
			return railwayCarriage;
		}

		public static void CreateTrain(RailTrack railTrack, Data.Static.Trains.Train train)
		{
			if (train == null) return;

			var count = train.RailwayCarriages.Length;
			var oldParent = new GameObject("Train").transform;

			for (var i = 0; i < count; i++)
			{
				oldParent = CreateRailwayCarriage(
					train.RailwayCarriages[i].Prefab,
					oldParent,
					railTrack.StartPoint.position).transform;
			}

			railTrack.SetOccupied(true);
		}
	}
}