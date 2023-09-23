using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarsGoBrrr : MonoBehaviour
{
    [SerializeField] GameObject _point1;
    [SerializeField] GameObject _point2;
    [SerializeField] List<GameObject> _carTypes;
    void Start()
    {
        StartCoroutine(carsTimer(Random.Range(1,5)));
    }

    void Update()
    {
        
    }
    IEnumerator carsTimer(int time)
    {
        while(true)
        {
            SummonCar();
            yield return new WaitForSeconds(time);
        }
    }
    void SummonCar()
    {
        GameObject newCar = Instantiate(_carTypes[Random.Range(0, _carTypes.Count)], gameObject.transform);
        newCar.transform.position = _point1.transform.position;
    }
}
