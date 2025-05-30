using UnityEngine;

[CreateAssetMenu(fileName = "RokRandomScriptableObject", menuName = "Scriptable Objects/RokRandomSO")]
public class RokRandomScriptableObject : ScriptableObject {
    public RokScriptableObject smallRok;
    public RokScriptableObject mediumRok;
    public RokScriptableObject largeRok;
    
    public RokData GenerateRandomRok() {
        float roll = Random.value;

        if (roll < 0.5f) {
            return smallRok.GenerateRandomRok();
        } else if (roll < 0.8f) {
            return mediumRok.GenerateRandomRok();
        } else {
            return largeRok.GenerateRandomRok();
        }
    }
    
    public RokData GenerateMediumRok() {
        return mediumRok.GenerateRandomRok();
    }

    public RokData GenerateSmallRok() {
        return smallRok.GenerateRandomRok();
    }
}
