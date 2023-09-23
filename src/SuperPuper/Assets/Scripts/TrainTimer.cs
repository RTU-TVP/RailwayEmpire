using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TrainTimer : MonoBehaviour
{
    [SerializeField] GameObject line;
    private void Start()
    {
        line.GetComponent<UnityEngine.UI.Image>().color = new Color32(0, 255, 0, 255);
        StartTimer(40);
    }
    private void Update()
    {
        line.GetComponent<UnityEngine.UI.Image>().color = new Color(1 - GetComponent<UnityEngine.UI.Slider>().value, GetComponent<UnityEngine.UI.Slider>().value, 0, 1);
    }
    void StartTimer(int time)
    {
        StartCoroutine(Timer(time));
    }
    IEnumerator Timer(int time)
    {
        int startTime = time;
        while(time > 0)
        {
            time--;
            GetComponent<UnityEngine.UI.Slider>().value = (float)time/startTime;
            yield return new WaitForSeconds(1);
        }
        TimeOver();
        yield break;
    }
    void TimeOver()
    {
        Debug.Log("TimeOver");
    }
}
