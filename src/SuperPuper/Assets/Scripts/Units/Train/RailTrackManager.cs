using System.Collections;
using System.Collections.Generic;
using Data.Static.Trains;
using Interactive;
using UnityEngine;
using UnityEngine.Events;

namespace Train
{
	public class RailTrackManager : MonoBehaviour
	{
		[SerializeField] private RailTrack[] _railTracks;
		[SerializeField] private List<Data.Static.Trains.Train> _trains;
		[SerializeField] private TrainConfiguration _trainConfiguration;

		private Transform[] _railsStart = new Transform[4];
		private UnityAction<int> _railTrackEmpty;

		private void Start()
		{
			_railTrackEmpty += CreatedTrain;
			CheckAllRailTracks();
		}

		private void CreatedTrain(int index)
		{
			if (_trains.Count == 0) return;
			var train = _trains[0];
			var count = train.RailwayCarriages.Length;

			var oldParent = new GameObject("Train").transform;
			_railsStart[index] = oldParent;

			oldParent = CreatedRailwayCarriage(
				train.RailwayCarriages[0].Prefab,
				oldParent,
				_railTracks[index].StartPoint.position).transform;

			for (var i = 1; i < count; i++)
			{
				oldParent = CreatedRailwayCarriage(train.RailwayCarriages[i].Prefab, oldParent).transform;
			}

			_trains.RemoveAt(0);
			_railTracks[index].SetOccupied(true);

			StartCoroutine(TrainMovement(index));
		}

		private IEnumerator TrainMovement(int index)
		{
			var train = _railsStart[index];
			var trigger = _railTracks[index].EndPoint;

			var startPosition = train.position;
			var endPosition = trigger.position;

			var journeyLength = Vector3.Distance(startPosition, endPosition);
			var journeyTime = journeyLength / _trainConfiguration.Speed;

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

		private static GameObject CreatedRailwayCarriage(GameObject prefab, Transform parent,
			Vector3 startPosition = default)
		{
			var railwayCarriage = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity, parent);

			railwayCarriage.transform.localPosition = new Vector3(
				startPosition.x - railwayCarriage.GetComponent<MeshRenderer>().bounds.size.x,
				startPosition.y,
				startPosition.z);

			railwayCarriage.AddComponent<InteractiveTrain>();

			return railwayCarriage;
		}

		private void CheckAllRailTracks()
		{
			for (var i = 0; i < _railTracks.Length; i++)
			{
				CheckRailTrack(i);
			}
		}

		private void CheckRailTrack(int index)
		{
			if (_railTracks[index].IsOccupied == false)
			{
				_railTrackEmpty?.Invoke(index);
			}
		}
	}
}