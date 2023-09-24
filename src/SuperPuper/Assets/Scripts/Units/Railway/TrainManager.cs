using System;
using System.Collections;
using Units.Money;
using UnityEngine;
using UnityEngine.Events;

namespace Units.Railway
{
    public class TrainManager : MonoBehaviour
    {
        public float Lifetime { get; private set; }
        private RailwayCarriageManager[] _railwayCarriageManagers;
        private int _countRailwayCarriagesNotCompleted;
        private UnityAction _onTrainCompleted;
        private Coroutine _moveCoroutine;
        private int _index;
        private int _money;

        public void RegisterOnTrainCompleted(UnityAction onTrainCompleted) => _onTrainCompleted += onTrainCompleted;

        public void CreateTrain(int index)
        {
            _index = index;
            var train = RailwayCarriageManager.GenerateTrain(RailsTracksManager.Instance.railwayCarriagesDatabase);
            _railwayCarriageManagers = new RailwayCarriageManager[train.RailwayCarriages.Length];
            _countRailwayCarriagesNotCompleted = train.RailwayCarriages.Length;
            Lifetime = train.Lifetime;
            
            for (int i = 0; i < train.RailwayCarriages.Length; i++)
            {
                _money += train.RailwayCarriages[i].Money;
                var railwayCarriage = RailwayCarriageManager.CreateRailwayCarriage(train.RailwayCarriages[i].Prefab, transform, i);
                if (train.RailwayCarriages[i].IsInteractive)
                {
                    int i1 = i;
                    _railwayCarriageManagers[i] = railwayCarriage.GetComponent<RailwayCarriageManager>();
                    _railwayCarriageManagers[i].RegisterOnComplete(() => CompletedRailwayCarriage(_railwayCarriageManagers[i1]));
                }
                else
                {
                    _countRailwayCarriagesNotCompleted--;
                }
            }
            CheckTrainCompleted();
            _onTrainCompleted += () =>
            {
                MoneyManager.Instance.ChangeMoneyTo(_money);
                Debug.Log($"{_money}");
            };
        }

        public void MoveTrain(Transform train, Vector3 stopPoint, UnityAction onTrainArrived = null)
        {
            if (_moveCoroutine != null) StopCoroutine(_moveCoroutine);
            _moveCoroutine = StartCoroutine(Move());
            return;

            IEnumerator Move()
            {
                Vector3 startPosition = train.position;

                float journeyLength = Vector3.Distance(startPosition, stopPoint);
                float journeyTime = journeyLength / RailsTracksManager.Instance.trainConfiguration.Speed;

                float startTime = Time.time;
                float distanceCovered = 0.0f;

                while (distanceCovered < journeyLength)
                {
                    float distanceFraction = (Time.time - startTime) / journeyTime;
                    train.position = Vector3.Lerp(startPosition, stopPoint, distanceFraction);
                    distanceCovered = distanceFraction * journeyLength;
                    yield return null;
                }

                onTrainArrived?.Invoke();
            }
        }

        private void CompletedRailwayCarriage(RailwayCarriageManager railwayCarriageManager)
        {
            _countRailwayCarriagesNotCompleted--;
            CheckTrainCompleted();
        }

        private void CheckTrainCompleted()
        {
            if (_countRailwayCarriagesNotCompleted != 0) return;
            _onTrainCompleted?.Invoke();
        }
    }
}
