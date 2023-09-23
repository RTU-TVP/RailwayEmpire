#region

using UnityEngine;

#endregion

namespace Data.Static
{
    [CreateAssetMenu(fileName = "CameraMovementRestriction", menuName = "Data/Static/CameraMovementRestriction")]
    public class CameraMovementRestriction : ScriptableObject
    {
        [field: SerializeField] public Vector2 MinPosition { get; private set; }
        [field: SerializeField] public Vector2 MaxPosition { get; private set; }
        [field: SerializeField] public float MinZoom { get; private set; }
        [field: SerializeField] public float MaxZoom { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float ZoomSpeed { get; private set; }
        [SerializeField] private Vector3 _rotation;
        public Quaternion Rotation => Quaternion.Euler(_rotation);
    }
}
