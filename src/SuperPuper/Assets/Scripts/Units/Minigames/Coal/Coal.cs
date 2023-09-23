using System;
using System.Collections;
using System.Collections.Generic;
using Units.Minigames.Coal;
using UnityEngine;

public class Coal : MonoBehaviour
{
    private PlayerController _coal;
    private CoalSpawner _coalSpawner;
    private void Start()
    {
        _coal = FindObjectOfType<PlayerController>();
        _coalSpawner = FindObjectOfType<CoalSpawner>();
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            _coal.AddLostScore(1);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            if (_coal.time >= 0.2f)
            {
                _coal.time -= 0.05f;
            }
            _coalSpawner.waitTime -= 0.1f;
            _coal.AddScore(1);
            Destroy(gameObject);
        }
    }
}
