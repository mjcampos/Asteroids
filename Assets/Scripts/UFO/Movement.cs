using System;
using UnityEngine;

namespace UFO {
    public class Movement : MonoBehaviour {
        [SerializeField] float speed = 2f;
        
        Rigidbody2D _rigidbody;

        void Awake() {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void Launch(Vector2 moveDirection) {
            _rigidbody.linearVelocity = moveDirection * speed;
        }
    }
}
