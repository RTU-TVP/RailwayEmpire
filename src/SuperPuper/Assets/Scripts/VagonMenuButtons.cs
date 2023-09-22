using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VagonMenuButtons : MonoBehaviour
{
    public void ButtonDoMyself()
    {
        Debug.Log("Я сам разгружу вагон!");
    }
    public void ButtonCallWorkers()
    {
        Debug.Log("Я позову рабочих!");
    }
    public void UnloadVagon()
    {
        Debug.Log("Разгрузка Вагона!");
    }
}
