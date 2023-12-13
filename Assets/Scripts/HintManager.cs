using System.Collections.Generic;
using System.Drawing;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class HintManager : MonoBehaviour
{
    public GameData gameData;
    public Text textHint;

    string jsonData;
    string jsonLocation;

    private void Awake()
    {
        jsonLocation = Application.persistentDataPath + "/gameData.json";

        LoadData(jsonLocation, jsonData);
    }

    private void Start()
    {
        ShowHintData();
    }

    public void LoadData(string location, string data)
    {
        data = File.ReadAllText(location);
        gameData = JsonUtility.FromJson<GameData>(data);
    }

    public void WriteData()
    {
        jsonData = JsonUtility.ToJson(gameData, true);
        File.WriteAllText(jsonLocation, jsonData);
        ShowHintData();
    }

    public void ShowHintData()
    {
        textHint.text = gameData.hint.ToString();
    }

    public void AddHint(float amount)
    {
        gameData.hint += amount;
        WriteData();
        ShowHintData();
    }
}