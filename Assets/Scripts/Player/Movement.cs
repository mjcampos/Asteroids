using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class Movement : MonoBehaviour
    {
        [Header("Player Speed")]
        [SerializeField] float rotationSpeed = 7f;
        [SerializeField] float moveSpeed = 15f;
        
        Rigidbody2D _rigidbody;
        float _rotationDirection;
        float _direction;
        
        Camera _mainCamera;
        
        float _halfScreenWidth;
        float _halfScreenHeight;
        
        float _spriteHalfWidth;
        float _spriteHalfHeight;
        
        void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            
            _mainCamera = Camera.main;
            _halfScreenHeight = _mainCamera.orthographicSize;
            _halfScreenWidth = _halfScreenHeight * _mainCamera.aspect;
            
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

            if (spriteRenderer != null)
            {
                _spriteHalfWidth = spriteRenderer.bounds.extents.x;
                _spriteHalfHeight = spriteRenderer.bounds.extents.y;
            }
            else
            {
                _spriteHalfWidth = _spriteHalfHeight = 0.5f;
            }
        }

        void OnMove(InputValue value)
        {
            Vector2 movement = value.Get<Vector2>();
            
            _direction = movement.y;
        }

        void OnRotate(InputValue value)
        {
            Vector2 rotation = value.Get<Vector2>();
            
            _rotationDirection = -rotation.x;
        }

        void FixedUpdate()
        {
            _rigidbody.SetRotation(_rigidbody.rotation + (_rotationDirection * rotationSpeed));
            _rigidbody.linearVelocity = transform.up * _direction * moveSpeed;
        }

        void LateUpdate()
        {
            Vector3 newPos = transform.position;

            if (newPos.x > _halfScreenWidth + _spriteHalfWidth)
            {
                newPos.x = -_halfScreenWidth - _spriteHalfWidth;
            } else if (newPos.x < -_halfScreenWidth - _spriteHalfWidth)
            {
                newPos.x = _halfScreenWidth + _spriteHalfWidth;
            }

            if (newPos.y > _halfScreenHeight + _spriteHalfHeight)
            {
                newPos.y = -_halfScreenHeight - _spriteHalfHeight;
            } else if (newPos.y < -_halfScreenHeight - _spriteHalfHeight)
            {
                newPos.y = _halfScreenHeight + _spriteHalfHeight;
            }
            
            transform.position = newPos;
        }
    }
}

