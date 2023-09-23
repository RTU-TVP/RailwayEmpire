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
    public bool _canStart;
    private WarningController _warningController;
    private Random rnd = new Random();
    private Vector3 _spawnPosition;
    public float waitTime = 1.0f;
        
    public void GoGame()
    {
        if (_canStart)
        {
            _warningController = FindObjectOfType<WarningController>();
            StartCoroutine(wait(waitTime));
        }
    }

    IEnumerator wait(float time)
    {
        
        int i = 1;
        while (true)
        {
            switch (rnd.Next(0, 4))
            {
                case 0:
                    _warningController.ShowSec(2);
                    _spawnPosition = _pos1.position;
                    break;
                case 1:
                    _warningController.ShowSec(3);
                    _spawnPosition = _pos2.position;
                    break;
                case 2:
                    _warningController.ShowSec(0);
                    _spawnPosition = _pos3.position;
                    break;
                case 3:
                    _warningController.ShowSec(1);
                    _spawnPosition = _pos4.position;
                    break;
            }
            var coalInst = Instantiate(_coal,_spawnPosition, Quaternion.identity);
            coalInst.GetComponent<CustomGravity>().gravityScale += i;
            i++;
            if(time >= .5f)
            {
                time -= .01f;
            }
            yield return new WaitForSeconds(time);
        }
    }
}
