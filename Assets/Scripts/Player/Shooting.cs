using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player {
    public class Shooting : MonoBehaviour {
        [SerializeField] float shootSpeed = 5f;
        [SerializeField] GameObject laserPrefab;

        float _lastShootSpeed;
        
        Animator _animator;

        bool _fireButtonPressed;
        bool _isShooting;
        bool _isRecoveringFromHit = false;
        
        void Start() { 
            _animator = GetComponent<Animator>();
        }

        void OnFire(InputValue value) {
            _fireButtonPressed = value.isPressed;
        }

        void Update() {
            ShootSpeedChangeListener();
            
            if (_fireButtonPressed && !_isShooting && !_isRecoveringFromHit) {
                // Mark the player as currently shooting
                _isShooting = true;
            
                // Instantiate the laser
                GameObject laser = Instantiate(laserPrefab, transform);
                
                laser.transform.parent = null;
            
                _animator.SetTrigger("IsShooting");
            }
        }

        void NoLongerShooting() {
            _isShooting = false;
        }

        // This will allow shooting speed to be changed at runtime
        void ShootSpeedChangeListener() {
            if (!Mathf.Approximately(_lastShootSpeed, shootSpeed))
            {
                _lastShootSpeed = shootSpeed;
                
                _animator.SetFloat("SpeedMultiplier", shootSpeed);
            }
        }

        public void SetIsRecoveringFromHit(bool isRecovering)
        {
            _isRecoveringFromHit = isRecovering;
        }
    }
}

