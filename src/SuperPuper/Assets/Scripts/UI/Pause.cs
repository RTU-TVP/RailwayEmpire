using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    
    public void PauseGame()
    {
        Time.timeScale = 0;
        
        _pauseMenu.SetActive(true);
    }
    
    public void ContinueGame()
    {
        Time.timeScale = 1;
        
        _pauseMenu.SetActive(false);
    }
    
    public void ExitGame()
    {
        Time.timeScale = 1;
        
        SceneManager.LoadScene("_MainMenu");
    }
}
