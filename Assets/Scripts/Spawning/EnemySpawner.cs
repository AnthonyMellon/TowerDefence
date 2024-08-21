using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemies;

namespace Spawning
{
    public class EnemySpawner: MonoBehaviour
    {
        public List<EnemyBase> SpawnedEnemies { get; private set; } = new List<EnemyBase>();

        /// <summary>
        /// Place a single <see cref="EnemyBase"/> in the world
        /// </summary>
        /// <param name="enemyToSpawn">The enemy to be spawned</param>
        private void SpawnSingle(EnemyBase enemyToSpawn)
        {
            //Place in world
            Debug.LogError("Not Implemented");
        }

        /// <summary>
        /// Follow a <see cref="SpawnInstruction"/>, spawning a group of enemies
        /// </summary>
        /// <param name="spawnInstruction">The spawn instruction to be followed</param>
        private void SpawnGroup(SpawnInstruction spawnInstruction)
        {
            //Start the spawn coroutine
            StartCoroutine(SpawnGroupRoutine(spawnInstruction));
        }

        /// <summary>
        /// Follow a <see cref="SpawnInstruction"/>, spawning a group of enemies
        /// </summary>
        /// <param name="spawnInstruction">The spawn instruction to be followed</param>
        /// <returns></returns>
        private IEnumerator SpawnGroupRoutine(SpawnInstruction spawnInstruction)
        {
            yield return new WaitForSeconds(spawnInstruction.DelayBefore);

            for (int i = 0; i < spawnInstruction.EnemiesToSpawn.Count; i++)
            {
                SpawnSingle(spawnInstruction.EnemiesToSpawn[i]);
                yield return new WaitForSeconds(spawnInstruction.DelayBetween);
            }

            yield return new WaitForSeconds(spawnInstruction.DelayAfter);
        }

        /// <summary>
        /// Follow a <see cref="WaveInstruction"/>, spawning a wave of enemies
        /// </summary>
        /// <param name="waveInstruction">The wave instruciton to be followed</param>
        private void SpawnWave(WaveInstruction waveInstruction)
        {
            //Start the wave coroutine
            StartCoroutine(SpawnWaveRoutine(waveInstruction));
        }

        /// <summary>
        /// Follow a <see cref="WaveInstruction"/>, spawning a wave of enemies
        /// </summary>
        /// <param name="waveInstruction">The wave instruction to be followed</param>
        private IEnumerator SpawnWaveRoutine(WaveInstruction waveInstruction)
        {
            yield return new WaitForSeconds(waveInstruction.DelayBefore);

            for (int i = 0; i < waveInstruction.SpawnInstructions.Count; i++)
            {
                SpawnGroup(waveInstruction.SpawnInstructions[i]);
            }

            yield return new WaitForSeconds(waveInstruction.DelayAfter);
        }
    }
}


