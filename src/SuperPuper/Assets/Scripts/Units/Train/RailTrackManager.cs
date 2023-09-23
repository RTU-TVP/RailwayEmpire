#region

using Data.Static.Trains;
using UnityEngine;
using UnityEngine.Events;

#endregion

namespace Units.Train
{
    public class RailTrackManager : MonoBehaviour
    {
        [field: SerializeField] public RailwayCarriagesDatabaseScriptableObject railwayCarriagesDatabaseScriptableObject { get; private set; }
        [field: SerializeField] public TrainConfigurationScriptableObject trainConfigurationScriptableObject { get; private set; }
        [field: SerializeField] public RailTrack[] _railTracks { get; private set; }
        [field: SerializeField] public GameObject _vagonButtonsScreen { get; private set; }

        public readonly Transform[] _rails = new Transform[4];
        public UnityAction<int> _railTrackEmpty { get; private set; }

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
            _rails[index] = RailwayCarriage.CreateTrain(train, this).transform;

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

        public void DoMyself() {}
        public void CallWorkers() {}
    }
}
