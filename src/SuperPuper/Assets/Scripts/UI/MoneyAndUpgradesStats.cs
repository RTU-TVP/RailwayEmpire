using Data.Constant;
using UnityEngine;

namespace UI
{
    static public class MoneyStats
    {
        static public int money = 500;
        public static void ChangeMoney(int amount)
        {
            money += amount;
            PlayerPrefs.SetInt(WorkersConstantData.MONEY, money);
        }
    }
}
