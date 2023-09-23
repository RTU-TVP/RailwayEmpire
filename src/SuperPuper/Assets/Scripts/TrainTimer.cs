#region

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

#endregion

public class TrainTimer : MonoBehaviour
{
    [SerializeField]
    private GameObject line;
    private void Start()
    {
        line.GetComponent<Image>().color = new Color32(0, 255, 0, 255);
        StartTimer(40);
    }
    private void Update()
    {
        line.GetComponent<Image>().color = new Color(1 - GetComponent<Slider>().value, GetComponent<Slider>().value, 0, 1);
    }
    private void StartTimer(int time)
    {
        StartCoroutine(Timer(time));
    }
    private IEnumerator Timer(int time)
    {
        int startTime = time;
        while (time > 0)
        {
            time--;
            GetComponent<Slider>().value = (float)time / startTime;
            yield return new WaitForSeconds(1);
        }
        TimeOver();
    }
    private void TimeOver()
    {
        Debug.Log("TimeOver");
    }
}
