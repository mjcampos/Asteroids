using UnityEngine;

public class RokData {
    public GameObject Prefab;
    public RokSize Size;
    public int Points;
    
    public RokData(GameObject prefab, RokSize size, int points) {
        this.Prefab = prefab;
        this.Size = size;
        this.Points = points;
    }
}
