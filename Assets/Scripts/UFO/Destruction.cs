using System.Collections;
using UnityEngine;

namespace UFO {
    public class Destruction : MonoBehaviour {
        [Header("UFO Properties")]
        [SerializeField] Rok.SpawnSide originSide;
        [SerializeField] int points;
        
        [Header("Explosion Properties")]
        [SerializeField] GameObject explosionParticles;
        [SerializeField] AudioClip explosionClip;
        [SerializeField] float destroyDelay = 1f;
        [SerializeField] int explosionScale = 2;
        
        public void SetProperties(Rok.SpawnSide _originSide, int _points) {
            originSide = _originSide;
            points = _points;
        }
        
        /*
         * There are three ways a UFO can be destroyed:
         * 1. Collides with spawner
         * 2. Collides with player
         * 3. Collides with a laser
         */
        void OnTriggerEnter2D(Collider2D other)
        {
            // 1. Collides with spawner
            if (other.CompareTag("RokSpawner")) {
                Rok.SpawnSide otherSide = other.gameObject.GetComponent<Rok.Spawning>().GetSpawnSide();
                
                if (originSide != otherSide) {
                    StartCoroutine(DestroyAfterDelay());
                }
            } else {
                // Pass along its points to a score manager if the rok is destroyed by collision with laser
                if (other.CompareTag("Laser")) ScoreManager.Instance.UpdateScore(points);
                
                // Instantiate an explosion
                GameObject explosionInstance = Instantiate(explosionParticles, transform.position, Quaternion.identity);
                
                explosionInstance.transform.localScale = Vector3.one * explosionScale;
                explosionInstance.transform.SetParent(null);
                
                // Generate explosion audio
                AudioSource.PlayClipAtPoint(explosionClip, transform.position);
                
                // Then destroy the UFO
                Destroy(gameObject);
            }
        }
        
        IEnumerator DestroyAfterDelay() {
            yield return new WaitForSeconds(destroyDelay);
            Destroy(gameObject);
        }
    }
}
