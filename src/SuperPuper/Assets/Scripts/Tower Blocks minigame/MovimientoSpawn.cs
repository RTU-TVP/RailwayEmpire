using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoSpawn : MonoBehaviour {

	private bool condicionMovimientoHorizontal = true;
	private bool condicionMovimientoVertical = true;
	public Camera camaraPrincipal;

	void Update () {

		if (Input.GetKeyDown (KeyCode.Space) && condicionMovimientoVertical) {
			transform.Translate (Vector3.up * Time.deltaTime * 85);
			camaraPrincipal.transform.Translate (Vector3.up * Time.deltaTime * 85);
			condicionMovimientoVertical = false;
		}
		if (transform.position.x <= 6 && condicionMovimientoHorizontal) {
			transform.Translate (Vector3.right * Time.deltaTime * 5);
			if (transform.position.x >= 6) {
				condicionMovimientoHorizontal = false;
			}
		}
		if (!condicionMovimientoHorizontal) {
			transform.Translate (-Vector3.right * Time.deltaTime * 5);
			if (transform.position.x <= -6) {
				condicionMovimientoHorizontal = true;
			}
		}
		condicionMovimientoVertical = true;
	}
}
