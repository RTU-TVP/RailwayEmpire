using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MemoGameManager : MonoBehaviour
{
    public const int COLS = 4;
    public const int ROWS = 3;

    [SerializeField] private float _spacingX = 0;
    [SerializeField] private float _spacingY = 0;

    [SerializeField] private UcoveredObj _ucoveredObj;
    [SerializeField] private GameObject[] _objects;

    private int[] Randomiser(int[] locations)
    {
        int[] array = locations.Clone() as int[];
        for (int i = 0; i < array.Length; i++)
        {
            int newArray = array[i];
            int j = Random.Range(i, array.Length);
            array[i] = array[j];
            array[j] = newArray;
        }

        return array;
    }

    private void Start()
    {
        int[] locations = { 0, 0, 1, 1, 2, 2, 3, 3, 4,4,5,5,6,6 };
        locations = Randomiser(locations);
        Vector3 _startPosition = _ucoveredObj.transform.position;

        for (int i = 0; i < COLS; i++)
        {
            for (int j = 0; j < ROWS; j++)
            {
                UcoveredObj gameObj = _ucoveredObj;
                gameObj = _objects[locations[j + COLS + i]];
                gameObj.transform.position = new Vector3((_startPosition.x * (_spacingX*i)), 5,
                    _startPosition.y * (_spacingY*j));
            }
            
        }
    }
}