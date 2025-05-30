using System;
using System.Collections;
using UnityEngine;

namespace Rok {
    public class Destruction : MonoBehaviour {
        [SerializeField] SpawnSide originSide;
        [SerializeField] float destroyDelay = 1f;

        public void SetSpawnSide(SpawnSide spawnSide)
        {
            originSide = spawnSide;
        }

        /**
         * There are three ways a rok can be destroyed:
         * 1. Collides with spawner
         * 2. Collides with player
         * 3. Collides with a laser
         */
        void OnTriggerEnter2D(Collider2D other) {
            // 1. Collides with spawner
            if (other.CompareTag("RokSpawner")) {
                SpawnSide otherSide = other.gameObject.GetComponent<Spawning>().GetSpawnSide();
                
                if (originSide != otherSide) {
                    StartCoroutine(DestroyAfterDelay());
                }
            } else {
                // 2-3. Collides with player or laser
                Destroy(gameObject);
            }
        }

        IEnumerator DestroyAfterDelay() {
            yield return new WaitForSeconds(destroyDelay);
            Destroy(gameObject);
        }
    }
}
