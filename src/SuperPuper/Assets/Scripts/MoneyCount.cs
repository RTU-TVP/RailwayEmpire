using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyCount : MonoBehaviour
{
    void Update()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = Convert.ToString(MoneyAndUpgradesStats.money);
    }
}
