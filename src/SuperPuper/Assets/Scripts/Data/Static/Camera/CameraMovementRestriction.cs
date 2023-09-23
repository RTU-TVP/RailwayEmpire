#region

using UnityEngine;

#endregion

namespace Data.Static.Camera
{
    [CreateAssetMenu(fileName = "CameraMovementRestriction", menuName = "Data/Static/CameraMovementRestriction")]
    public class CameraMovementRestriction : ScriptableObject
    {
        [field: SerializeField] public Vector3 MinPosition { get; private set; }
        [field: SerializeField] public Vector3 MaxPosition { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float ZoomSpeed { get; private set; }
        [SerializeField] private Vector3 _rotation;
        public Quaternion Rotation => Quaternion.Euler(_rotation);
    }
}
