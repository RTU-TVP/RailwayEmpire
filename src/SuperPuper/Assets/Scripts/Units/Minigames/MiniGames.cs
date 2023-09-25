using System;
using DG.Tweening;
using Units.Camera;
using Units.Minigames.MemoGame;
using Units.Minigames.PipeGame;
using Units.Railway;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.Object;

namespace Units.Minigames
{
    public static class MiniGames
    {
        public static void StartContainer(CameraMovement cameraMovement, MiniGame miniGame, UnityAction onComplete)
        {
            if (miniGame != null)
            {
                var miniGamePrefab = Instantiate(miniGame.MiniGamePrefab, miniGame.MiniGamePosition, Quaternion.identity);

                cameraMovement.enabled = false;

                cameraMovement.SecondVirtualCamera.transform.position = miniGame.CameraPosition;

                cameraMovement.SecondVirtualCamera.gameObject.SetActive(true);

                var miniGameController = miniGamePrefab.GetComponentInChildren<SceneController>();

                miniGameController.OnGameCompleted += () =>
                {
                    cameraMovement.SecondVirtualCamera.gameObject.SetActive(false);
                    cameraMovement.enabled = true;
                    Destroy(miniGamePrefab);
                    onComplete?.Invoke();
                };

                miniGameController.OnGameLost += () =>
                {
                    cameraMovement.SecondVirtualCamera.gameObject.SetActive(false);
                    cameraMovement.enabled = true;
                    Destroy(miniGamePrefab);
                };
            }
            else
            {
                onComplete?.Invoke();
            }
        }

        public static void StartLogs(CameraMovement cameraMovement, MiniGame miniGameLogs, UnityAction onComplete)
        {
            if (miniGameLogs != null)
            {
                var miniGamePrefab = Instantiate(miniGameLogs.MiniGamePrefab, miniGameLogs.MiniGamePosition, Quaternion.identity);

                cameraMovement.enabled = false;

                cameraMovement.SecondVirtualCamera.transform.position = miniGameLogs.CameraPosition;

                cameraMovement.SecondVirtualCamera.gameObject.SetActive(true);

                var movementSpawn = miniGamePrefab.GetComponentInChildren<MovementSpawn>();

                movementSpawn.cameraPrincipal = cameraMovement.SecondVirtualCamera;

                movementSpawn._hasStarted = true;

                movementSpawn.OnGameCompleted += () =>
                {
                    cameraMovement.SecondVirtualCamera.gameObject.SetActive(false);
                    cameraMovement.enabled = true;
                    Destroy(miniGamePrefab);
                    onComplete?.Invoke();
                };

                movementSpawn.OnGameLost += () =>
                {
                    cameraMovement.SecondVirtualCamera.gameObject.SetActive(false);
                    cameraMovement.enabled = true;
                    Destroy(miniGamePrefab);
                };
            }
            else
            {
                onComplete?.Invoke();
            }
        }

        public static void StartPipes(CameraMovement cameraMovement, MiniGame miniGamePipes, UnityAction onComplete)
        {
            if (miniGamePipes != null)
            {
                var miniGamePrefab =
                    Instantiate(miniGamePipes.MiniGamePrefab, miniGamePipes.MiniGamePosition, Quaternion.identity);

                cameraMovement.enabled = false;

                cameraMovement.SecondVirtualCamera.transform.position = miniGamePipes.CameraPosition;

                cameraMovement.SecondVirtualCamera.transform.DOLocalRotate(new Vector3(90, 0, 90), 0.1f);

                cameraMovement.SecondVirtualCamera.gameObject.SetActive(true);

                var pipesGameManager = miniGamePrefab.GetComponentInChildren<PipesGameManager>();

                pipesGameManager.OnGameCompleted += () =>
                {
                    cameraMovement.SecondVirtualCamera.gameObject.SetActive(false);
                    cameraMovement.enabled = true;
                    Destroy(miniGamePrefab);
                    onComplete?.Invoke();

                    cameraMovement.SecondVirtualCamera.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.1f);
                };

                pipesGameManager.OnGameFailed += () =>
                {
                    cameraMovement.SecondVirtualCamera.gameObject.SetActive(false);

                    cameraMovement.SecondVirtualCamera.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.1f);

                    cameraMovement.enabled = true;
                    Destroy(miniGamePrefab);
                };
            }
            else
            {
                onComplete?.Invoke();
            }
        }

        public static void StartCoal(CameraMovement cameraMovement, MiniGame miniGameCoal, UnityAction onComplete)
        {
            if (miniGameCoal != null)
            {
                var miniGamePrefab = Instantiate(miniGameCoal.MiniGamePrefab, miniGameCoal.MiniGamePosition, Quaternion.identity);

                cameraMovement.enabled = false;

                cameraMovement.SecondVirtualCamera.transform.position = miniGameCoal.CameraPosition;

                cameraMovement.SecondVirtualCamera.gameObject.SetActive(true);

                var coalSpawner = miniGamePrefab.GetComponentInChildren<CoalSpawner>();

                coalSpawner._canStart = true;

                coalSpawner.GoGame();

                var playerController = miniGamePrefab.GetComponentInChildren<PlayerController>();

                playerController.OnGameCompleted += () =>
                {
                    cameraMovement.SecondVirtualCamera.gameObject.SetActive(false);
                    cameraMovement.enabled = true;
                    Destroy(miniGamePrefab);
                    onComplete?.Invoke();
                };

                playerController.OnGameFailed += () =>
                {
                    cameraMovement.SecondVirtualCamera.gameObject.SetActive(false);
                    cameraMovement.enabled = true;
                    Destroy(miniGamePrefab);
                };

            }
            else
            {
                onComplete?.Invoke();
            }
        }
    }
}
