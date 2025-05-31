using UnityEngine;

namespace Rok {
    public class GlobalSpawner : MonoBehaviour {
        public static GlobalSpawner Instance { get; private set; }

        [SerializeField] RokRandomScriptableObject rokRandomScriptableObject;
        [SerializeField] int numberOfMediumRoksToGenerate = 2;
        [SerializeField] int numberOfSmallRoksToGenerate = 3;
        
        private void Awake() {
            // Singleton Logic
            if (Instance != null && Instance != this) {
                Destroy(gameObject);
                return;
            }
            
            Instance = this;
        }

        public void SpawnRok(RokSize rokSize, Vector3 startingPosition) {
            int numberOfRoksToGenerate = rokSize == RokSize.Medium ? numberOfMediumRoksToGenerate : numberOfSmallRoksToGenerate;

            for (int i = 0; i < numberOfRoksToGenerate; i++) {
                RokData rokData = rokSize == RokSize.Medium ? rokRandomScriptableObject.GenerateMediumRok() : rokRandomScriptableObject.GenerateSmallRok();
                GameObject rokInstance = Instantiate(rokData.Prefab, startingPosition, Quaternion.identity);
                
                // Set the rok size
                rokInstance.transform.localScale = Vector3.one * (int)rokData.Size;
                
                // Place the instance in the rok parent object
                rokInstance.transform.SetParent(transform);
                
                // Set up move direction and launch rok
                Movement rokMovement = rokInstance.GetComponent<Movement>();
                float angleDeg = Random.Range(0f, 360f);
                float angleRad = angleDeg * Mathf.Deg2Rad;
                Vector2 moveDirection = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad)).normalized;
                
                rokMovement.Launch(moveDirection);
                
                // Set up rok's spawn side
                Destruction destruction = rokInstance.GetComponent<Destruction>();
                
                destruction.SetProperties(SpawnSide.Center, rokData.Size, rokData.Points);
            }
        }
    }
}
