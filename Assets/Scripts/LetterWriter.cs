using UnityEngine;
using UnityEngine.UI;

public class LetterWriter : MonoBehaviour
{
    public LevelGenerator level;
    public string letter;

    private void Start()
    {
        level = GameObject.FindGameObjectWithTag("Manager").GetComponent<LevelGenerator>();
        letter = GetComponentInChildren<Text>().text;
    }

    public void WriteLetter()
    {
        level.placeholder.text += letter;
    }
}