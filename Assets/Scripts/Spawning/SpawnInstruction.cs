using System.Collections.Generic;
using Enemies;

namespace Spawning
{
    public class SpawnInstruction
    {
        public List<EnemyBase> EnemiesToSpawn { get; private set; }
        public float DelayBefore { get; private set; }
        public float DelayAfter { get; private set; }
        public float DelayBetween { get; private set; }

        public SpawnInstruction(List<EnemyBase> enemiesToSpawn, float delayBefore = 0, float delayAfter = 0, float delayBetween = 0)
        {
            EnemiesToSpawn = enemiesToSpawn;
            DelayBefore = delayBefore;
            DelayAfter = delayAfter;
            DelayBetween = delayBetween;
        }
    }
}

