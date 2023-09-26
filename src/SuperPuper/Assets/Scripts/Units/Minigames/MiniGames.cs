using DG.Tweening;
using Units.Camera;
using Units.Minigames.MemoGame;
using Units.Minigames.PipeGame;
using Units.Railway;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.Object;

namespace Units.Minigames
{
    public static class MiniGames
    {
        public static void StartContainer(CameraMovement cameraMovement, MiniGame miniGame, UnityAction onComplete, UnityAction onCompletedNotSuccessful)
        {
            if (miniGame != null)
            {
                ConfigureCamera(cameraMovement, miniGame);
                var miniGamePrefab = Instantiate(miniGame.MiniGamePrefab, miniGame.MiniGamePosition, Quaternion.identity);
                var miniGameController = miniGamePrefab.GetComponentInChildren<SceneController>();
                miniGameController.OnGameCompleted += () => EndGame(cameraMovement, miniGamePrefab, onComplete);
                miniGameController.OnGameLost += () => EndGame(cameraMovement, miniGamePrefab, onCompletedNotSuccessful);
            }
            else onComplete?.Invoke();
        }

        public static void StartLogs(CameraMovement cameraMovement, MiniGame miniGame, UnityAction onComplete, UnityAction onCompletedNotSuccessful)
        {
            if (miniGame != null)
            {
                ConfigureCamera(cameraMovement, miniGame);
                var miniGamePrefab = Instantiate(miniGame.MiniGamePrefab, miniGame.MiniGamePosition, Quaternion.identity);
                var movementSpawn = miniGamePrefab.GetComponentInChildren<MovementSpawn>();
                movementSpawn.cameraPrincipal = cameraMovement.SecondVirtualCamera;
                movementSpawn._hasStarted = true;
                movementSpawn.OnGameCompleted += () => { EndGame(cameraMovement, miniGamePrefab, onComplete); };
                movementSpawn.OnGameLost += () => { EndGame(cameraMovement, miniGamePrefab, onCompletedNotSuccessful); };
            }
            else onComplete?.Invoke();
        }

        public static void StartPipes(CameraMovement cameraMovement, MiniGame miniGame, UnityAction onComplete, UnityAction onCompletedNotSuccessful)
        {
            if (miniGame != null)
            {
                ConfigureCamera(cameraMovement, miniGame);
                var miniGamePrefab = Instantiate(miniGame.MiniGamePrefab, miniGame.MiniGamePosition, Quaternion.identity);
                cameraMovement.SecondVirtualCamera.transform.DOLocalRotate(new Vector3(90, 0, 90), 0.1f);
                var pipesGameManager = miniGamePrefab.GetComponentInChildren<PipesGameManager>();
                pipesGameManager.OnGameCompleted += () =>
                {
                    EndGame(cameraMovement, miniGamePrefab, onComplete);
                    cameraMovement.SecondVirtualCamera.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.1f);
                };
                pipesGameManager.OnGameFailed += () =>
                {
                    EndGame(cameraMovement, miniGamePrefab, onCompletedNotSuccessful);
                    cameraMovement.SecondVirtualCamera.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.1f);
                };
            }
            else onComplete?.Invoke();
        }

        public static void StartCoal(CameraMovement cameraMovement, MiniGame miniGame, UnityAction onComplete, UnityAction onCompletedNotSuccessful)
        {
            if (miniGame != null)
            {
                ConfigureCamera(cameraMovement, miniGame);
                var miniGamePrefab = Instantiate(miniGame.MiniGamePrefab, miniGame.MiniGamePosition, Quaternion.identity);
                var coalSpawner = miniGamePrefab.GetComponentInChildren<CoalSpawner>();
                coalSpawner.CanStart = true;
                coalSpawner.GoGame();
                var playerController = miniGamePrefab.GetComponentInChildren<PlayerController>();
                playerController.OnGameCompleted += () => { EndGame(cameraMovement, miniGamePrefab, onComplete); };
                playerController.OnGameFailed += () => { EndGame(cameraMovement, miniGamePrefab, onCompletedNotSuccessful); };
            }
            else onComplete?.Invoke();
        }

        private static void ConfigureCamera(CameraMovement cameraMovement, MiniGame miniGame)
        {
            cameraMovement.enabled = false;
            cameraMovement.SecondVirtualCamera.transform.position = miniGame.CameraPosition;
            cameraMovement.SecondVirtualCamera.gameObject.SetActive(true);
        }

        private static void EndGame(CameraMovement cameraMovement, Object miniGamePrefab, UnityAction onComplete = null)
        {
            cameraMovement.SecondVirtualCamera.gameObject.SetActive(false);
            cameraMovement.enabled = true;
            Destroy(miniGamePrefab);
            onComplete?.Invoke();
        }
    }
}
