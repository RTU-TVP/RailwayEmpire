using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CondicionDeDerrota : MonoBehaviour {

	private Scene escena;

	void Start () {
		escena = SceneManager.GetActiveScene ();
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Piso") {
			SceneManager.LoadScene (escena.name);
		}
	}
}
