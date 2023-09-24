using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StupidTrainDestroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<StupidTrainMove>(out StupidTrainMove train))
        {
            Destroy(other.gameObject);
        }
    }
}
