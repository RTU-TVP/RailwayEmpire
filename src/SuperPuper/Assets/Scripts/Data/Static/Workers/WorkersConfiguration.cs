#region

using UnityEngine;

#endregion

namespace Data.Static.Workers
{
    [CreateAssetMenu(fileName = "WorkersConfiguration", menuName = "Data/Static/Workers/WorkersConfiguration")]
    public class WorkersConfiguration : ScriptableObject
    {
        public float MoveSpeedDefault;
        public float WorkTimeDefault;
        public float SaleTimeDefault;
        
        public int MaxWorkers;
    }
}
