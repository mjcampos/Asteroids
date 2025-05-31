using System;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour {
    public static ScoreManager Instance { get; private set; }
    
    [SerializeField] TextMeshProUGUI scoreText;
    
    int _score = 0;
    
    void Awake() {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
    }

    void Start() {
        UpdateScore(_score);
    }

    public void UpdateScore(int points) {
        _score += points;
        scoreText.text = _score.ToString("D5");
    }
}
