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

        public void LevelUp(string type)
        {
            var level = PlayerPrefs.GetInt(type);
            PlayerPrefs.SetInt(type, level + 1);
            _onLevelUp?.Invoke();
        }
    }
}
