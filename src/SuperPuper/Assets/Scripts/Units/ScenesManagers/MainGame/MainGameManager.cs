using System.Collections;
using Data.Static.Trains;
using Units.Train;
using Units.Workers;
using UnityEngine;
using UnityEngine.Events;

namespace Units.ScenesManagers.MainGame
{
    public class MainGameManager : MonoBehaviour
    {
        public static MainGameManager Instance { get; private set; }
        private WorkersManager _workersManager => WorkersManager.Instance;
        private RailsTracksManager railsTracksManager => RailsTracksManager.Instance;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        public IEnumerator Start()
        {
            for (int i = 0; i < railsTracksManager.CountRailTracks; i++)
            {
                if (railsTracksManager.CheckRailTrack(i))
                {
                    railsTracksManager.CreatedTrain(i);
                    yield return new WaitForSeconds(Random.Range(1, 3));
                }
                else
                {
                    yield return null;
                }
            }

            railsTracksManager.RegisterOnRailTrackEmpty(index => StartCoroutine(CreatedTrain(index)));

            yield return null;
        }

        public void DoMyself(RailwayCarriageType type) { Debug.Log(type); }
        public void CallWorkers(Transform workerPosition, UnityAction workDone)
        {
            CreatedWorker(workerPosition, workDone);
        }

        private IEnumerator CreatedTrain(int index)
        {
            yield return new WaitForSeconds(Random.Range(1, 3));
            railsTracksManager.CreatedTrain(index);
        }

        private void CreatedWorker(Transform workerPosition, UnityAction unityAction)
        {
            _workersManager.CreateWorker(
                workerPosition,
                unityAction,
                () => {});
        }
    }
}
