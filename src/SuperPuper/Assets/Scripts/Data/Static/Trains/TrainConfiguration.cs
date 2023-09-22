using UnityEngine;

namespace Data.Static.Trains
{
	[CreateAssetMenu(fileName = "TrainConfiguration", menuName = "Data/Static/Trains/TrainConfiguration")]
	public class TrainConfiguration : ScriptableObject
	{
		[field: SerializeField] public float Speed { get; private set; }
	}
}