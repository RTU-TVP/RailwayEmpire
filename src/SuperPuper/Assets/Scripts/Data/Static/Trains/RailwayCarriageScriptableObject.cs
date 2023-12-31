#region

using UnityEngine;

#endregion

namespace Data.Static.Trains
{
    [CreateAssetMenu(fileName = "RailwayCarriage", menuName = "Data/Static/Trains/RailwayCarriage")]
    public class RailwayCarriageScriptableObject : ScriptableObject
    {
        [field: SerializeField] public RailwayCarriageType RailwayCarriageType { get; private set; }
        [field: SerializeField] public bool IsInteractive { get; private set; }
        [field: SerializeField] public float Lifetime { get; private set; }
        [field: SerializeField] public GameObject Prefab { get; private set; }
        [field: SerializeField] public int Money { get; private set; }
    }
}
