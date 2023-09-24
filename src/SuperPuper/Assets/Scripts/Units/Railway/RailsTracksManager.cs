using System;
using System.Collections.Generic;
using Data.Static.Trains;
using Units.Camera;
using Units.Minigames.MemoGame;
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
            railTracks[index].SetIsRailTrackAvailable(false);
            var trainGameObject = new GameObject($"Train {index}");
            trainGameObject.transform.position = railTracks[index].StartPoint.position;
            var trainManager = trainGameObject.AddComponent<TrainManager>();
            var trainTransform = trainGameObject.transform;

            trainManager.CreateTrain(index);
            trainManager.RegisterOnTrainCompleted(() =>
            {
                Debug.Log($"Train {index} completed and destroyed");

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
            switch (railwayCarriageRailwayCarriageType)
            {
                case RailwayCarriageType.Container:
                    
                    var miniGame = MiniGames.Find(x => x.RailwayCarriageType == railwayCarriageRailwayCarriageType);
                    
                    if (miniGame != null)
                    {
                        var miniGamePrefab = Instantiate(miniGame.MiniGamePrefab, miniGame.MiniGamePosition, Quaternion.identity);
                        
                        CameraMovement.enabled = false;
                        
                        CameraMovement.SecondVirtualCamera.transform.position = miniGame.CameraPosition;
                        
                        CameraMovement.SecondVirtualCamera.gameObject.SetActive(true);
                        
                        var miniGameController = miniGamePrefab.GetComponentInChildren<SceneController>();
                        
                        miniGameController.OnGameCompleted += () =>
                        {
                            CameraMovement.SecondVirtualCamera.gameObject.SetActive(false);
                            CameraMovement.enabled = true;
                            Destroy(miniGamePrefab);
                            onComplete?.Invoke();
                        };
                        
                        miniGameController.OnGameLost += () =>
                        {
                            CameraMovement.SecondVirtualCamera.gameObject.SetActive(false);
                            CameraMovement.enabled = true;
                            Destroy(miniGamePrefab);
                        };
                    }
                    else
                    {
                        onComplete?.Invoke();
                    }
                    
                    break;
                case RailwayCarriageType.Logs:
                    
                    var miniGameLogs = MiniGames.Find(x => x.RailwayCarriageType == railwayCarriageRailwayCarriageType);
                    
                    if (miniGameLogs != null)
                    {
                        var miniGamePrefab = Instantiate(miniGameLogs.MiniGamePrefab, miniGameLogs.MiniGamePosition, Quaternion.identity);
                        
                        CameraMovement.enabled = false;
                        
                        CameraMovement.SecondVirtualCamera.transform.position = miniGameLogs.CameraPosition;
                        
                        CameraMovement.SecondVirtualCamera.gameObject.SetActive(true);
                        
                        var movementSpawn = miniGamePrefab.GetComponentInChildren<MovementSpawn>();

                        movementSpawn.cameraPrincipal = CameraMovement.SecondVirtualCamera;

                        movementSpawn._hasStarted = true;
                        
                        movementSpawn.OnGameCompleted += () =>
                        {
                            CameraMovement.SecondVirtualCamera.gameObject.SetActive(false);
                            CameraMovement.enabled = true;
                            Destroy(miniGamePrefab);
                            onComplete?.Invoke();
                        };
                        
                        movementSpawn.OnGameLost += () =>
                        {
                            CameraMovement.SecondVirtualCamera.gameObject.SetActive(false);
                            CameraMovement.enabled = true;
                            Destroy(miniGamePrefab);
                        };
                    }
                    else
                    {
                        onComplete?.Invoke();
                    }
                    
                    break;
                case RailwayCarriageType.Pipes:
                    
                    var miniGamePipes = MiniGames.Find(x => x.RailwayCarriageType == railwayCarriageRailwayCarriageType);
                    
                    if (miniGamePipes != null)
                    {
                        var miniGamePrefab = Instantiate(miniGamePipes.MiniGamePrefab, miniGamePipes.MiniGamePosition, Quaternion.identity);
                        
                        CameraMovement.enabled = false;
                        
                        CameraMovement.SecondVirtualCamera.transform.position = miniGamePipes.CameraPosition;
                        
                        CameraMovement.SecondVirtualCamera.gameObject.SetActive(true);
                    }
                    else
                    {
                        onComplete?.Invoke();
                    }
                    
                    break;
                case RailwayCarriageType.Coal:
                    break;
                case RailwayCarriageType.Tank:
                    break;
            }

            //onComplete?.Invoke();
        }

        public void CallWorkers(Transform workerPosition, Action onComplete)
        {
            WorkersManager.Instance.CreateWorker(workerPosition, () => onComplete?.Invoke(), () => {});
        }
    }
}
