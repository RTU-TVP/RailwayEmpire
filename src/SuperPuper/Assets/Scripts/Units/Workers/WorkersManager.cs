using Data.Constant;
using Data.Static.Workers;
using UnityEngine;

namespace Workers
{
    public class WorkersManager : MonoBehaviour
    {
        [SerializeField] private WorkersConfiguration _workersConfiguration;
        [SerializeField] private GameObject _workerPrefab;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Transform _home;

        private void CreateWorker(Transform target)
        {
            var moveSpeedLvl = PlayerPrefs.GetInt(WorkersConstantData.WORKERS_LVL_MOVE_SPEED);
            var workTimeLvl = PlayerPrefs.GetInt(WorkersConstantData.WORKERS_LVL_WORK_TIME);
            var saleTimeLvl = PlayerPrefs.GetInt(WorkersConstantData.WORKERS_LVL_SALE_TIME);

            var worker = Instantiate(_workerPrefab, _spawnPoint.position, Quaternion.identity);
            worker.GetComponent<Worker>().SetUp(
                target,
                _home,
                () => Debug.Log("Work done!"),
                _workersConfiguration.MoveSpeedDefault + moveSpeedLvl * _workersConfiguration.MoveSpeedDefault * 0.01f,
                _workersConfiguration.WorkTimeDefault - workTimeLvl * _workersConfiguration.WorkTimeDefault * 0.01f,
                _workersConfiguration.SaleTimeDefault - saleTimeLvl * _workersConfiguration.SaleTimeDefault * 0.01f);
        }
    }
}
