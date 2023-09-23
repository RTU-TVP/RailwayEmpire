using UnityEngine;
using UnityEngine.SceneManagement;

public class DefeatCondition : MonoBehaviour {

	private Scene _scene;
	private MovementSpawn _movementSpawn;
	

	void Start ()
	{
		_movementSpawn = FindObjectOfType<MovementSpawn>();
		_scene = SceneManager.GetActiveScene ();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("StopTrigger"))
		{
			//gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
			_movementSpawn.AddScore(1);
		}
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.CompareTag("Floor")) {
			SceneManager.LoadScene (_scene.name);
		}
	}
}
