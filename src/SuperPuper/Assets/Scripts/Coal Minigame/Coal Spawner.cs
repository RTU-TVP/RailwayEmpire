using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class CoalSpawner : MonoBehaviour
{
    [SerializeField] private Transform _pos1;
    [SerializeField] private Transform _pos2;
    [SerializeField] private Transform _pos3;
    [SerializeField] private Transform _pos4;
    [SerializeField] private GameObject _coal;
    private Random rnd = new Random();
    private Vector3 _spawnPosition;
    private void Start()
    {
        StartCoroutine(wait());
    }

    IEnumerator wait()
    {
        while (true)
        {
            //int _switch = rnd.Next(0, 4);
            switch (rnd.Next(0, 4))
            {
                case 0:
                    _spawnPosition = _pos1.position;
                    break;
                case 1:
                    _spawnPosition = _pos2.position;
                    break;
                case 2:
                    _spawnPosition = _pos3.position;
                    break;
                case 3:
                    _spawnPosition = _pos4.position;
                    break;
            }
            var coalInst = Instantiate(_coal,_spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(1);
        }
    }
}
