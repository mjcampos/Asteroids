using System;
using UnityEngine;

namespace UFO {
    public class Shooting : MonoBehaviour {
        [SerializeField] GameObject laserPrefab;
        [SerializeField] float fireInterval = 2f;

        Transform _player;

        void Start() {
            _player = GameObject.FindGameObjectWithTag("Player").transform;
            
            InvokeRepeating(nameof(Fire), fireInterval, fireInterval);
        }
        
        void Fire() {
            if (_player == null) return;
            
            // Calculate direction to player
            Vector2 direction = (_player.position - transform.position).normalized;
            
            // Instantiate the laser
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            
            laser.transform.rotation = Quaternion.Euler(0f, 0f, angle);
            laser.transform.parent = null;
        }
    }
}
