﻿#region

using UnityEngine;

#endregion

namespace Units.Minigames.TowerBlocks
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
