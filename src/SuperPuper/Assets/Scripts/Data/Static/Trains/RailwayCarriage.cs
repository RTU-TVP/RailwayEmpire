using System;
using UnityEngine;

namespace Data.Static.Trains
{
	[CreateAssetMenu(fileName = "RailwayCarriage", menuName = "Data/Static/Trains/RailwayCarriage")]
	public class RailwayCarriage : ScriptableObject
	{
		[field: SerializeField] public RailwayCarriageType RailwayCarriageType { get; private set; }
		[field: SerializeField] public GameObject Prefab { get; private set; }
	}
}