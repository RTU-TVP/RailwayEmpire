using System;
using System.Collections;
using System.Collections.Generic;
using Data.Static.Trains;
using DG.Tweening;
using Units.Camera;
using Units.Train;
using Units.Workers;
using UnityEngine;
using UnityEngine.Events;
using static Units.Minigames.MiniGames;
using Random = UnityEngine.Random;

namespace Units.Railway
{
    public class RailsTracksManager : MonoBehaviour
    {
        [field: SerializeField] public RailwayCarriagesDatabaseScriptableObject railwayCarriagesDatabase { get; private set; }
        [field: SerializeField] public TrainConfigurationScriptableObject trainConfiguration { get; private set; }
        [field: SerializeField] public RailTrack[] railTracks { get; private set; }
        [field: SerializeField] public List<MiniGame> MiniGames { get; private set; }
        [field: SerializeField] public GameObject vagonButtonsScreen { get; private set; }
        [field: SerializeField] public CameraMovement CameraMovement { get; private set; }

        public static RailsTracksManager Instance { get; private set; }
        private List<TrainManager> _trains = new List<TrainManager>();

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        private IEnumerator Start()
        {
            for (int i = 0; i < railTracks.Length; i++)
            {
                if (railTracks[i].IsRailTrackAvailable)
                {
                    CreateTrain(i);
                    yield return new WaitForSeconds(Random.Range(
                        trainConfiguration.WaitingTimeForNextTrainMin,
                        trainConfiguration.WaitingTimeForNextTrainMax));
                }
            }
        }

        private void CreateTrain(int index)
        {
            var railTrack = railTracks[index];
            var trainGameObject = new GameObject($"Train {index}")
            {
                transform =
                {
                    position = railTrack.StartPoint.position
                }
            };
            var trainManager = trainGameObject.AddComponent<TrainManager>();
            var trainTransform = trainGameObject.transform;
            trainManager.CreateTrain();
            trainManager.MoveTrain(trainTransform, railTrack.StopPoint.position);
            trainManager.RegisterOnTrainCompleted(TrainLeaving);
            trainManager.RegisterOnTimerZero(TrainLeaving);
            _trains.Add(trainManager);
            railTracks[index].SetIsRailTrackAvailable(false);

            void TrainLeaving()
            {
                _trains.Remove(trainManager);
                trainManager.GetComponentInChildren<AudioManager>().Play($"leaving1");
                railTrack.SetIsRailTrackAvailable(true);

                var sequence = DOTween.Sequence();
                sequence.Append(trainGameObject.transform.DOMoveX(1000, 30).SetEase(Ease.InSine));
                sequence.onComplete += () =>
                {
                    Destroy(trainGameObject);
                    CreateTrain(index);
                };
            }
        }

        public void DoMyself(
            RailwayCarriageType railwayCarriageRailwayCarriageType,
            UnityAction onCompletedSuccessfully,
            UnityAction onCompletedNotSuccessful)
        {
            MiniGame miniGame;
            switch (railwayCarriageRailwayCarriageType)
            {
                case RailwayCarriageType.Container:
                    miniGame = MiniGames.Find(x => x.RailwayCarriageType == railwayCarriageRailwayCarriageType);
                    StartContainer(CameraMovement, miniGame, onCompletedSuccessfully, onCompletedNotSuccessful);
                    break;
                case RailwayCarriageType.Logs:
                    miniGame = MiniGames.Find(x => x.RailwayCarriageType == railwayCarriageRailwayCarriageType);
                    StartLogs(CameraMovement, miniGame, onCompletedSuccessfully, onCompletedNotSuccessful);
                    break;
                case RailwayCarriageType.Pipes:
                    miniGame = MiniGames.Find(x => x.RailwayCarriageType == railwayCarriageRailwayCarriageType);
                    StartPipes(CameraMovement, miniGame, onCompletedSuccessfully, onCompletedNotSuccessful);
                    break;
                case RailwayCarriageType.Coal:
                    miniGame = MiniGames.Find(x => x.RailwayCarriageType == railwayCarriageRailwayCarriageType);
                    StartCoal(CameraMovement, miniGame, onCompletedSuccessfully, onCompletedNotSuccessful);
                    break;
                case RailwayCarriageType.Tank:
                    miniGame = MiniGames.Find(x => x.RailwayCarriageType == railwayCarriageRailwayCarriageType);
                    StartPipes(CameraMovement, miniGame, onCompletedSuccessfully, onCompletedNotSuccessful);
                    break;
            }
        }

        public void CallWorkers(Transform workerPosition, UnityAction onComplete, UnityAction onWorkersCreated)
        {
            WorkersManager.Instance.CreateWorker(workerPosition, () => onComplete?.Invoke(), () => {}, onWorkersCreated);
        }
    }
}
