using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

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
            transform.DORotate(new Vector3(0, 0, 0), .25f);
            transform.DOMove(_playerPosition1.position,.25f);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.DORotate(new Vector3(0, 180, 0), .25f);
            transform.DOMove(_playerPosition2.position,.25f);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            transform.DORotate(new Vector3(0, 0, 0), .25f);
            transform.DOMove(_playerPosition3.position,.25f);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            transform.DORotate(new Vector3(0, 180, 0), .25f);
            transform.DOMove(_playerPosition4.position,.25f);
        }
    }
}
