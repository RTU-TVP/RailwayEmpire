#region

using Data.Static.Trains;
using UnityEngine;
using UnityEngine.Events;

#endregion

namespace Units.Train
{
    public class RailsTracksManager : MonoBehaviour
    {
        public static RailsTracksManager Instance;

        [field: SerializeField] public RailwayCarriagesDatabaseScriptableObject railwayCarriagesDatabaseScriptableObject { get; private set; }
        [field: SerializeField] public TrainConfigurationScriptableObject trainConfigurationScriptableObject { get; private set; }
        [field: SerializeField] public RailTrack[] _railTracks { get; private set; }
        [field: SerializeField] public GameObject _vagonButtonsScreen { get; private set; }

        public int CountRailTracks => _railTracks.Length;
        private readonly Transform[] _rails = new Transform[4];
        private UnityAction<int> _railTrackEmpty;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        public void CreatedTrain(int index)
        {
            var train = RailwayCarriage.GenerationTrain(railwayCarriagesDatabaseScriptableObject);

            _railTracks[index].SetOccupied(true);
            _rails[index] = Train.CreateTrain(train, _railTracks[index].StartPoint.position, this).transform;

            StartCoroutine(
                Train.MoveTrain(
                    _rails[index],
                    _railTracks[index].StopPoint.position,
                    trainConfigurationScriptableObject.Speed));
        }

        public void RegisterOnRailTrackEmpty(UnityAction<int> railTrackEmpty) => _railTrackEmpty = railTrackEmpty;
        public void UnregisterOnRailTrackEmpty(UnityAction<int> railTrackEmpty) => _railTrackEmpty -= railTrackEmpty;
        public bool CheckRailTrack(int index) => !_railTracks[index].IsOccupied;
    }
}
