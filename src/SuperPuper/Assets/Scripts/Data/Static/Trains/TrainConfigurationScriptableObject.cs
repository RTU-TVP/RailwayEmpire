#region

using UnityEngine;

#endregion

namespace Data.Static.Trains
{
    [CreateAssetMenu(fileName = "TrainConfiguration", menuName = "Data/Static/Trains/TrainConfiguration")]
    public class TrainConfigurationScriptableObject : ScriptableObject
    {
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public Color32 OutlineColorDefault { get; private set; }
        [field: SerializeField] public Color32 OutlineColorChosen { get; private set; }
        [field: Range(0, 10), SerializeField] public float OutlineIntensity { get; private set; }
        
        [field: SerializeField] public Vector3 ScreenPosition { get; private set; }
        [field: SerializeField] private Vector3 _screenRotation { get; set; }
        public Quaternion ScreenRotation => Quaternion.Euler(_screenRotation);
    }
}
