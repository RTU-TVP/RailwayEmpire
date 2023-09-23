#region

using UnityEngine;

#endregion

public class ButtonsFollowMouse : MonoBehaviour
{
    private UnityEngine.Camera cam;
    private void Start()
    {
        cam = UnityEngine.Camera.main;
    }

    private void Update()
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
