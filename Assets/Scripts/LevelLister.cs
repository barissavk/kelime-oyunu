using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLister : MonoBehaviour
{
    public Button sceneButton;
    public Transform scrollView;

    private void Start()
    {
        ListLevels();
    }

    private void ListLevels()
    {
        int indexCount = SceneManager.sceneCountInBuildSettings;

        for (int i = 2; i < indexCount; i++)
        {
            sceneButton.GetComponentInChildren<Text>().text = (i - 1).ToString();
            sceneButton.GetComponent<OpenScene>().index = i;
            Instantiate(sceneButton,scrollView);
        }
    }
}