#region

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

#endregion

public class MenuButtons : MonoBehaviour
{
    [SerializeField]
    private GameObject _progressBar;
    public void StartLevel()
    {

        _progressBar.SetActive(true);
        StartCoroutine(GameSceneStarter("GameTestScene"));
        gameObject.SetActive(false);
    }
    private IEnumerator GameSceneStarter(string scene)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
        while (!operation.isDone)
        {
            _progressBar.GetComponent<Slider>().value = operation.progress;
            yield return null;
        }
        _progressBar.SetActive(false);
        _progressBar.GetComponent<Slider>().value = 0;
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}