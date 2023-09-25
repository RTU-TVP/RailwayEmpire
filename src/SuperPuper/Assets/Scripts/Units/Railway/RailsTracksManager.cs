using System;
using System.Collections.Generic;
using Data.Static.Trains;
using DG.Tweening;
using Units.Camera;
using Units.Train;
using Units.Workers;
using UnityEngine;
using UnityEngine.Events;
using static Units.Minigames.MiniGames;

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
            trainManager.RegisterOnTrainCompleted(() =>
            {
                _trains.Remove(trainManager);
                trainManager.GetComponentInChildren<AudioManager>().Play($"leaving1");
                //trainManager.MoveTrain(trainTransform, railTrack.EndPoint.position);
                railTrack.SetIsRailTrackAvailable(true);

                var sequence = DOTween.Sequence();
                sequence.Append(trainGameObject.transform.DOMoveX(1500, 30).SetEase(Ease.InSine)); // тута я пытался, но чёто оно дёргается как-то странно, спасите
                sequence.onComplete += () =>
                {
                    Destroy(trainGameObject);
                    CreateTrain(index);
                };

            });
            _trains.Add(trainManager);
            railTracks[index].SetIsRailTrackAvailable(false);
        }

        public void DoMyself(RailwayCarriageType railwayCarriageRailwayCarriageType, UnityAction onComplete)
        {
            MiniGame miniGame;
            switch (railwayCarriageRailwayCarriageType)
            {
                case RailwayCarriageType.Container:
                    miniGame = MiniGames.Find(x => x.RailwayCarriageType == railwayCarriageRailwayCarriageType);
                    StartContainer(CameraMovement, miniGame, onComplete);
                    break;
                case RailwayCarriageType.Logs:
                    miniGame = MiniGames.Find(x => x.RailwayCarriageType == railwayCarriageRailwayCarriageType);
                    StartLogs(CameraMovement, miniGame, onComplete);
                    break;
                case RailwayCarriageType.Pipes:
                    miniGame = MiniGames.Find(x => x.RailwayCarriageType == railwayCarriageRailwayCarriageType);
                    StartPipes(CameraMovement, miniGame, onComplete);
                    break;
                case RailwayCarriageType.Coal:
                    miniGame = MiniGames.Find(x => x.RailwayCarriageType == railwayCarriageRailwayCarriageType);
                    StartCoal(CameraMovement, miniGame, onComplete);
                    break;
                case RailwayCarriageType.Tank:
                    miniGame = MiniGames.Find(x => x.RailwayCarriageType == railwayCarriageRailwayCarriageType);
                    StartPipes(CameraMovement, miniGame, onComplete);
                    break;
            }
        }

        public void CallWorkers(Transform workerPosition, Action onComplete)
        {
            WorkersManager.Instance.CreateWorker(workerPosition, () => onComplete?.Invoke(), () => {});
        }
    }
}
