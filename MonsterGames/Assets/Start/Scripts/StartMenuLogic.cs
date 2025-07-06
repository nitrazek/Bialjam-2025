using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuLogic : MonoBehaviour
{
    public void OnStartGame()
    {
        SceneManager.LoadScene("Chapter1");
    }

    public void OnExitGame()
    {
        Application.Quit();
    }
}
