using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pushVagon : MonoBehaviour
{
    private Camera _mainCamera;
    private Renderer _renderer;
    private Ray _ray;
    private RaycastHit _hit;
    GameObject buttons;
    bool isVagonPressed = false;
    bool isMenuFollowing = true;
    void Start()
    {
        _mainCamera = Camera.main;
        _renderer = GetComponent<Renderer>();
        buttons = FindObjectOfType<ButtonsFollowMouse>().gameObject;
    }
    void Update()
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
                ChosenVagonInfo.ChooseVagon(this.gameObject);
                buttons.gameObject.SetActive(true);
            }
            else
            {
                ChosenVagonInfo.StopChoosingVagon();
                buttons.gameObject.SetActive(false);
            }
        }
        if(Input.GetMouseButton(0))
        {
            GetComponent<Outline>().OutlineColor = new Color32(255, 0, 0, 255);
        }
        else
        {
            GetComponent<Outline>().OutlineColor = new Color32(255, 108, 0, 148);
        }
    }
}
