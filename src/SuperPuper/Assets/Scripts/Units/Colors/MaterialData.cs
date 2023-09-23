#region

using System;
using UnityEngine;

#endregion

namespace Units.Colors
{
    [Serializable]
    public class MaterialData
    {
        [field: SerializeField] public int MatIndex { get; private set; }
        [field: SerializeField] public Color[] Colors { get; private set; }
    }
}
