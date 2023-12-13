using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLister : MonoBehaviour
{
    public Button sceneButton;
    public Transform scrollView;
    int sceneIndex;

    private void Start()
    {
        sceneIndex = PlayerPrefs.GetInt("sceneData");
        ListLevels();
        UnlockLevels();
    }

    private void ListLevels()
    {
        int indexCount = SceneManager.sceneCountInBuildSettings;

        for (int i = 2; i < indexCount; i++)
        {
            sceneButton.GetComponentInChildren<Text>().text = (i - 1).ToString();
            sceneButton.GetComponent<OpenScene>().index = i;
            sceneButton.interactable = false;
            Instantiate(sceneButton,scrollView);
        }
    }

    private void UnlockLevels()
    {
        for (int i = 0; i < sceneIndex; i++)
        {
            scrollView.GetChild(i).GetComponent<Button>().interactable = true;
        }
    }
}