using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipesGameManager : MonoBehaviour
{
    [SerializeField] private GameObject _pipesHold;
    [SerializeField] private int _correctPipesCount;
    [SerializeField] private int _totalPipes = 0;
    void OnEnable()
    {
        _totalPipes = _pipesHold.transform.childCount;
        _correctPipesCount = 0;
    }

    public void CorrectMove()
    {
        _correctPipesCount++;
        if (_totalPipes == _correctPipesCount)
        {
            Debug.Log("Win");
        }
    }

    public void WrongMove()
    {        
        _correctPipesCount--;
    }
}
