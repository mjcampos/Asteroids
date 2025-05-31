using System.Collections;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }
    
    [SerializeField] InputActionAsset inputActionAsset;
    
    InputActionMap _uiInputActionMap;
    InputAction _restartInputAction;
    InputAction _backInputAction;

    bool _isGameOver;
    
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
        _uiInputActionMap = inputActionAsset.FindActionMap("UI");

        _restartInputAction = _uiInputActionMap.FindAction("Restart");
        _backInputAction = _uiInputActionMap.FindAction("Back");
        
        LoadGame();
    }

    void Update() {
        RestartListener();
        BackListener();
    }

    void RestartListener() {
        if (_restartInputAction.triggered && _isGameOver) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void BackListener() {
        if (_backInputAction.triggered) {
            SceneManager.LoadScene("Main Menu");
        }
    }

    void LoadGame() {
        /*
         * When scene loads:
         * 1. Freeze the scene
         * 2. Initiate countdown sequence
         * 3. Upon countdown completion, start the game
         */
        
        // Step 1
        _isGameOver = false;
        Time.timeScale = 0f;
        
        // Step 2
        CountdownManager.Instance.StartCountdown();
        
        /*
         * Step 3
         * CountdownEnded gets called at the end of the countdown sequence
         */
    }

    public void CountdownEnded() {
        Time.timeScale = 1f;
    }

    public void GameOver() {
        StartCoroutine(PauseThenContinue());
    }

    IEnumerator PauseThenContinue() {
        /*
         * When game ends:
         * 1. Freeze the scene
         * 2. Display game over screen
         * 3. Give the player the chance to play again
         */
        
        yield return new WaitForSecondsRealtime(0.5f);
        
        // Step 1
        Time.timeScale = 0f;
        
        // Step 2
        GameOverManager.Instance.DisplayGameOverScreen();
        
        // Step 3
        _isGameOver = true;
    }
}
