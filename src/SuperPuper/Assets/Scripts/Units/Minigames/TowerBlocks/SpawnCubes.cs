#region

using UnityEngine;

#endregion

namespace Tower_Blocks_minigame
{
    public class SpawnCubes : MonoBehaviour
    {

        public GameObject CubePrefab;

        private void Update()
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(CubePrefab, transform.position, Quaternion.identity);
            }
        }
    }
}
