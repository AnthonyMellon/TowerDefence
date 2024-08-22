using System.Collections.Generic;

namespace Spawning
{
    public class WaveInstruction
    {
        public List<SpawnInstruction> SpawnInstructions { get; private set; }

        public WaveInstruction(List<SpawnInstruction> spawnInstructions)
        {
            SpawnInstructions = spawnInstructions;
        }
    }
}

