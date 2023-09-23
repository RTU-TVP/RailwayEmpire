#region

using System;
using TMPro;
using UnityEngine;

#endregion

namespace UI
{
    public class MoneyCount : MonoBehaviour
    {
        private void Update()
        {
            gameObject.GetComponent<TextMeshProUGUI>().text = Convert.ToString(MoneyStats.money);
        }
    }
}
