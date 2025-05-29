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
        
        void Start() { 
            _animator = GetComponent<Animator>();
            
            //_animator.SetFloat("SpeedMultiplier", shootSpeed);
        }

        void OnFire(InputValue value) {
            _fireButtonPressed = value.isPressed;
        }

        void Update() {
            ShootSpeedChangeListener();
            
            if (_fireButtonPressed && !_isShooting) {
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

        void ShootSpeedChangeListener()
        {
            if (!Mathf.Approximately(_lastShootSpeed, shootSpeed))
            {
                _lastShootSpeed = shootSpeed;
                
                _animator.SetFloat("SpeedMultiplier", shootSpeed);
            }
        }
    }
}

