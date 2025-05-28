using UnityEngine;

[CreateAssetMenu(fileName = "RokSO", menuName = "Scriptable Objects/RokSO")]
public class RokScriptableObject : ScriptableObject
{
    public GameObject[] asteroidPrefabs;
    
    public GameObject GenerateRandomAsteroid()
    {
        return asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)];
    }
}
