using System.Collections;
using Data.Static.Trains;
using Units.Money;
using DG.Tweening;
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
        private UnityAction _onTrainArrived;
        private Coroutine _moveCoroutine;

        public void RegisterOnTrainCompleted(UnityAction onTrainCompleted) => _onTrainCompleted += onTrainCompleted;

        public void CreateTrain()
        {
            var train = GenerateRailwaysCarriages(RailsTracksManager.Instance.railwayCarriagesDatabase);
            _railwayCarriageManagers = new RailwayCarriageManager[train.RailwayCarriages.Length];
            _countRailwayCarriagesNotCompleted = train.RailwayCarriages.Length;
            Lifetime = train.Lifetime;

            for (int i = 0; i < train.RailwayCarriages.Length; i++)
            {
                var money = train.RailwayCarriages[i].Money;
                var railwayCarriage = CreateRailwayCarriage(train.RailwayCarriages[i].Prefab, transform, i);
                railwayCarriage.RegisterOnComplete(() => MoneyManager.Instance.ChangeMoneyTo(money));
                railwayCarriage.RegisterOnComplete(CompletedRailwayCarriage);
                _onTrainArrived += railwayCarriage.OnTrainArrived;
                _railwayCarriageManagers[i] = railwayCarriage;
            }

            CheckTrainCompleted();
        }

        public void MoveTrain(Transform train, Vector3 stopPoint, UnityAction onTrainArrived = null)
        {
            if (_moveCoroutine != null) StopCoroutine(_moveCoroutine);
            _onTrainArrived += onTrainArrived;
            Vector3 startPosition = train.position;
            float journeyLength = Vector3.Distance(startPosition, stopPoint);
            float journeyTime = journeyLength / RailsTracksManager.Instance.trainConfiguration.Speed;
            train.transform.DOMove(stopPoint, journeyTime).SetEase(Ease.Linear).OnComplete(() =>
            {
                _onTrainArrived?.Invoke();
                _onTrainArrived = null;
            });
        }

        private Data.Static.Trains.Train GenerateRailwaysCarriages(RailwayCarriagesDatabaseScriptableObject railwayCarriagesDatabaseScriptableObject)
        {
            int count = Random.Range(4, 7);

            var railwayCarriages = new RailwayCarriageScriptableObject[count];
            railwayCarriages[0] = railwayCarriagesDatabaseScriptableObject.GetRailwayCarriage(RailwayCarriageType.Locomotive);
            for (int i = 1; i < count; i++)
            {
                railwayCarriages[i] = railwayCarriagesDatabaseScriptableObject.GetRandomRailwayCarriage();
                if (railwayCarriages[i].RailwayCarriageType == RailwayCarriageType.Locomotive)
                {
                    i--;
                }
            }

            return new Data.Static.Trains.Train(railwayCarriages);
        }

        private RailwayCarriageManager CreateRailwayCarriage(GameObject prefab, Transform parent, int count)
        {
            var railwayCarriage = Instantiate(prefab, parent);
            railwayCarriage.transform.position = parent.position - new Vector3(16.5f * count, 0, 0);
            railwayCarriage.transform.rotation = Quaternion.Euler(0, 90, 0);
            return railwayCarriage.GetComponent<RailwayCarriageManager>();
        }

        private void CompletedRailwayCarriage()
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
