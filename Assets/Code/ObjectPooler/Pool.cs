using Assets.Objects.ObjectPooler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pool
{
    public string tag;
    public PooledObject prefab;
    public int size;
}
