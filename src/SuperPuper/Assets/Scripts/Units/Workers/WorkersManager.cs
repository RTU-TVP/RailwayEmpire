#region

using Data.Constant;
using Data.Static.Workers;
using Units.LevelingUp;
using UnityEngine;
using UnityEngine.Events;

#endregion

namespace Units.Workers
{
    public class WorkersManager : MonoBehaviour
    {
        public static WorkersManager Instance;

        [SerializeField] private GameObject _workerParentPrefab;
        [field: SerializeField] public WorkersConfiguration _workersConfiguration { get; private set; }
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Transform _home;
        [SerializeField] private Transform _shop;

        private int _workersCountCurrent;
        private int _workersCountMax;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        private void Start()
        {
            UpdateWorkersCountMax();
            LevelingUpManager.Instance.RegisterOnLevelUp(UpdateWorkersCountMax);
        }

        public void CreateWorker(Transform work, UnityAction workDone, UnityAction saleDone, UnityAction onWorkersCreated)
        {
            if (TryCreatedWorker())
            {
                return;
            }
            onWorkersCreated?.Invoke();

            int moveSpeedLvl = PlayerPrefs.GetInt(WorkersConstantData.WORKERS_LVL_MOVE_SPEED);
            int workTimeLvl = PlayerPrefs.GetInt(WorkersConstantData.WORKERS_LVL_WORK_TIME);
            int saleTimeLvl = PlayerPrefs.GetInt(WorkersConstantData.WORKERS_LVL_SALE_TIME);

            var speedForAnimation = moveSpeedLvl * 0.1f;

            GameObject worker = Instantiate(_workerParentPrefab, _spawnPoint.position, Quaternion.identity);

            _workersCountCurrent++;

            worker.AddComponent<Worker>().SetUp(
                work,
                _home,
                _shop,
                workDone,
                saleDone,
                () =>
                {
                    _workersCountCurrent--;
                    Destroy(worker);
                },
                _workersConfiguration.MoveSpeedDefault + moveSpeedLvl * _workersConfiguration.MoveSpeedDefault * 0.01f,
                _workersConfiguration.WorkTimeDefault - workTimeLvl * _workersConfiguration.WorkTimeDefault * 0.01f,
                _workersConfiguration.SaleTimeDefault - saleTimeLvl * _workersConfiguration.SaleTimeDefault * 0.01f,
                speedForAnimation);
        }

        private bool TryCreatedWorker()
        {
            if (_workersCountCurrent >= _workersConfiguration.MaxWorkers)
            {
                return false;
            }

            return true;
        }

        private void UpdateWorkersCountMax()
        {
            _workersCountMax = _workersConfiguration.MaxWorkers + PlayerPrefs.GetInt(WorkersConstantData.WORKERS_COUNT_MAX);
        }
    }
}
