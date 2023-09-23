using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PipesGameManager : MonoBehaviour
{
    [SerializeField] private GameObject _pipesHold;
    [SerializeField] private int _correctPipesCount;
    [SerializeField] private TMP_Text _timerUI;
    [SerializeField] private int _totalPipes = 0;
    
    [SerializeField] private float _looseTimer;
    void FixedUpdate()
    {
        _looseTimer -= Time.deltaTime;
        _timerUI.text = "Осталось времени: "+((int)_looseTimer).ToString();
        if (_looseTimer <= 0) {print("Loose"); FindObjectOfType<PipeGameLoader>().DestroyGame();}
    }

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
            FindObjectOfType<PipeGameLoader>().DestroyGame();
            Debug.Log("Win");
        }
    }

    public void WrongMove()
    {        
        _correctPipesCount--;
    }
}
