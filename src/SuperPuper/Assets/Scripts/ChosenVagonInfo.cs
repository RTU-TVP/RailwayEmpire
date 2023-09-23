using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class ChosenVagonInfo
{
    public static GameObject vagonData;
    public static bool isVagonChosen = false;
    public static void ChooseVagon(GameObject vagon)
    {
        isVagonChosen = true;
        vagonData = vagon;
    }
    public static void StopChoosingVagon()
    {
        isVagonChosen = false;
    }
}
