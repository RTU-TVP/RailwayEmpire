#region

using UnityEngine;

#endregion

namespace Data.Static.Trains
{
    [CreateAssetMenu(fileName = "TrainConfiguration", menuName = "Data/Static/Trains/TrainConfiguration")]
    public class TrainConfigurationScriptableObject : ScriptableObject
    {
        [field: SerializeField] public float Speed { get; private set; }
    }
}
