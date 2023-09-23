#region

using UnityEngine;

#endregion

public class ColorChanger : MonoBehaviour
{
    [field: SerializeField] private IdToMaterial[] _cells;

    private void Start()
    {
        MeshRenderer renderer = gameObject.GetComponent<MeshRenderer>();
        foreach (IdToMaterial cell in _cells)
        {
            renderer.materials[cell.MatIndex].color = cell.Colors[Random.Range(0, cell.Colors.Length)];
        }
    }
}
