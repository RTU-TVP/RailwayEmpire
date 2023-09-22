using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    [field: SerializeField] private IdToMaterial[] _cells;

    private void Start()
    {
        MeshRenderer renderer = gameObject.GetComponent<MeshRenderer>();
        foreach (IdToMaterial cell in _cells)
        {
            renderer.materials[cell.MatIndex].color = cell.Colors[UnityEngine.Random.Range(0, cell.Colors.Length)];
        }
    }
    
}

[Serializable]
public class IdToMaterial
{
    [field:SerializeField] public int MatIndex { get; private set; }
    [field:SerializeField] public Color[] Colors { get; private set; }
}