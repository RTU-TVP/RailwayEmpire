using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScene : MonoBehaviour
{
    [SerializeField] private KeyCode _restartButton = KeyCode.R;

    private void Update()
    {
        if (Input.GetKeyDown(_restartButton))
            Restart();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
