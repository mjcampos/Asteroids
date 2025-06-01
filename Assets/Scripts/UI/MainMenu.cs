using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    [SerializeField] TextMeshProUGUI highScoreText;

    void Start()
    {
        // Extract score from the player prefs, else set to 0
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        
        // Set high score
        highScoreText.text = "High Score: " + highScore.ToString("D5");
    }

    public void LoadGame() {
        SceneManager.LoadScene("Game");
    }
}
