using UnityEngine;

public enum RokSize {
    Small = 1,
    Medium = 2,
    Large = 3,
}

[CreateAssetMenu(fileName = "RokScriptableObject", menuName = "Scriptable Objects/RokSO")]
public class RokScriptableObject : ScriptableObject {
    public RokSize rokSize;
    public RokPrefabsSO rokPrefabsSO;
    
    public RokData GenerateRandomRok() {
        GameObject[] prefabs = rokPrefabsSO.rokPrefabs;
        GameObject rokInstance = prefabs[Random.Range(0, prefabs.Length)];
        
        return new RokData(rokInstance, rokSize);
    }
}
