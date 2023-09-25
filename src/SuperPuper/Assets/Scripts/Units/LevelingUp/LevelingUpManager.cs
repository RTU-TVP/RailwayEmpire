using Units.Money;
using UnityEngine;
using UnityEngine.Events;

namespace Units.LevelingUp
{
    public class LevelingUpManager : MonoBehaviour
    {
        public static LevelingUpManager Instance { get; private set; }
        private UnityAction _onLevelUp;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        public void RegisterOnLevelUp(UnityAction onLevelUp) => _onLevelUp += onLevelUp;
        public void UnregisterOnLevelUp(UnityAction onLevelUp) => _onLevelUp -= onLevelUp;

        public void LevelUp(SkillType type)
        {
            var key = GetKeyByType(type);
            var price = GetPrice(key);
            var currentLevel = PlayerPrefs.GetInt(key);
            if (MoneyManager.Instance.IsEnoughMoney(price) && currentLevel < _skillsDictionary[type].MaxLevel)
            {
                MoneyManager.Instance.ChangeMoneyTo(-price);
                PlayerPrefs.SetInt(key, currentLevel + 1);
                _onLevelUp?.Invoke();
            }
        }

        public int GetPrice(string key)
        {
            var level = PlayerPrefs.GetInt(key);
            return (int)Mathf.Pow(1.618f, level);
        }

        public string GetKeyByType(SkillType type)
        {
            return type switch
            {
                SkillType.MoveSpeed => Data.Constant.WorkersConstantData.WORKERS_LVL_MOVE_SPEED,
                SkillType.WorkTime => Data.Constant.WorkersConstantData.WORKERS_LVL_WORK_TIME,
                SkillType.SaleTime => Data.Constant.WorkersConstantData.WORKERS_LVL_SALE_TIME,
                _ => string.Empty
            };
        }
    }

}
