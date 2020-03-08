using Assets.Objects.ObjectPooler;

[System.Serializable]
public class Pool
{
    public string tag;
    public PooledObject prefab;
    public int size;
}
