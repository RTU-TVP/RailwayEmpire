using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coal : MonoBehaviour
{
    private PlayerController _player;
    public float _speed;
    private void Start()
    {
        _player = FindObjectOfType<PlayerController>();
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
            _player.AddScore(1);
            Destroy(gameObject);
        }
    }
}
