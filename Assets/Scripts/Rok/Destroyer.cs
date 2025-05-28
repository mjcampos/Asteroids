using UnityEngine;

namespace Rok {
    public class Destroyer : MonoBehaviour {
        [SerializeField] SpawnSide originSide;

        public void SetSpawnSide(SpawnSide spawnSide)
        {
            originSide = spawnSide;
        }
    }
}
