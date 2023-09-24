using System;
using System.Collections.Generic;
using Data.Static.Trains;
using DG.Tweening;
using Units.Camera;
using Units.Minigames.MemoGame;
using Units.Minigames.PipeGame;
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
                trainManager.MoveTrain(trainTransform, railTracks[index].EndPoint.position);
                railTracks[index].SetIsRailTrackAvailable(true);
                _trains.Remove(trainManager);
               
                var sequence = DOTween.Sequence();
                
                sequence.Append(trainGameObject.transform.DOMoveX(300, 10));
                
                sequence.onComplete += () =>
                {
                    Destroy(trainGameObject);
                    CreateTrain(index);
                };
              
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
                    
                    TryCreatePipes(railwayCarriageRailwayCarriageType, onComplete);

                    break;
                case RailwayCarriageType.Coal:
                    
                    var miniGameCoal = MiniGames.Find(x => x.RailwayCarriageType == railwayCarriageRailwayCarriageType);
                    
                    if (miniGameCoal != null)
                    {
                        var miniGamePrefab = Instantiate(miniGameCoal.MiniGamePrefab, miniGameCoal.MiniGamePosition, Quaternion.identity);
                        
                        CameraMovement.enabled = false;
                        
                        CameraMovement.SecondVirtualCamera.transform.position = miniGameCoal.CameraPosition;
                        
                        CameraMovement.SecondVirtualCamera.gameObject.SetActive(true);
                        
                        var coalSpawner = miniGamePrefab.GetComponentInChildren<CoalSpawner>();
                        
                        coalSpawner._canStart = true;
                        
                        coalSpawner.GoGame();
                        
                        var playerController = miniGamePrefab.GetComponentInChildren<PlayerController>();
                        
                        playerController.OnGameCompleted += () =>
                        {
                            CameraMovement.SecondVirtualCamera.gameObject.SetActive(false);
                            CameraMovement.enabled = true;
                            Destroy(miniGamePrefab);
                            onComplete?.Invoke();
                        };
                        
                        playerController.OnGameFailed += () =>
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
                case RailwayCarriageType.Tank:
                    
                    TryCreatePipes(railwayCarriageRailwayCarriageType, onComplete);
                    
                    break;
            }

            //onComplete?.Invoke();
        }

        private void TryCreatePipes(RailwayCarriageType railwayCarriageRailwayCarriageType, Action onComplete)
        {
            var miniGamePipes = MiniGames.Find(x => x.RailwayCarriageType == railwayCarriageRailwayCarriageType);

            if (miniGamePipes != null)
            {
                var miniGamePrefab =
                    Instantiate(miniGamePipes.MiniGamePrefab, miniGamePipes.MiniGamePosition, Quaternion.identity);

                CameraMovement.enabled = false;

                CameraMovement.SecondVirtualCamera.transform.position = miniGamePipes.CameraPosition;

                CameraMovement.SecondVirtualCamera.transform.DOLocalRotate(new Vector3(90, 0, 90), 0.1f); 

                CameraMovement.SecondVirtualCamera.gameObject.SetActive(true);

                var pipesGameManager = miniGamePrefab.GetComponentInChildren<PipesGameManager>();

                pipesGameManager.OnGameCompleted += () =>
                {
                    CameraMovement.SecondVirtualCamera.gameObject.SetActive(false);
                    CameraMovement.enabled = true;
                    Destroy(miniGamePrefab);
                    onComplete?.Invoke();
                    
                    CameraMovement.SecondVirtualCamera.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.1f); 
                };

                pipesGameManager.OnGameFailed += () =>
                {
                    CameraMovement.SecondVirtualCamera.gameObject.SetActive(false);
                    
                    CameraMovement.SecondVirtualCamera.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.1f); 
                    
                    CameraMovement.enabled = true;
                    Destroy(miniGamePrefab);
                };
            }
            else
            {
                onComplete?.Invoke();
            }
        }

        public void CallWorkers(Transform workerPosition, Action onComplete)
        {
            WorkersManager.Instance.CreateWorker(workerPosition, () => onComplete?.Invoke(), () => {});
        }
    }
}
