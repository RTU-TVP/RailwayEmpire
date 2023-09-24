using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StupidTrainSound : MonoBehaviour
{
    void Start()
    {
        GetComponent<AudioManager>().Play("goBy");
    }

}
