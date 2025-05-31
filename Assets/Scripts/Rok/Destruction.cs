using System;
using System.Collections;
using UnityEngine;

namespace Rok {
    public class Destruction : MonoBehaviour {
        [SerializeField] SpawnSide originSide;
        [SerializeField] RokSize rokSize;
        [SerializeField] float destroyDelay = 1f;
        [SerializeField] GameObject explosionParticles;
        [SerializeField] int points;

        public void SetProperties(SpawnSide _originSide, RokSize _rokSize, int _points) {
            originSide = _originSide;
            rokSize = _rokSize;
            points = _points;
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
                switch (rokSize) {
                    case RokSize.Large:
                        GlobalSpawner.Instance.SpawnRok(RokSize.Medium, transform.position);
                        break;
                    case RokSize.Medium:
                        GlobalSpawner.Instance.SpawnRok(RokSize.Small, transform.position);
                        break;
                    case RokSize.Small:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
                // Pass along its points to a score manager if the rok is destroyed by collision with laser
                if (other.CompareTag("Laser")) ScoreManager.Instance.UpdateScore(points);
                
                // Instantiate an explosion
                GameObject explosionInstance = Instantiate(explosionParticles, transform.position, Quaternion.identity);
                
                explosionInstance.transform.localScale = Vector3.one * (int)rokSize;
                explosionInstance.transform.SetParent(null);
                
                // Then destroy the rok
                Destroy(gameObject);
            }
        }

        IEnumerator DestroyAfterDelay() {
            yield return new WaitForSeconds(destroyDelay);
            Destroy(gameObject);
        }
    }
}
