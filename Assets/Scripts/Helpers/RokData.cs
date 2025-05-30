using UnityEngine;

public class RokData {
    public GameObject Prefab;
    public RokSize Size;
    
    public RokData(GameObject prefab, RokSize size) {
        this.Prefab = prefab;
        this.Size = size;
    }
}
