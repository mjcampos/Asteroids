using UnityEngine;

public class RokData
{
    public GameObject Prefab;
    public int Size;
    
    public RokData(GameObject prefab, RokSize size)
    {
        this.Prefab = prefab;
        this.Size = (int)size;
    }
}
