using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayerForCamera : MonoBehaviour
{
    void Start()
    {
        GetComponent<AudioManager>().Play("ambient");
    }
}
