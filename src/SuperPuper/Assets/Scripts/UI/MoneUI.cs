using System;
using TMPro;
using Units.Money;
using UnityEngine;

namespace UI
{
    public class MoneUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _moneyText;

        private void Start()
        {
            SetMoney(MoneyManager.Instance.GetMoney());
            MoneyManager.Instance.RegisterOnMoneyChanged(SetMoney);
        }

        public void SetMoney(int money)
        {
            _moneyText.text = money.ToString();
        }
    }
}
