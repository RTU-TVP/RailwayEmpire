using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCubos : MonoBehaviour {

	public GameObject CuboPrefab;

	void Start () {
		
	}

	void Update () {

		if (Input.GetKeyDown (KeyCode.Space)) {
			Instantiate (CuboPrefab, transform.position, Quaternion.identity);
		}
	}
}