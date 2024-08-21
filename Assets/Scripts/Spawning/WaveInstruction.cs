using System.Collections.Generic;

namespace Spawning
{
    public class WaveInstruction
    {
        public List<SpawnInstruction> spawnInstructions { get; private set; }
        public float delayBefore { get; private set; }
        public float delayAfter { get; private set; }

        public WaveInstruction(List<SpawnInstruction> spawnInstructions, float delayBefore = 0, float delayAfter = 0)
        {
            this.spawnInstructions = spawnInstructions;
            this.delayBefore = delayBefore;
            this.delayAfter = delayAfter;
        }
    }
}

