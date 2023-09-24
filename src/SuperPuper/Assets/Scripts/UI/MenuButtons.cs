#region

using Data.Constant;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

#endregion

namespace UI
{
    public class MenuButtons : MonoBehaviour
    {
        [SerializeField] GameObject _progressBar;
        public void NewGame()
        {
            DeleteProgress();
            ContinueGame();
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
        public void ContinueGame()
        {
            _progressBar.SetActive(true);
            StartCoroutine(GameSceneStarter("_MainScene"));
            gameObject.SetActive(false);
            
        }
        public void DeleteProgress()
        {
            PlayerPrefs.DeleteKey(WorkersConstantData.WORKERS_LVL_MOVE_SPEED);
            PlayerPrefs.DeleteKey(WorkersConstantData.WORKERS_LVL_WORK_TIME);
            PlayerPrefs.DeleteKey(WorkersConstantData.WORKERS_LVL_SALE_TIME);
            PlayerPrefs.DeleteKey(WorkersConstantData.MONEY);
        }
        private void Start()
        {
            
        }
    }
}
