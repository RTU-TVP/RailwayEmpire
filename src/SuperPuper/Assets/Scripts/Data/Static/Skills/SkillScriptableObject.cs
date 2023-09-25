using Units.LevelingUp;
using UnityEngine;

namespace Data.Static.Skills
{
    [CreateAssetMenu(fileName = "Skill", menuName = "Data/Static/Skills/Skill", order = 0)]
    public class SkillScriptableObject : ScriptableObject
    {
        [field: SerializeField] public int AdditionalPrice { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public SkillType Type { get; private set; }
    }
}
