using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 45;
    }

    // Uygulamadan çýk.
    public void ExitGame()
    {
        FindObjectOfType<AudioManager>().Play("button");
        Application.Quit();
    }

    public void StartGame()
    {
        FindObjectOfType<AudioManager>().Play("button");
        SceneManager.LoadScene(1);
    }
}