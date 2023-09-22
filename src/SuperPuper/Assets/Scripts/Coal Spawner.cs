using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class CoalSpawner : MonoBehaviour
{
    [SerializeField] private Transform _pos1;
    [SerializeField] private Transform _pos2;
    [SerializeField] private GameObject _coal;
    private Random rnd = new Random();
    private void Start()
    {
        StartCoroutine(wait());
    }

    IEnumerator wait()
    {
        while (true)
        {
            int _switch = rnd.Next(0, 2);
            var coalInst = Instantiate(_coal, _switch == 1 ? _pos1.position : _pos2.position, Quaternion.identity);
            yield return new WaitForSeconds(1);
        }
    }
}
