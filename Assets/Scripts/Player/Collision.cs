using System;
using System.Collections;
using UI;
using UnityEngine;

namespace Player {
    public class Collision : MonoBehaviour
    {
        [SerializeField] float invincibilityDuration = 3f;

        [SerializeField] float flickerInterval = 0.2f;

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
            }
        }

        void HandlePlayerHit()
        {
            StartCoroutine(RespawnRoutine());
        }

        IEnumerator RespawnRoutine()
        {
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
