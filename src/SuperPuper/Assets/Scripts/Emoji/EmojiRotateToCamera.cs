using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EmojiRotateToCamera : MonoBehaviour
{
    void Update()
    {
        transform.LookAt(UnityEngine.Camera.main.transform);
    }
}
