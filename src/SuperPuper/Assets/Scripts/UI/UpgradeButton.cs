#region

using Data.Constant;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

#endregion

namespace UI
{
    public class UpgradeButton : MonoBehaviour
    {
        public enum TypeOfUpgrade
        {
            Walkspeed,
            UnloadSpeed,
            SellSpeed
        }
        [SerializeField]
        private GameObject _levelText;
        [SerializeField]
        private GameObject _nextLvlPriceText;
        [SerializeField]
        private TypeOfUpgrade typeOfUpgrade;
        [SerializeField] private List<int> _costsPerLevel = new List<int>();
        private int currentLevel;
        private void Awake()
        {
            currentLevel = 1;
            MoneyStats.money = 500;
            if (typeOfUpgrade == TypeOfUpgrade.Walkspeed && PlayerPrefs.HasKey(WorkersConstantData.WORKERS_LVL_MOVE_SPEED))
            {
                currentLevel = PlayerPrefs.GetInt(WorkersConstantData.WORKERS_LVL_MOVE_SPEED);
            }
            if (typeOfUpgrade == TypeOfUpgrade.UnloadSpeed && PlayerPrefs.HasKey(WorkersConstantData.WORKERS_LVL_WORK_TIME))
            {
                currentLevel = PlayerPrefs.GetInt(WorkersConstantData.WORKERS_LVL_WORK_TIME);
            }
            if (typeOfUpgrade == TypeOfUpgrade.SellSpeed && PlayerPrefs.HasKey(WorkersConstantData.WORKERS_LVL_SALE_TIME))
            {
                currentLevel = PlayerPrefs.GetInt(WorkersConstantData.WORKERS_LVL_SALE_TIME);
            }
            if (PlayerPrefs.HasKey(WorkersConstantData.MONEY))
            {
                MoneyStats.money = PlayerPrefs.GetInt(WorkersConstantData.MONEY);
            }
        }
        private void Update()
        {
            if (currentLevel < _costsPerLevel.Count)
            {
                _nextLvlPriceText.GetComponent<TextMeshProUGUI>().text = Convert.ToString(_costsPerLevel[currentLevel]);
            }
            else
            {
                _nextLvlPriceText.GetComponent<TextMeshProUGUI>().text = "None";
            }
            _levelText.GetComponent<TextMeshProUGUI>().text = Convert.ToString(currentLevel);
            if (currentLevel < _costsPerLevel.Count && MoneyStats.money >= _costsPerLevel[currentLevel])
            {
                gameObject.GetComponent<Button>().interactable = true;
            }
            else
            {
                gameObject.GetComponent<Button>().interactable = false;
            }
        }
        public void OnClickEvent()
        {
            if (currentLevel < _costsPerLevel.Count)
            {
                currentLevel++;
                if (typeOfUpgrade == TypeOfUpgrade.Walkspeed)
                {
                    PlayerPrefs.SetInt(WorkersConstantData.WORKERS_LVL_MOVE_SPEED, currentLevel);
                }
                if (typeOfUpgrade == TypeOfUpgrade.SellSpeed)
                {
                    PlayerPrefs.SetInt(WorkersConstantData.WORKERS_LVL_SALE_TIME, currentLevel);
                }
                if (typeOfUpgrade == TypeOfUpgrade.UnloadSpeed)
                {
                    PlayerPrefs.SetInt(WorkersConstantData.WORKERS_LVL_WORK_TIME, currentLevel);
                }
                MoneyStats.ChangeMoney(-_costsPerLevel[currentLevel - 1]);
            }
        }
    }
}
