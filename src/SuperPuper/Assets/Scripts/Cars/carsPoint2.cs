using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carsPoint2 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<CarRide>(out CarRide car))
        {
            Destroy(car.gameObject);
        }
    }
}
