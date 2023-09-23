#region

using UnityEngine;

#endregion

namespace Units.Colors
{
    public class ColorChanger : MonoBehaviour
    {
        [field: SerializeField] private MaterialData[] _cells;

        private void Start()
        {
            MeshRenderer renderer = gameObject.GetComponent<MeshRenderer>();
            foreach (MaterialData cell in _cells)
            {
                renderer.materials[cell.MatIndex].color = cell.Colors[Random.Range(0, cell.Colors.Length)];
            }
        }
    }
}
