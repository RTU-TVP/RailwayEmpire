using System;
using TMPro;
using Units.Money;
using UnityEngine;

namespace UI
{
    public class MoneyUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _moneyText;

        private void OnEnable()
        {
            SetMoney(MoneyManager.Instance.GetMoney());
            MoneyManager.Instance.RegisterOnMoneyChanged(SetMoney);
        }
        
        private void OnDisable()
        {
            MoneyManager.Instance.UnregisterOnMoneyChanged(SetMoney);
        }

        public void SetMoney(int money)
        {
            _moneyText.text = money.ToString();
        }
    }
}
