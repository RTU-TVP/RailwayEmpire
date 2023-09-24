using System;
using Data.Constant;
using Data.Static.Workers;
using UnityEngine;
using UnityEngine.Events;

namespace Units.Money
{
    public class MoneyManager : MonoBehaviour
    {
        public static MoneyManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        private UnityAction<int> _onMoneyChanged;

        public void RegisterOnMoneyChanged(UnityAction<int> onMoneyChanged) => _onMoneyChanged += onMoneyChanged;
        public void UnregisterOnMoneyChanged(UnityAction<int> onMoneyChanged) => _onMoneyChanged -= onMoneyChanged;

        public int GetMoney() => PlayerPrefs.GetInt(WorkersConstantData.MONEY);

        public bool IsEnoughMoney(int money) => PlayerPrefs.GetInt(WorkersConstantData.MONEY) >= money;

        public void ChangeMoneyTo(int money)
        {
            PlayerPrefs.SetInt(WorkersConstantData.MONEY, money);
            _onMoneyChanged?.Invoke(PlayerPrefs.GetInt(WorkersConstantData.MONEY));
        }
    }
}
