using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCubes : MonoBehaviour {

	public GameObject CubePrefab;

	void Update () {

		if (Input.GetKeyDown (KeyCode.Space)) {
			Instantiate (CubePrefab, transform.position, Quaternion.identity);
		}
	}
}