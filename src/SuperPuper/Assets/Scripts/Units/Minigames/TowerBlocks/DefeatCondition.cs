#region

using UnityEngine;
using UnityEngine.SceneManagement;

#endregion

namespace Minigames.TowerBlocks
{
    public class DefeatCondition : MonoBehaviour
    {
        private MovementSpawn _movementSpawn;

        private Scene _scene;


        private void Start()
        {
            _movementSpawn = FindObjectOfType<MovementSpawn>();
            _scene = SceneManager.GetActiveScene();
        }

        private void OnCollisionEnter(Collision col)
        {
            if (col.gameObject.CompareTag("Floor"))
            {
                SceneManager.LoadScene(_scene.name);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("StopTrigger"))
            {
                //gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                _movementSpawn.AddScore(1);
            }
        }
    }
}
