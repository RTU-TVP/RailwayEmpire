using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PipeGameLoader : MonoBehaviour
{
    [SerializeField] private Button _button;
    [Header("Prefab PipeGame")]
    [SerializeField] private GameObject _gamePrefab;

    [Header("Позиция относительно земли ~5,5")]
    [SerializeField] private Vector3 _position = new Vector3(0, 5.5f, 0);

    private GameObject InstObject;

    private void Start()
    {
        _button.onClick.AddListener(ShowGame);
    }

    private void ShowGame()
    {
        InstObject=Instantiate(_gamePrefab, _position, Quaternion.identity);
    }

    public void DestroyGame()
    {
        Destroy(InstObject);
    }
}
