using Assets.Objects.ObjectPooler;
using UnityEngine;

namespace Assets.Code.WaveManagement
{
    public abstract class Wave
    {
        public abstract int ZOMBIECOUNT { get; }
        public abstract int SEQUENCE { get;}

        protected GameObject spawnPoints = null;
        public Wave()
        {
            spawnPoints = GameObject.Find("SpawnPoints");
            iteration = 0;
        }
        protected int iteration = 0;
        public virtual int GetZombieCount()
        {
            return ZOMBIECOUNT;
        }
        public virtual int GetSequence()
        {
            return SEQUENCE;
        }
        public virtual void SpawnWave()
        {
            for (iteration = iteration; iteration < spawnPoints.transform.childCount; iteration++)
            {
                Transform spawner = spawnPoints.transform.GetChild(iteration);
                Game.POOL.SpawnFromPool("zombie", spawner.position, spawner.rotation);
                if (iteration + 1 == spawnPoints.transform.childCount && 0 <= WaveManager.Instance.currentAlive)
                {
                    iteration = 0;
                }
                if (Game.POOL.poolDictionary["zombie"].Count <= 0)
                {
                    iteration++;
                    break;
                }
            }
        }
    }
}
