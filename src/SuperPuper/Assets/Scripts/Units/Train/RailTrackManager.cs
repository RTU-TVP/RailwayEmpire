#region

using Data.Static.Trains;
using UnityEngine;
using UnityEngine.Events;

#endregion

namespace Units.Train
{
    public class RailTrackManager : MonoBehaviour
    {
        [SerializeField] private RailwayCarriagesDatabaseScriptableObject railwayCarriagesDatabaseScriptableObject;
        [SerializeField] private TrainConfigurationScriptableObject trainConfigurationScriptableObject;
        [SerializeField] private RailTrack[] _railTracks;

        private readonly Transform[] _rails = new Transform[4];
        private UnityAction<int> _railTrackEmpty;

        private void Start()
        {
            _railTrackEmpty += CreatedTrain;
            for (int i = 0; i < _railTracks.Length; i++)
            {
                CheckRailTrack(i);
            }
        }

        private void CreatedTrain(int index)
        {
            var train = RailwayCarriage.GenerationTrain(railwayCarriagesDatabaseScriptableObject);

            _railTracks[index].SetOccupied(true);
            _rails[index] = RailwayCarriage.CreateTrain(_railTracks[index].StartPoint.position, train).transform;

            StartCoroutine(
                TrainMovement.MoveTrain(
                    _rails[index],
                    _railTracks[index].StopPoint.position,
                    trainConfigurationScriptableObject.Speed));
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
