using UnityEngine;

[CreateAssetMenu(fileName = "RokSO", menuName = "Scriptable Objects/RokSO")]
public class RokSO : ScriptableObject
{
    public GameObject[] AsteroidPrefabs;
    
    public GameObject GenerateRandomAsteroid()
    {
        return AsteroidPrefabs[Random.Range(0, AsteroidPrefabs.Length)];
    }
}
