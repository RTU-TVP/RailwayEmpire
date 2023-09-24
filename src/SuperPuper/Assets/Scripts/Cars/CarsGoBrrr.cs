using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarsGoBrrr : MonoBehaviour
{
    [SerializeField] bool direction;
    [SerializeField] GameObject _point1;
    [SerializeField] GameObject _point2;
    [SerializeField] List<GameObject> _carTypes;
    void Start()
    {
        StartCoroutine(carsTimer());
    }

    void Update()
    {
        
    }
    IEnumerator carsTimer()
    {
        while(true)
        {
            SummonCar();
            yield return new WaitForSeconds(Random.Range(1,6));
        }
    }
    void SummonCar()
    {
        if(_carTypes.Count != 0 && _carTypes[Random.Range(0, _carTypes.Count)] != null)
        {
            GameObject newCar = Instantiate(_carTypes[Random.Range(0, _carTypes.Count)], GameObject.Find("Map").transform.parent);
            newCar.transform.position = _point1.transform.position;
            Vector3 scale = newCar.transform.localScale;
            newCar.GetComponent<CarRide>().direction = direction;
            if(direction)
            {
                scale.y = -newCar.transform.localScale.y;
                newCar.transform.localScale = scale;
            }
        }
    }
}
