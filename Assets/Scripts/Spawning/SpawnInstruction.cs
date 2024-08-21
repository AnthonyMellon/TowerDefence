using System.Collections.Generic;
using Enemies;

namespace Spawning
{
    public class SpawnInstruction
    {
        public List<EnemyBase> enemiesToSpawn { get; private set; }
        public float delayBefore { get; private set; }
        public float delayAfter { get; private set; }
        public float delayBetween { get; private set; }

        public SpawnInstruction(List<EnemyBase> enemiesToSpawn, float delayBefore = 0, float delayAfter = 0, float delayBetween = 0)
        {
            this.enemiesToSpawn = enemiesToSpawn;
            this.delayBefore = delayBefore;
            this.delayAfter = delayAfter;
            this.delayBetween = delayBetween;
        }
    }
}

