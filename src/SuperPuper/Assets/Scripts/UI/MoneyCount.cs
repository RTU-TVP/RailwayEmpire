#region

using System;
using TMPro;
using UnityEngine;

#endregion

public class MoneyCount : MonoBehaviour
{
    private void Update()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = Convert.ToString(MoneyAndUpgradesStats.money);
    }
}
