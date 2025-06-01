using System;
using System.Collections;
using UnityEngine;

namespace Laser
{
    public class Destruction : MonoBehaviour {
        [SerializeField] float destroyDelay = 1f;
        
        /**
         * There are two ways to destroy this object:
         * 1. Collides with spawner
         *      a. Delay before destruction to give laser time to leave the screen
         * 2. Collides with rok
         *      a. Destroy immediately
         */
        void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("RokSpawner")) {
                StartCoroutine(DestroyAfterDelay());
            }

            Destroy(gameObject);
        }

        IEnumerator DestroyAfterDelay() {
            yield return new WaitForSeconds(destroyDelay);
            Destroy(gameObject);
        }
    }
}
