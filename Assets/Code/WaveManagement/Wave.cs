using UnityEngine;

namespace Assets.Code.WaveManagement
{
    public abstract class Wave
    {
        public abstract int ZOMBIECOUNT { get; }
        public abstract int SEQUENCE { get; }
        public abstract int MAXZOMBIECOUNT { get; }
        public abstract bool NEXTTRACK { get; }

        protected readonly int zombiePoolSize;
        protected int iteration = 0;
        protected GameObject spawnPoints = null;

        public Wave()
        {
            zombiePoolSize = Game.POOL.poolDictionary["zombie"].Count;
            spawnPoints = GameObject.Find("SpawnPoints");
            iteration = 0;
        }
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
            while (true)
            {
                if (iteration == spawnPoints.transform.childCount && 0 <= WaveManager.Instance.currentAlive)
                {
                    iteration = 0;
                }
                Transform spawner = spawnPoints.transform.GetChild(iteration);
                Game.POOL.SpawnFromPool("zombie", spawner.position, spawner.rotation);

                int zombiesSpawned = zombiePoolSize - Game.POOL.poolDictionary["zombie"].Count;
                if (zombiesSpawned >= MAXZOMBIECOUNT || Game.POOL.poolDictionary["zombie"].Count <= 0)
                {
                    iteration++;
                    break;
                }
                iteration++;
            }
        }
    }
}
