using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Rok {
    public enum SpawnSide {
        Top,
        Bottom,
        Left,
        Right,
        Center
    }
    
    public class Spawning : MonoBehaviour {
        [SerializeField] RokRandomScriptableObject rokRandomScriptableObject;
        [SerializeField] GameObject rokParent;
        
        [Header("Spawner Info")]
        [SerializeField] float minSpawnDelay = 1f;
        [SerializeField] float maxSpawnDelay = 3f;
        [SerializeField] BoxCollider2D spawnArea;
        [SerializeField] SpawnSide spawnSide;
        
        [Header("Spawn Angle")]
        [SerializeField] float minAngle = 120f;
        [SerializeField] float maxAngle = 240f;
        [SerializeField] float lineLength = 4f;
        
        void Start() {
            StartCoroutine(SpawnLoop());
        }

        IEnumerator SpawnLoop() {
            while (true) {
                yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
                
                // Spawn a rok
                RokData rokData = rokRandomScriptableObject.GenerateRandomRok();
                GameObject rokInstance = Instantiate(rokData.Prefab, GetRandomPointInBounds(), Quaternion.identity);
                
                // Set the rok size
                rokInstance.transform.localScale = Vector3.one * (int)rokData.Size;
                
                // Place the instance in the rok parent object
                rokInstance.transform.SetParent(rokParent.transform);
                
                // Set up move direction and launch rok
                Movement rokMovement = rokInstance.GetComponent<Movement>();
                float angleDeg = Random.Range(minAngle, maxAngle);
                float angleRad = angleDeg * Mathf.Deg2Rad;
                Vector2 moveDirection = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad)).normalized;
                
                rokMovement.Launch(moveDirection);
                
                // Set the rok's spawn side
                Destruction destruction = rokInstance.GetComponent<Destruction>();
                
                destruction.SetProperties(spawnSide, rokData.Size, rokData.Points);
            }
        }

        Vector2 GetRandomPointInBounds() {
            Bounds bounds = spawnArea.bounds;
            
            float x = Random.Range(bounds.min.x, bounds.max.x);
            float y = Random.Range(bounds.min.y, bounds.max.y);
            
            return new Vector2(x, y);
        }

        public SpawnSide GetSpawnSide() => spawnSide;

        void OnDrawGizmos() {
            if (spawnArea == null) return;
            
            Gizmos.color = Color.red;
            
            // Get the center of the spawn area
            Vector2 center = spawnArea.bounds.center;
            
            // Draw direction lines
            Vector2 minDir = AngleToVector(minAngle);
            Vector2 maxDir = AngleToVector(maxAngle);
            Vector2 midDir = AngleToVector((minAngle + maxAngle) / 2f);
            
            Gizmos.DrawLine(center, center + minDir * lineLength);
            Gizmos.DrawLine(center, center + maxDir * lineLength);
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(center, center + midDir * lineLength);
            
            // Draw arc approximation
            Gizmos.color = new Color(1f, 0.5f, 0f, 0.2f);

            int arcSteps = 20;

            for (int i = 0; i < arcSteps; i++)
            {
                float t1 = Mathf.Lerp(minAngle, maxAngle, i / (float)arcSteps);
                float t2 = Mathf.Lerp(minAngle, maxAngle, (i + 1) / (float)arcSteps);
                Vector2 p1 = center + AngleToVector(t1) * lineLength;
                Vector2 p2 = center + AngleToVector(t2) * lineLength;
                Gizmos.DrawLine(p1, p2);
            }
        }

        Vector2 AngleToVector(float angleDegrees) {
            float angleRad = angleDegrees * Mathf.Deg2Rad;

            return new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
        }
    }
}

