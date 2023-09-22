using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsFollowMouse : MonoBehaviour
{
    Camera cam;
    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (ChosenVagonInfo.isVagonChosen)
        {
            ChosenVagonInfo.StopChoosingVagon();
            TeleportToMouse();
        }
    }
    public void TeleportToMouse()
    {
        transform.position = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 9));
    }
}
