using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MovementSpawn : MonoBehaviour {

	private bool conditionMovementHorizontal = true;
	public Camera cameraPrincipal;
	private int _score;
	public GameObject CubePrefab;
	private bool _canSpawn = true;
	private float _spawnTimer = 0;
	public bool _hasStarted;
	[SerializeField] private TextMeshProUGUI _scoreText;

	private void Start()
	{
		cameraPrincipal = FindObjectOfType<Camera>();
	}

	void Update () 
	{
		if (_hasStarted)
		{
			Movement();
			UpdateTime();
		}
	}

	private void Movement()
	{
		if (Input.GetKeyDown (KeyCode.Space)  && _canSpawn) 
		{
			SpawnCube();
			transform.Translate (new Vector3(0,1.9f,0));
			cameraPrincipal.transform.Translate (new Vector3(0,1.9f,0));
		}
		if (transform.position.x <= 6 && conditionMovementHorizontal) 
		{
			transform.Translate (Vector3.right * (Time.deltaTime * 5));
			if (transform.position.x >= 6) 
			{
				conditionMovementHorizontal = false;
			}
		}
		if (!conditionMovementHorizontal) 
		{
			transform.Translate (-Vector3.right * (Time.deltaTime * 5));
			if (transform.position.x <= -6) 
			{
				conditionMovementHorizontal = true;
			}
		}
	}

	public void AddScore(int points)
	{
		_score += points;
		_scoreText.text = "Score: " + _score;
		if (_score == 10)
		{
			print("Winner Winner Chicken Dinner");
		}
	}

	private void UpdateTime()
	{
		_spawnTimer += Time.deltaTime;
		if (_spawnTimer >= 1.0f)
		{
			_spawnTimer = 0;
			_canSpawn = true;
		}
	}

	private void SpawnCube()
	{
		if (Input.GetKeyDown (KeyCode.Space) && _canSpawn) {
			Instantiate (CubePrefab, transform.position, Quaternion.Euler(-90,0,90));
			_canSpawn = false;
		}
	}
}
