using UnityEngine;

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
	}
}