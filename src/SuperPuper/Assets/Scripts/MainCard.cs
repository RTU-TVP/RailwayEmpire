﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCard : MonoBehaviour {

    [SerializeField] private SceneController controller;
    [SerializeField] private GameObject Card_Back;

    public void Reveal()
    {
        if(Card_Back.activeSelf && controller.canReveal)
        {
            Card_Back.SetActive(false);
            controller.CardRevealed(this);
        }
    }

    public int Id { get; private set; }
    

    public void ChangeSprite(int id, GameObject gameObject)
    {
        Id = id;
        Instantiate(gameObject, this.transform); //This gets the sprite renderer component and changes the property of it's sprite!
    }

    public void Unreveal()
    {
        Card_Back.SetActive(true);
    }
}
