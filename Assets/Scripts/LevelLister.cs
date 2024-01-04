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

        for (int i = 3; i < indexCount; i++)
        {
            sceneButton.GetComponentInChildren<Text>().text = (i - 2).ToString();
            sceneButton.GetComponent<OpenScene>().index = i;
            sceneButton.interactable = false;
            Instantiate(sceneButton,scrollView);
        }
    }

    private void UnlockLevels()
    {
        for (int i = 0; i < sceneIndex - 1; i++)
        {
            scrollView.GetChild(i).GetComponent<Button>().interactable = true;
            Debug.Log("scene index: " + i);
        }
    }
}