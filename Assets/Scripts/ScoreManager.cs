using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    private int score = 0;
    public int score_max = 0;
    float pastTime = 0;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("playerScore"))
        {
            PlayerPrefs.SetInt("playerScore",0);
            PlayerPrefs.SetInt("maxScore", 0);
            score = PlayerPrefs.GetInt("playerScore");
            DisplayScore();
        }
        else
        {
            score = PlayerPrefs.GetInt("playerScore");
            score_max = PlayerPrefs.GetInt("maxScore");
            DisplayScore();
        }
    }

    public void CalculateScore(int length, float time)
    {
        float exactTime = time - pastTime;
        pastTime = exactTime;

        score += (int)((100 * length) / (exactTime * 0.1f));
        DisplayScore();
        PlayerPrefs.SetInt("playerScore", score);

        if (score >= score_max)
        {
            score_max = score;
            PlayerPrefs.SetInt("maxScore", score_max);
        }
    }

    private void DisplayScore()
    {
        scoreText.text = "SCORE\n" + score;
    }
}