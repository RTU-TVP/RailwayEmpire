using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coal : MonoBehaviour
{
    private PlayerController _player;
    private CoalSpawner _coalSpawner;
    private void Start()
    {
        _player = FindObjectOfType<PlayerController>();
        _coalSpawner = FindObjectOfType<CoalSpawner>();
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            _player.AddLostScore(1);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            if (_player.time >= .2f)
            {
                _player.time -= .05f;
            }
            _coalSpawner.waitTime -= .1f;
            _player.AddScore(1);
            Destroy(gameObject);
        }
    }
}
