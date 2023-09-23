#region

using System;
using UnityEngine;

#endregion

[Serializable]
public class IdToMaterial
{
    [field: SerializeField] public int MatIndex { get; private set; }
    [field: SerializeField] public Color[] Colors { get; private set; }
}
