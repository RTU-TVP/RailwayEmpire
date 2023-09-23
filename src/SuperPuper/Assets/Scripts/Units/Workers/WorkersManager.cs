using UnityEngine;

namespace Workers
{
    public class WorkersManager : MonoBehaviour
    {
        [SerializeField] private GameObject _workerPrefab;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Transform _home;

        private void CreateWorker(Transform target)
        {
            var worker = Instantiate(_workerPrefab, _spawnPoint.position, Quaternion.identity);
            worker.GetComponent<Worker>().SetUp(target, _home, () => Debug.Log("Work done!"));
        }
    }
}
