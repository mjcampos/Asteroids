using System;
using UnityEngine;

namespace UI {
    public class LivesManager : MonoBehaviour {
        public static LivesManager Instance { get; private set; }

        [Header("Lives")]
        [SerializeField, Range(1, 3)] int startingLives = 3;
        [SerializeField] int currentLives;
        [SerializeField] GameObject[] lifeImages;
        
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
            currentLives = startingLives;
            
            SetLiveImages();
        }
        
        public void DecrementLives() {
            currentLives--;
            
            RemoveLifeImage();
        }

        void SetLiveImages() {
            for (int i = 0; i < lifeImages.Length; i++) {
                GameObject life = lifeImages[i];
                
                life.SetActive(i < currentLives);
            }
        }

        void RemoveLifeImage() {
            for (int i = 0; i < lifeImages.Length; i++) {
                GameObject life = lifeImages[i];

                if (life.activeSelf) {
                    life.SetActive(false);
                    return;
                }
            }
        }
        
        public int GetCurrentLives() {
            return currentLives;
        }
    }
}
