using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharScreams : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(scream());
    }
    IEnumerator scream()
    {
        while(true)
        {
            GetComponent<AudioManager>().Play($"ch{Random.Range(1, 11)}");
            yield return new WaitForSeconds(Random.Range(5,10));
        }
    }
}
