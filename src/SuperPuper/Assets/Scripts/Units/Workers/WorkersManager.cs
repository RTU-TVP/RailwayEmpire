#region

using Data.Constant;
using Data.Static.Workers;
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

        public int WorkersCount;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        public void CreateWorker(Transform work, UnityAction workDone, UnityAction saleDone)
        {
            if (WorkersCount >= _workersConfiguration.MaxWorkers)
            {
                return;
            }
            
            int moveSpeedLvl = PlayerPrefs.GetInt(WorkersConstantData.WORKERS_LVL_MOVE_SPEED);
            int workTimeLvl = PlayerPrefs.GetInt(WorkersConstantData.WORKERS_LVL_WORK_TIME);
            int saleTimeLvl = PlayerPrefs.GetInt(WorkersConstantData.WORKERS_LVL_SALE_TIME);

            var speedForAnimation = moveSpeedLvl * 0.1f;

            GameObject worker = Instantiate(_workerParentPrefab, _spawnPoint.position, Quaternion.identity);
            
            WorkersCount++;

            worker.AddComponent<Worker>().SetUp(
                work,
                _home,
                _shop,
                workDone,
                saleDone,
                () =>
                {
                    WorkersCount--;
                    Destroy(worker);
                },
                _workersConfiguration.MoveSpeedDefault + moveSpeedLvl * _workersConfiguration.MoveSpeedDefault * 0.01f,
                _workersConfiguration.WorkTimeDefault - workTimeLvl * _workersConfiguration.WorkTimeDefault * 0.01f,
                _workersConfiguration.SaleTimeDefault - saleTimeLvl * _workersConfiguration.SaleTimeDefault * 0.01f,
                speedForAnimation);
        }
    }
}
