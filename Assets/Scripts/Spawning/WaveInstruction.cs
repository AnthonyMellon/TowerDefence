using System.Collections.Generic;

namespace Spawning
{
    public class WaveInstruction
    {
        public List<SpawnInstruction> SpawnInstructions { get; private set; }
        public float DelayBefore { get; private set; }
        public float DelayAfter { get; private set; }

        public WaveInstruction(List<SpawnInstruction> spawnInstructions, float delayBefore = 0, float delayAfter = 0)
        {
            SpawnInstructions = spawnInstructions;
            DelayBefore = delayBefore;
            DelayAfter = delayAfter;
        }
    }
}

