using UnityEngine;

namespace Train
{
	public class RailTrack : MonoBehaviour
	{
		[field: SerializeField] public Transform StartPoint { get; private set; }
		[field: SerializeField] public Transform EndPoint { get; private set; }
		public bool IsOccupied { get; private set; } = false;

		public void SetOccupied(bool occupied)
		{
			IsOccupied = occupied;
		}
	}
}