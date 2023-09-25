using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarRide : MonoBehaviour
{
    GameObject point1;
    GameObject point2;
    public bool direction;
    void Awake()
    {
        point1 = GameObject.Find("Point1");
        point2 = GameObject.Find("Point2");
    }
    private void Start()
    {
        GetComponent<AudioManager>().Play("rideSound");
    }

    void Update()
    {
        if(direction)
        {
            transform.position += (point2.transform.position - point1.transform.position).normalized * Time.deltaTime * Random.Range(30, 75);
        }
        else
        {
            transform.position -= (point2.transform.position - point1.transform.position).normalized * Time.deltaTime * Random.Range(30, 75);
        }

    }
}
