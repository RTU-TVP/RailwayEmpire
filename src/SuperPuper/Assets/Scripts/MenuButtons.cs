using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] GameObject _progressBar;
    public void NewGame()
    {
        _progressBar.SetActive(true);
        StartCoroutine(GameSceneStarter("GameTestScene"));
        gameObject.SetActive(false);
    }
    IEnumerator GameSceneStarter(string scene)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
        while (!operation.isDone)
        {
            _progressBar.GetComponent<UnityEngine.UI.Slider>().value = operation.progress;
            yield return null;
        }
        _progressBar.SetActive(false);
        _progressBar.GetComponent<UnityEngine.UI.Slider>().value = 0;
        yield break;
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void ContinueGame()
    {
        Debug.Log("Continue");
    }
}
