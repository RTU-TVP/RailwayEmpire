using System;
using UnityEngine;

namespace Data.Static.Trains
{
	[CreateAssetMenu(fileName = "RailwayCarriage", menuName = "Data/Static/Trains/RailwayCarriage")]
	public class RailwayCarriage : ScriptableObject
	{
		[field: Range(0, 1), SerializeField] public float ChanceForMiniGame { get; private set; }
		[field: SerializeField] public RailwayCarriageType RailwayCarriageType { get; private set; }
		[field: SerializeField] public GameObject Prefab { get; private set; }
	}
}