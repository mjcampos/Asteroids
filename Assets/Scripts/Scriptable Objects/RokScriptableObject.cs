using UnityEngine;

public enum RokSize {
    Small = 1,
    Medium = 2,
    Large = 3,
}

[CreateAssetMenu(fileName = "RokScriptableObject", menuName = "Scriptable Objects/RokSO")]
public class RokScriptableObject : ScriptableObject {
    public RokSize rokSize;
    public GameObject[] rokPrefabs;
    
    public RokData GenerateRandomRok()
    {
        GameObject rokInstance = rokPrefabs[Random.Range(0, rokPrefabs.Length)];
        
        return new RokData(rokInstance, rokSize);
    }
}
