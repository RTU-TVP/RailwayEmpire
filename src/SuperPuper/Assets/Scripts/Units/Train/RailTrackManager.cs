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

			RailwayCarriage.CreateTrain(_railTracks[index], train);
			_trains.RemoveAt(0);

			StartCoroutine(
				TrainMovement.MoveTrain(
					_railsStart[index],
					_railTracks[index].EndPoint,
					_trainConfiguration.Speed));
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
			if (!_railTracks[index].IsOccupied)
			{
				_railTrackEmpty?.Invoke(index);
			}
		}
	}
}