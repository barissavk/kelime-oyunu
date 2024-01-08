using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;
using System.Text;
using UnityEngine.UI;
using System;
using System.Net.Http.Headers;

public class PlayfabManager : MonoBehaviour
{
    public static PlayfabManager instance;
    public GameObject rowPrefab;
    public Transform rowsParent;

    public Canvas mainMenu;
    public Canvas getUsername;

    public InputField inputUserName;
    void Start()
    {
        // Objeyi farklı sahnelerde de kullanma.
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        SceneManager.activeSceneChanged += SceneChanged;

        // Playfab' e login işlemi.
        Login();
    }

    private void SceneChanged(Scene current, Scene next)
    {
        rowsParent = GameObject.FindGameObjectWithTag("RowParent").transform;
        if (PlayFabClientAPI.IsClientLoggedIn())
        {
            GetLeaderboard();
        }
    }

    void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithCustomID(request,OnSuccess, OnError);
    }

    void OnSuccess(LoginResult result)
    {
        Debug.Log("Successful Login/Account created.");
        string name = null;
        if(result.InfoResultPayload.PlayerProfile != null)
            name = result.InfoResultPayload.PlayerProfile.DisplayName;

        if (name == null)
        {
            getUsername.gameObject.SetActive(true);
            mainMenu.gameObject.SetActive(false);
        }
        else
        {
            mainMenu.gameObject.SetActive(true);
            getUsername.gameObject.SetActive(false);
        }  
    }

    public void SubmitNameButton()
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = inputUserName.text
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request,OnDisplayNameUpdate,OnError);
    }

    void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log("Updated username.");
        mainMenu.gameObject.SetActive(true);
        getUsername.gameObject.SetActive(false);
    }

    void OnError(PlayFabError error)
    {
        Debug.Log("Error while logging in/creating account!");
        Debug.Log(error.GenerateErrorReport());
    }

    public void SendLeaderboard(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate> {
                new StatisticUpdate
                {
                    StatisticName = "PlayerScore",
                    Value = score
                }
            }
        };

        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }

    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Leaderboard sent successfully.");
    }

    public void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "PlayerScore",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
    }

    void OnLeaderboardGet(GetLeaderboardResult result)
    {
        foreach (Transform item in rowsParent)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in result.Leaderboard)
        {
            GameObject newGo = Instantiate(rowPrefab,rowsParent);
            Text[] texts = newGo.GetComponentsInChildren<Text>();
            texts[0].text = (item.Position + 1).ToString();
            texts[1].text = item.DisplayName;
            texts[2].text = item.StatValue.ToString();
            Debug.Log("PLACE: " + item.Position + " ID: " + item.PlayFabId + " SCORE: " + item.StatValue);
        }
    }
}