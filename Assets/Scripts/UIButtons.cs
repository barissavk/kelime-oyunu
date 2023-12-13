using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;
using System.Linq;

public class UIButtons : MonoBehaviour
{
    public Button buttonHint;
    public HintManager HintManager;
    public LevelGenerator level;
    public Text placeHolder;

    private void Start()
    {
        for (int i = 0; i < level.addedWords.Length; i++)
        {
            for (int a = 0; a < level.addedWords[i].transform.childCount; a++)
            {
                if (level.addedWords[i].transform.GetChild(a).GetComponent<HintBoolean>().isCommon == false)
                {
                    level.LetterData.Add(level.addedWords[i].transform.GetChild(a).GetChild(0).gameObject);
                }
            }
        }

        if (HintManager.gameData.hint <= 0)
        {
            buttonHint.interactable = false;
        }
        else
        {
            buttonHint.interactable = true;
        }
    }

    public void CheckWord()
    {
        for (int i = 0; i < level.addedWords.Length; i++)
        {
            if (placeHolder.text == level.addedWords[i].GetComponent<WordContainer>().word)
            {
                Debug.Log("Bulundu");
                for (int a = 0; a < level.addedWords[i].transform.childCount; a++)
                {
                    level.addedWords[i].transform.GetChild(a).GetChild(0).gameObject.SetActive(true);
                    level.addedWords[i].transform.GetChild(a).GetChild(0).gameObject.GetComponent<Text>().CrossFadeAlpha(1,0.01f,false);
                    level.LetterData.Remove(level.addedWords[i].transform.GetChild(a).GetChild(0).gameObject);
                }
                --level.totalWordNumber;
                level.addedWords[i].transform.SetAsLastSibling();
            }
        }
        placeHolder.text = string.Empty;
        
        if (level.totalWordNumber == 0)
        {
            Debug.Log("Kazand�n.");
            StartCoroutine(WaitForScene());
        }

        IEnumerator WaitForScene()
        {
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene(sceneIndex + 1);
        }
    }

    // Go to Main Menu
    public void GoMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Delete a char.
    public void DeleteChar()
    {
        if (placeHolder.text != string.Empty)
        {
            placeHolder.text = placeHolder.text.Substring(0, placeHolder.text.Length - 1);
        }
    }

    public void HintButton()
    {
        if (HintManager.gameData.hint > 0)
        {
            int pickChar = Random.Range(0, level.LetterData.Count);

            level.LetterData[pickChar].GetComponent<Text>().CrossFadeAlpha(0.5f,2.0f,false);
            level.LetterData[pickChar].gameObject.SetActive(true);
            level.LetterData[pickChar].transform.SetAsLastSibling();
            level.LetterData.RemoveAt(pickChar);
            --HintManager.gameData.hint;
            HintManager.WriteData();

            if (HintManager.gameData.hint <= 0)
            {
                buttonHint.interactable = false;
            }
        }
        else
        {
            Debug.Log("Yetersiz �pucu");
        }
    }
}