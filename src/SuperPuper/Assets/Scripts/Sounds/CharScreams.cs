using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharScreams : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(scream(7));
    }
    IEnumerator scream(int time)
    {
        GetComponent<AudioManager>().Play($"ch{Random.Range(1, 11)}");
        yield return new WaitForSeconds(time);
    }
}
