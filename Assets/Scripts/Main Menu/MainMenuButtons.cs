using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
    public Canvas storeCanvas;
    public SceneManage SceneManage;
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

    public void OpenStore()
    {
        FindObjectOfType<AudioManager>().Play("button");
        storeCanvas.gameObject.SetActive(true);
    }

    public void CloseStore()
    {
        FindObjectOfType<AudioManager>().Play("button");
        storeCanvas.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        FindObjectOfType<AudioManager>().Play("button");
        SceneManager.LoadScene(SceneManage.gameData.sceneIndex.ToString());
    }
}