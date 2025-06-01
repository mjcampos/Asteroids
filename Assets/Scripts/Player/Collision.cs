using System;
using System.Collections;
using UI;
using UnityEngine;

namespace Player {
    public class Collision : MonoBehaviour
    {
        [Header("Taking Damage")]
        [SerializeField] float invincibilityDuration = 3f;
        [SerializeField] float flickerInterval = 0.2f;
        
        [Header("Explosion Particle")]
        [SerializeField] GameObject particlePrefab;
        [SerializeField] int explosionScale = 3;
        [SerializeField] AudioClip explosionClip;

        Shooting _shooting;
        Collider2D _collider;
        SpriteRenderer _spriteRenderer;
        bool _isInvincible;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _shooting = GetComponent<Shooting>();
            _collider = GetComponent<Collider2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            /*
             * If player has been hit:
             * 1. Trigger HandlePlayerHit sequence
             * 2. Deduct a life
             */
            if (other.CompareTag("Rok") && !_isInvincible) {
                HandlePlayerHit();
                LivesManager.Instance.DecrementLives();

                // Check if the player has no lives left
                if (LivesManager.Instance.GetCurrentLives() < 1) {
                    /*
                     * If no lives left:
                     * 1. Instantiate explosion
                     * 2. Play explosion sound
                     * 3. Destroy the player
                     * 4. Trigger GameOver
                     */
                    
                    // Step 1
                    GameObject explosionInstance = Instantiate(particlePrefab, transform.position, Quaternion.identity);
                
                    explosionInstance.transform.localScale = Vector3.one * explosionScale;
                    explosionInstance.transform.SetParent(null);
                    
                    // Step 2
                    AudioSource.PlayClipAtPoint(explosionClip, transform.position);;
                    
                    // Step 3
                    Destroy(gameObject);
                    
                    // Step 4
                    GameManager.Instance.GameOver();
                }
            }
        }

        void HandlePlayerHit() {
            StartCoroutine(RespawnRoutine());
        }

        IEnumerator RespawnRoutine() {
            _isInvincible = true;
            _collider.enabled = false;
            
            float elapsedTime = 0f;
            bool visible = false;
            
            _shooting.SetIsRecoveringFromHit(true);

            while (elapsedTime < invincibilityDuration)
            {
                visible = !visible;
                _spriteRenderer.enabled = visible;
                
                yield return new WaitForSeconds(flickerInterval);
                elapsedTime += flickerInterval;
            }
            
            _spriteRenderer.enabled = true;
            _collider.enabled = true;
            _isInvincible = false;
            
            _shooting.SetIsRecoveringFromHit(false);
        }
    }
}
