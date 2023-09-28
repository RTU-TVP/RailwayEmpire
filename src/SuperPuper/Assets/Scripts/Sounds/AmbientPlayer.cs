using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientPlayer : MonoBehaviour
{
    void Start()
    {
        GetComponent<AudioManager>().Play("ambient");
    }
}
