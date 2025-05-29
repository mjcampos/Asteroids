using UnityEngine;

namespace  Laser
{
    public class Movement : MonoBehaviour {
        [SerializeField] float speed = 10f;
        
        Rigidbody2D _rigidbody;
        
        void Awake() {
            _rigidbody = GetComponent<Rigidbody2D>();
        }
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start() {
            _rigidbody.linearVelocity = transform.up * speed;
        }
    }
}
