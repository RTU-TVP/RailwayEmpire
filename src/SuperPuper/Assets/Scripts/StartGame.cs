using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Units.Minigames.Coal;
using Units.Minigames.TowerBlocks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    [SerializeField] private GameObject _game;
    private MovementSpawn _movementSpawn;
    private CoalSpawner _coalSpawner;
    private UnityAction _action;
    private Button _button;

    private void Start()
    {
        _button = transform.GetComponent<Button>();
        _action += Play;
        _button.onClick.AddListener(_action);
        _action -= Play;
    }

    public void Play()
    {
        Instantiate(_game);
        _action += GameStart;
        if (FindObjectOfType<MovementSpawn>() != null)
        {
            _movementSpawn = FindObjectOfType<MovementSpawn>();
        }
        if (FindObjectOfType<CoalSpawner>() != null)
        {
            _coalSpawner = FindObjectOfType<CoalSpawner>();
        }
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(_action);
        _button.GetComponentInChildren<TextMeshProUGUI>().text = "Start";
    }

    public void GameStart()
    {
        if (_coalSpawner != null)
        {
            _coalSpawner._canStart = true;
            _coalSpawner.GoGame();
        }
        if (_movementSpawn != null)
        {
            _movementSpawn._hasStarted = true;
        }        
        Destroy(transform.parent.gameObject);
    }
}
