using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PipeScript : MonoBehaviour
{
    private float[] rotations = { 0, 90, 180, 270 };
    [SerializeField] private PipesGameManager _pipesGameManager;
    [SerializeField] private int[] _correctRotation;
    [SerializeField] private int _currentRotation;
    [field:SerializeField] public bool IsPlaced { get; private set; } = false;

    void Start()
    {
        int rand = Random.Range(0, rotations.Length);
        transform.eulerAngles = new Vector3(0, rotations[rand], 0);
        _currentRotation = rand;

        foreach (float rot in _correctRotation)
        {
            if (_currentRotation == rot)
            {
                IsPlaced = true;
                _pipesGameManager.CorrectMove();
            }
            
        }
        
    }

    // Update is called once per frame
    private void OnMouseDown()
    {
        transform.Rotate(new Vector3(0, 90, 0));
        _currentRotation += 1;
        if (_currentRotation > 3) _currentRotation = 0;
        foreach (float rot in _correctRotation)
        {
            if (_currentRotation == rot)
            {
                IsPlaced = true;
                _pipesGameManager.CorrectMove();
                break;
            }
            else
            {
                if (IsPlaced == true)
                {
                    IsPlaced = false;
                    _pipesGameManager.WrongMove();
                }
            }
        }
    }
}
