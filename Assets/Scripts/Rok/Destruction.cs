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

        void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("RokSpawner")) {
                SpawnSide otherSide = other.gameObject.GetComponent<Spawning>().GetSpawnSide();
                
                if (originSide != otherSide) {
                    StartCoroutine(DestroyAfterDelay());
                }
            } else {
                Destroy(gameObject);
            }
        }

        IEnumerator DestroyAfterDelay() {
            yield return new WaitForSeconds(destroyDelay);
            Destroy(gameObject);
        }
    }
}
