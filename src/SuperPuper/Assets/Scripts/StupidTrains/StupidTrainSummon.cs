using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StupidTrainSummon : MonoBehaviour
{
    [SerializeField] List<GameObject> _spawnerList;
    [SerializeField] List<GameObject> _trainsList;
    void Start()
    {
        StartCoroutine(StupidTrainTimer(Random.Range(5,10)));
    }
    IEnumerator StupidTrainTimer(int time)
    {
        while(true)
        {
            if (Random.Range(0, 4) != 1)
            {
                GameObject go = Instantiate(_trainsList[Random.Range(0, 2)]);
                go.transform.position = _spawnerList[Random.Range(0, 2)].transform.position;
            }
            else
            {
                GameObject go = Instantiate(_trainsList[2]);
                go.transform.position = _spawnerList[2].transform.position;
            }
            yield return new WaitForSeconds(time);
        }
    }
}
