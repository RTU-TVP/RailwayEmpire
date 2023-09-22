using UnityEngine;

namespace Data.Static.Workers
{
    [CreateAssetMenu(fileName = "WorkersConfiguration", menuName = "Data/Static/Workers/WorkersConfiguration")]
    public class WorkersConfiguration : ScriptableObject
    {
        public float WorkTimeDefault;
        public float MoveSpeedDefault;
    }
}
