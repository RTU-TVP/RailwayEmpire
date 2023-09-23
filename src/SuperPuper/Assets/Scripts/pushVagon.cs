#region

using UnityEngine;

#endregion

public class pushVagon : MonoBehaviour
{
    private RaycastHit _hit;
    private UnityEngine.Camera _mainCamera;
    private Ray _ray;
    private GameObject buttons;
    private bool isMenuActive;
    private bool isVagonPressed;
    private void Start()
    {
        _mainCamera = UnityEngine.Camera.main;
        buttons = FindObjectOfType<ButtonsFollowMouse>().gameObject;
    }
    private void Update()
    {
        _ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        isVagonPressed = Physics.Raycast(_ray, out _hit, 1000f) && _hit.transform == transform && Input.GetMouseButtonUp(0);
        if (Physics.Raycast(_ray, out _hit, 1000f) && _hit.transform == transform)
        {
            GetComponent<Outline>().enabled = true;
        }
        else
        {
            GetComponent<Outline>().enabled = false;
        }
        if (Input.GetMouseButtonUp(0))
        {

            if (isVagonPressed)
            {
                buttons.GetComponent<ButtonsFollowMouse>().TeleportToMouse();
                ChosenVagonInfo.ChooseVagon(gameObject);
                buttons.gameObject.SetActive(true);
                isMenuActive = true;
            }
            else
            {
                ChosenVagonInfo.StopChoosingVagon();
                buttons.gameObject.SetActive(false);
                isMenuActive = false;
            }
        }
        if (Input.anyKey && isMenuActive && !Input.GetMouseButton(0))
        {
            ChosenVagonInfo.StopChoosingVagon();
            buttons.gameObject.SetActive(false);
            isMenuActive = false;
        }
        if (Input.GetMouseButton(0))
        {
            GetComponent<Outline>().OutlineColor = new Color32(255, 0, 0, 255);
        }
        else
        {
            GetComponent<Outline>().OutlineColor = new Color32(255, 108, 0, 148);
        }
    }
}
