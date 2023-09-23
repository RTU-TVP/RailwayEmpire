#region

using System;
using UnityEngine;

#endregion

namespace Color
{
    [Serializable]
    public class MaterialData
    {
        [field: SerializeField] public int MatIndex { get; private set; }
        [field: SerializeField] public UnityEngine.Color[] Colors { get; private set; }
    }
}
