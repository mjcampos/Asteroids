using System;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour {
    public static ScoreManager Instance { get; private set; }
    
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highScoreText;
    
    int _score = 0;
    int _highScore;
    
    void Awake() {
        // Singleton pattern
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
    }

    void Start() {
        // Extract score from the player prefs, else set to 0
        _highScore = PlayerPrefs.GetInt("HighScore", 0);
        
        // Set score and high score texts
        scoreText.text = _score.ToString("D5");
        highScoreText.text = "High Score: " + _highScore.ToString("D5");
    }

    public void UpdateScore(int points) {
        // Update score
        _score += points;
        scoreText.text = _score.ToString("D5");
        
        // Update high score
        if (_score > _highScore) {
            _highScore = _score;
            highScoreText.text = "High Score: " + _highScore.ToString("D5");
            PlayerPrefs.SetInt("HighScore", _highScore);
        }
    }
}
