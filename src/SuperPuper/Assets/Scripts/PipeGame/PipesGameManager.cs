<<<<<<<< HEAD:src/SuperPuper/Assets/Scripts/Units/Minigames/PipeGame/PipesGameManager.cs
#region

========
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
>>>>>>>> origin/Kotov-GamesByBtn:src/SuperPuper/Assets/Scripts/PipeGame/PipesGameManager.cs
using UnityEngine;

#endregion

public class PipesGameManager : MonoBehaviour
{
    [SerializeField] private GameObject _pipesHold;
    [SerializeField] private int _correctPipesCount;
<<<<<<<< HEAD:src/SuperPuper/Assets/Scripts/Units/Minigames/PipeGame/PipesGameManager.cs
    [SerializeField] private int _totalPipes;
    private void OnEnable()
========
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
>>>>>>>> origin/Kotov-GamesByBtn:src/SuperPuper/Assets/Scripts/PipeGame/PipesGameManager.cs
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
