using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 45;
    }

    // Uygulamadan ??k.
    public void ExitGame()
    {
        FindObjectOfType<AudioManager>().Play("button");
        Application.Quit();
    }

    public void StartGame()
    {
        FindObjectOfType<AudioManager>().Play("button");
        if (!PlayerPrefs.HasKey("sceneData"))
        {
            PlayerPrefs.SetInt("sceneData", 2);
        }
        SceneManager.LoadScene(2);
    }

    public void DeleteData()
    {
        PlayerPrefs.DeleteAll();
    }
}