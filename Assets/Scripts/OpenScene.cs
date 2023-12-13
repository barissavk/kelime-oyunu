using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenScene : MonoBehaviour
{
    public int index;

    public void SceneOpener()
    {
        SceneManager.LoadScene(index);
    }
}