using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StupidTrainMove : MonoBehaviour
{
    [SerializeField] bool rightOrLeft = true;
    Vector3 pos;
    void Start()
    {
        pos = gameObject.transform.position;
    }
    void Update()
    {
        if(rightOrLeft)
        {
            pos.x -= 100 * Time.deltaTime;
        }
        else
        {
            pos.x += 100 * Time.deltaTime;
        }
        gameObject.transform.position = pos;
    }
}
