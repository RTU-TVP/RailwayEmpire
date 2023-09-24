#region

using System;
using TMPro;
using UnityEngine;

#endregion

namespace Units.Minigames.PipeGame
{
    public class PipesGameManager : MonoBehaviour
    {
        [SerializeField] private GameObject _pipesHold;
        [SerializeField] private int _correctPipesCount;
        [SerializeField] private int _totalPipes;
        [SerializeField] private TMP_Text _timerUI;
        [SerializeField] private float _looseTimer;
        
        public event Action OnGameCompleted;
        public event Action OnGameFailed;
    
        void FixedUpdate()
        {
            _looseTimer -= Time.deltaTime;
            _timerUI.text = "Осталось времени: "+((int)_looseTimer);
            if (_looseTimer <= 0) {print("Loose"); OnGameFailed?.Invoke();}
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
               // FindObjectOfType<PipeGameLoader>().DestroyGame();
                Debug.Log("Win");
                
                OnGameCompleted?.Invoke();
            }
        }

        public void WrongMove()
        {
            _correctPipesCount--;
        }
    }
}
