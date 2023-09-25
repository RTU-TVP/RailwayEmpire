using System;
using Data.Constant;
using UI;
using UnityEngine;
using UnityEngine.Events;

namespace Units.Money
{
    public class MoneyManager : MonoBehaviour
    {
        public static MoneyManager Instance { get; private set; }
        [SerializeField] private GameObject _moneyScreen;
        [SerializeField] private Transform _instanceParent;
        private UnityAction<int> _onMoneyChanged;
        private int _money;
        private MoneyUI _moneyUI;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        private void Start()
        {
            _moneyUI = Instantiate(_moneyScreen, _instanceParent).GetComponent<MoneyUI>();
            _money = PlayerPrefs.GetInt(WorkersConstantData.MONEY);
            _onMoneyChanged?.Invoke(_money);
        }

        public void RegisterOnMoneyChanged(UnityAction<int> onMoneyChanged) => _onMoneyChanged += onMoneyChanged;
        public void UnregisterOnMoneyChanged(UnityAction<int> onMoneyChanged) => _onMoneyChanged -= onMoneyChanged;

        public int GetMoney() => _money;

        public bool IsEnoughMoney(int money) => _money >= money;

        public void ChangeMoneyTo(int money)
        {
            _money += money;
            _onMoneyChanged?.Invoke(_money);
            PlayerPrefs.SetInt(WorkersConstantData.MONEY, _money);
        }
    }
}
