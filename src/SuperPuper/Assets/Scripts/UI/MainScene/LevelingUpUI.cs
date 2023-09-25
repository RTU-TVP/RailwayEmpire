using Data.Static.Skills;
using Units.LevelingUp;
using UnityEngine;

namespace UI.MainScene
{
    public class LevelingUpUI : MonoBehaviour
    {
        [SerializeField] private SkillScriptableObject[] _skillsData;
        [SerializeField] private GameObject _skillPrefab;
        [SerializeField] private Transform _skillsParent;
        private SkillUI[] _skillsUI;

        private void Awake()
        {
            _skillsUI = new SkillUI[_skillsData.Length];
            for (int i = 0; i < _skillsData.Length; i++)
            {
                var skillUI = Instantiate(_skillPrefab, _skillsParent).GetComponent<SkillUI>();
                _skillsUI[i] = skillUI;
            }
        }

        private void OnEnable()
        {
            SetSkillsDefaultData();
            UpdateSkillsData();
            LevelingUpManager.Instance.RegisterOnLevelUp(UpdateSkillsData);
        }

        private void OnDisable()
        {
            LevelingUpManager.Instance.UnregisterOnLevelUp(UpdateSkillsData);
        }

        private void SetSkillsDefaultData()
        {
            for (int i = 0; i < _skillsData.Length; i++)
            {
                var data = _skillsData[i];
                var skillUI = _skillsUI[i];

                skillUI.SetIcon(data.Icon);
                skillUI.SetDescription(data.Description);
                skillUI.SetButtonAction(() => OnImprovementsPurchase(data.Type));
            }
        }

        private void UpdateSkillsData()
        {
            for (int i = 0; i < _skillsUI.Length; i++)
            {
                var skillUI = _skillsUI[i];
                var data = _skillsData[i];
                var key = LevelingUpManager.Instance.GetKeyByType(data.Type);

                skillUI.SetLevel(PlayerPrefs.GetInt(key));
                skillUI.SetPrice(LevelingUpManager.Instance.GetPrice(key) + data.AdditionalPrice);
            }
        }

        private void OnImprovementsPurchase(SkillType type)
        {
            LevelingUpManager.Instance.LevelUp(type);
        }
    }
}
