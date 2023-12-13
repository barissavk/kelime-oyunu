using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;

public enum Rotation { Top, Left };

public class LevelGenerator : MonoBehaviour
{
    public static bool isShow = false;

    [Header("Prefabs")]
    public GameObject WordTop;
    public GameObject WordLeft;
    public GameObject KeyBoardArea;
    public GameObject WordArea;
    public GameObject selectedChar;

    [Header("UI Elements")]
    public GameObject buttonLetter;
    public Text placeholder;

    [Header("Words")]
    public List<string> Letters;
    public GameObject[] LetterObjects;
    public List<string> Words;
    public List<GameObject> LetterData;
    public GameObject[] addedWords;

    public int totalWordNumber = 0;
    [Space]
    public Rotation rotation;

    public void CreateGrid()
    {
        int lenght = Words[0].Count();
        string selected = Words[0];

        if (rotation == Rotation.Top)
        {
            WordTop.GetComponent<WordContainer>().word = selected;
            var top = Instantiate(WordTop, WordArea.transform);
            for (int i = 0; i < lenght; i++)
            {
                selectedChar.GetComponentInChildren<Text>().text = selected[i].ToString();
                selectedChar.transform.GetChild(0).gameObject.SetActive(false);
                Instantiate(selectedChar,top.transform);
                selectedChar.transform.GetChild(0).gameObject.SetActive(true);
            }
            totalWordNumber++;
            Words.RemoveAt(0);
        }

        if (rotation == Rotation.Left)
        {
            WordLeft.GetComponent<WordContainer>().word = selected;
            var left = Instantiate(WordLeft, WordArea.transform);
            for (int i = 0; i < lenght; i++)
            {
                selectedChar.GetComponentInChildren<Text>().text = selected[i].ToString();
                selectedChar.transform.GetChild(0).gameObject.SetActive(false);
                Instantiate(selectedChar, left.transform);
                selectedChar.transform.GetChild(0).gameObject.SetActive(true);
            }
            totalWordNumber++;
            Words.RemoveAt(0);
        }
    }

    public void CreateKeyboard()
    {
        for (int i = 0; i < Words.Count; i++)
        {
            for (int a = 0; a < Words[i].Length; a++)
            {
                Letters.Add(Words[i][a].ToString());
            }
        }
        Letters = Letters.Distinct().ToList();


        while (Letters.Count != 0)
        {
            int j = Random.Range(0, Letters.Count);

            Debug.Log(Letters[j]);
            buttonLetter.GetComponentInChildren<Text>().text = Letters[j];
            Instantiate(buttonLetter, KeyBoardArea.transform);
            LetterObjects = GameObject.FindGameObjectsWithTag("CharRadial");
            Letters.RemoveAt(j);
        }
    }

    public void GetWords()
    {
        addedWords = GameObject.FindGameObjectsWithTag("Word");
    }

    public void HideShow()
    {
        if (!isShow)
        {
            for (int i = 0; i < addedWords.Length; i++)
            {
                for (int a = 0; a < addedWords[i].transform.childCount; a++)
                {
                    addedWords[i].transform.GetChild(a).GetChild(0).gameObject.SetActive(true);
                }
            }
            isShow = true;
        }
        else
        {
            for (int i = 0; i < addedWords.Length; i++)
            {
                for (int a = 0; a < addedWords[i].transform.childCount; a++)
                {
                    addedWords[i].transform.GetChild(a).GetChild(0).gameObject.SetActive(false);
                }
            }
            isShow = false;
        }
    }
}