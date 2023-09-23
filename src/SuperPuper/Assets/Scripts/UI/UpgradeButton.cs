#region

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
        private void Start()
        {
            currentLevel = 1;
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
            if (currentLevel < _costsPerLevel.Count && MoneyAndUpgradesStats.money >= _costsPerLevel[currentLevel])
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
                    MoneyAndUpgradesStats.levelWalkSpeed = currentLevel;
                }
                else
                {
                    if (typeOfUpgrade == TypeOfUpgrade.UnloadSpeed)
                    {
                        MoneyAndUpgradesStats.levelUnloadSpeed = currentLevel;
                    }
                    else
                    {
                        if (typeOfUpgrade == TypeOfUpgrade.SellSpeed)
                        {
                            MoneyAndUpgradesStats.levelSellSpeed = currentLevel;
                        }
                    }
                }
                MoneyAndUpgradesStats.money -= _costsPerLevel[currentLevel - 1];
            }
        }
    }
}
