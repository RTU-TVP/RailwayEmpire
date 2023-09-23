#region

using UnityEngine;

#endregion

static public class ChosenVagonInfo
{
    static public GameObject vagonData;
    static public bool isVagonChosen;
    static public void ChooseVagon(GameObject vagon)
    {
        isVagonChosen = true;
        vagonData = vagon;
    }
    static public void StopChoosingVagon()
    {
        isVagonChosen = false;
    }
}
