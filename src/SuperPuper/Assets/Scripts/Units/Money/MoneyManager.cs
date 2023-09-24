using System;
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

        public int GetMoney() => PlayerPrefs.GetInt("Money");

        public bool IsEnoughMoney(int money) => PlayerPrefs.GetInt("Money") >= money;

        public void ChangeMoneyTo(int money)
        {
            PlayerPrefs.SetInt("Money", money);
            _onMoneyChanged?.Invoke(PlayerPrefs.GetInt("Money"));
        }
    }
}
