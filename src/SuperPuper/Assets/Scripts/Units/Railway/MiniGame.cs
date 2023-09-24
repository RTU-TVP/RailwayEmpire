using System;
using Data.Static.Trains;
using UnityEngine;

namespace Units.Railway
{
    [Serializable]
    public class MiniGame
    {
        [field: SerializeField] public RailwayCarriageType RailwayCarriageType { get; private set; }
        [field: SerializeField] public GameObject MiniGamePrefab { get; private set; }
        [field: SerializeField] public Vector3 MiniGamePosition { get; private set; }
        [field: SerializeField] public Vector3 CameraPosition { get; private set; }
    }
}