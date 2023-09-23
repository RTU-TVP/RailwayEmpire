#region

using Data.Constant;
using Data.Static.Workers;
using UnityEngine;

#endregion

namespace Units.Workers
{
    public class WorkersManager : MonoBehaviour
    {
        [SerializeField] private WorkersConfiguration _workersConfiguration;
        [SerializeField] private GameObject _workerPrefab;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Transform _home;
        [SerializeField] private Transform _shop;

        private void CreateWorker(Transform work)
        {
            int moveSpeedLvl = PlayerPrefs.GetInt(WorkersConstantData.WORKERS_LVL_MOVE_SPEED);
            int workTimeLvl = PlayerPrefs.GetInt(WorkersConstantData.WORKERS_LVL_WORK_TIME);
            int saleTimeLvl = PlayerPrefs.GetInt(WorkersConstantData.WORKERS_LVL_SALE_TIME);

            GameObject worker = Instantiate(_workerPrefab, _spawnPoint.position, Quaternion.identity);
            worker.GetComponent<Worker>().SetUp(
                work,
                _home,
                _shop,
                () => Debug.Log("Work done!"),
                () => Debug.Log("Sale done!"),
                () => Debug.Log("Home done!"),
                _workersConfiguration.MoveSpeedDefault + moveSpeedLvl * _workersConfiguration.MoveSpeedDefault * 0.01f,
                _workersConfiguration.WorkTimeDefault - workTimeLvl * _workersConfiguration.WorkTimeDefault * 0.01f,
                _workersConfiguration.SaleTimeDefault - saleTimeLvl * _workersConfiguration.SaleTimeDefault * 0.01f);
        }
    }
}
