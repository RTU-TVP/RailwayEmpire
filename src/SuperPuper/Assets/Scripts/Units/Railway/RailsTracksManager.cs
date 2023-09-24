using System;
using System.Collections.Generic;
using Data.Static.Trains;
using Units.Train;
using Units.Workers;
using Unity.VisualScripting;
using UnityEngine;

namespace Units.Railway
{
    public class RailsTracksManager : MonoBehaviour
    {
        [field: SerializeField] public RailwayCarriagesDatabaseScriptableObject railwayCarriagesDatabase { get; private set; }
        [field: SerializeField] public TrainConfigurationScriptableObject trainConfiguration { get; private set; }
        [field: SerializeField] public RailTrack[] railTracks { get; private set; }
        [field: SerializeField] public GameObject vagonButtonsScreen { get; private set; }

        public static RailsTracksManager Instance { get; private set; }
        private List<TrainManager> _trains = new List<TrainManager>();

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        private void Start()
        {
            for (int i = 0; i < railTracks.Length; i++)
            {
                if (railTracks[i].IsRailTrackAvailable)
                {
                    CreateTrain(i);
                }
            }
        }

        private void CreateTrain(int index)
        {
            railTracks[index].SetIsRailTrackAvailable(false);
            var trainGameObject = new GameObject($"Train {index}");
            trainGameObject.transform.position = railTracks[index].StartPoint.position;
            var trainManager = trainGameObject.AddComponent<TrainManager>();
            var trainTransform = trainGameObject.transform;

            trainManager.CreateTrain(index);
            trainManager.RegisterOnTrainCompleted(() =>
            {
                trainManager.MoveTrain(trainTransform, railTracks[index].EndPoint.position);
                railTracks[index].SetIsRailTrackAvailable(true);
                _trains.Remove(trainManager);
                Destroy(trainGameObject);
                
                CreateTrain(index);
            });
            _trains.Add(trainManager);

            trainManager.MoveTrain(trainTransform, railTracks[index].StopPoint.position);
        }

        public void DoMyself(RailwayCarriageType railwayCarriageRailwayCarriageType, Action onComplete)
        {
            onComplete?.Invoke();
        }

        public void CallWorkers(Transform workerPosition, Action onComplete)
        {
            WorkersManager.Instance.CreateWorker(workerPosition, () => onComplete?.Invoke(), () => {});
        }
    }
}
