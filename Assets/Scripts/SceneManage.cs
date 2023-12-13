using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    public GameData gameData;

    string jsonData;
    string jsonLocation;

    private void Awake()
    {
        Application.targetFrameRate = 35;

        jsonLocation = Application.persistentDataPath + "/gameData.json";
        
        GetSaveGameData(jsonLocation, jsonData);
        WriteSceneData();
    }

    void GetSaveGameData(string location, string data)
    {
        if (!File.Exists(location))
        {
            File.Create(location).Dispose();
            gameData.hint = 10;
            gameData.sceneIndex = 1;
            data = JsonUtility.ToJson(gameData, true);
            File.WriteAllText(location, data);
            LoadData();
        }
        else
        {
            LoadData();
        }
    }

    public void LoadData()
    {
        jsonData = File.ReadAllText(jsonLocation);
        gameData = JsonUtility.FromJson<GameData>(jsonData);
    }

    public void WriteSceneData()
    {
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (activeSceneIndex != 0)
        {
            gameData.sceneIndex = activeSceneIndex;
            jsonData = JsonUtility.ToJson(gameData, true);
            File.WriteAllText(jsonLocation, jsonData);
        }
    }

    // DEMO AREA.

    public void DemoReset()
    {
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (activeSceneIndex != 0)
        {
            gameData.sceneIndex = 1;
            jsonData = JsonUtility.ToJson(gameData, true);
            File.WriteAllText(jsonLocation, jsonData);
        }
    }
}