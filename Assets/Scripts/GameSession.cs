using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    int score = 0;
        
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highScoreText;


    void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if(numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        livesText.text = playerLives.ToString("Lives:\n00");
        scoreText.text = score.ToString("Score:\n000000");
        highScoreText.text = GetHighScore().ToString("High Score:\n000000");
    }

    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeLife();
            livesText.text = playerLives.ToString("Lives:\n00");
        }
        else
        {
            ResetGameSession(); 
        }
    }

    public void ProcessPickup(int pointsToAdd)
    {
        score += pointsToAdd;
        Mathf.Clamp(score, 0, int.MaxValue);
        scoreText.text = score.ToString("Score:\n000000");
        
        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
            highScoreText.text = GetHighScore().ToString("High Score:\n000000");
        }
    }

    public void ProcessPickupLife(int livesToAdd)
    {
        playerLives += livesToAdd;
        Mathf.Clamp(playerLives, 0, int.MaxValue);
        livesText.text = playerLives.ToString("Lives:\n00");
    }

    public int GetHighScore()
    {
        return PlayerPrefs.GetInt("HighScore");
    }

    void TakeLife()
    {
        playerLives--;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        livesText.text = playerLives.ToString();
    }

    void ResetGameSession()
    {
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(0);
        //Loop naar Game over screen
        Destroy(gameObject);
    }
}
