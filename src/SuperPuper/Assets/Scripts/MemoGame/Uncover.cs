using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uncover : MonoBehaviour
{
    [SerializeField] private MainCard _mainCard;
    private void OnMouseDown()
    {
        _mainCard.Reveal();
    }
}
