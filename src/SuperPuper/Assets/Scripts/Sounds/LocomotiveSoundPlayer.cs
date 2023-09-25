using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocomotiveSoundPlayer : MonoBehaviour
{
    void Start()
    {
        GetComponent<AudioManager>().Play("arrive");
    }
}
