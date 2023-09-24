using System;
using UnityEngine;

public class DefeatCondition : MonoBehaviour {

	private MovementSpawn _movementSpawn;

	public event Action OnCubeFall;
	

	void Start ()
	{
		_movementSpawn = FindObjectOfType<MovementSpawn>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("StopTrigger"))
		{
			//gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
			_movementSpawn.AddScore(1);
			Destroy(other.gameObject);
		}
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.CompareTag("Floor")) 
		{
			OnCubeFall?.Invoke();
		}
	}
}
