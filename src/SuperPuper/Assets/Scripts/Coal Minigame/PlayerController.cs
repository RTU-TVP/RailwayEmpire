using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int _score;
    [SerializeField] private Transform _playerPosition1;
    [SerializeField] private Transform _playerPosition2;
    [SerializeField] private Transform _playerPosition3;
    [SerializeField] private Transform _playerPosition4;
    [SerializeField] private TextMeshProUGUI _scoreScreen;

    private void Update()
    {
        Move();
    }

    public void AddScore(int points)
    {
        _score += points;
        _scoreScreen.text = "Score: " + _score;
    }

    void Move()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            transform.position = _playerPosition1.position;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            transform.position = _playerPosition2.position;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            transform.position = _playerPosition3.position;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            transform.position = _playerPosition4.position;
        }
    }
}
