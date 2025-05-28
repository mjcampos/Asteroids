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

        void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start() {
            Launch();
        }

        void Launch()
        {
            // Pick a random direction
            Vector2 direction = new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));
            direction.Normalize();
            
            // Move in that direction
            _rigidbody.linearVelocity = direction * speed;
            
            // Random rotation
            _rotationDirection = Random.value < 0.5f ? -1f : 1f;
            
            // Random rotation speed
            _rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
        }

        void Update()
        {
            pivot.transform.Rotate(Vector3.forward * _rotationDirection * _rotationSpeed * Time.deltaTime);;
        }
    }
}

