using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenScene : MonoBehaviour
{
    public int index;

    public void SceneOpener(int sceneIndex)
    {
        int activeScene = SceneManager.GetActiveScene().buildIndex;
        if (activeScene == 2)
        {
            SceneManager.LoadScene(index);
        }
        else
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }
}