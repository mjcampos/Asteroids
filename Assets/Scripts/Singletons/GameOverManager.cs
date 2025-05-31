using System;
using TMPro;
using UnityEngine;

public class GameOverManager : MonoBehaviour {
    public static GameOverManager Instance { get; private set; }
    
    [SerializeField] GameObject gameOverText;

    void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
            
        Instance = this;
    }

    void Start()
    {
        gameOverText.SetActive(false);
    }
    
    public void DisplayGameOverScreen()
    {
        gameOverText.SetActive(true);
    }
}
