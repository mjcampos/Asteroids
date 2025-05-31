using System;
using TMPro;
using UI;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }
    
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
        /*
         * When scene loads:
         * 1. Freeze the scene
         * 2. Initiate countdown sequence
         * 3. Upon countdown completion, start the game
         */
        
        // Step 1
        Time.timeScale = 0f;
        
        // Step 2
        CountdownManager.Instance.StartCountdown();
    }

    public void CountdownEnded() {
        Time.timeScale = 1f;
    }
}
