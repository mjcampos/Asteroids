using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Rok
{
    public class Movement : MonoBehaviour {
        [Header("Velocity")]
        [SerializeField] float speed = 10f;
        
        [Header("Rotation")]
        [SerializeField] GameObject pivot;

        [SerializeField] float minRotationSpeed = 20f;
        [SerializeField] float maxRotationSpeed = 30f;

        float _rotationDirection;
        float _rotationSpeed;
        Rigidbody2D _rigidbody;

        void Awake() {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void Launch(Vector2 moveDirection) {
            // Move in that direction
            _rigidbody.linearVelocity = moveDirection * speed;
            
            // Random rotation
            _rotationDirection = Random.value < 0.5f ? -1f : 1f;
            
            // Random rotation speed
            _rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
        }

        void Update() {
            pivot.transform.Rotate(Vector3.forward * _rotationDirection * _rotationSpeed * Time.deltaTime);;
        }
    }
}

