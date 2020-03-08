﻿using UnityEngine;

namespace Assets.Objects.ObjectPooler
{
    public abstract class PooledObject : MonoBehaviour
    {
        public abstract void OnObjectSpawn();
        public abstract void OnObjectDespawn();
    }
}
