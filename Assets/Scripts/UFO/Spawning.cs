using System.Collections;
using UnityEngine;

namespace UFO {
    public class Spawning : MonoBehaviour {
        [SerializeField] Rok.Spawning[] spawning;
        [SerializeField] float minSpawnTime = 5f;
        [SerializeField] float maxSpawnTime = 10f;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start() {
            StartCoroutine(SpawnTimer());
        }

        IEnumerator SpawnTimer() {
            while (true) {
                float waitTime = Random.Range(minSpawnTime, maxSpawnTime);

                yield return new WaitForSeconds(waitTime);
                
                Rok.Spawning spawner = spawning[Random.Range(0, spawning.Length)];

                spawner.SpawnChosen = true;
            }
        }
    }
}
