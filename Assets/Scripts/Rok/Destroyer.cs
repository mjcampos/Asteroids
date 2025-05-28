using UnityEngine;

namespace Rok {
    public class Destroyer : MonoBehaviour {
        [SerializeField] SpawnSide spawnSide;

        public void SetSpawnSide(SpawnSide spawnSide)
        {
            this.spawnSide = spawnSide;
        }
    }
}
