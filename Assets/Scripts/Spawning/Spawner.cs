using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemies;
using Zenject;
using System;
using System.Runtime.CompilerServices;

namespace Spawning
{
    public class Spawner: MonoBehaviour
    {
        private Enemy.Factory _enemyFactory;
        private EnemyStatBroker _statBroker;

        [Inject]
        private void Initialise(Enemy.Factory enemyFactory)
        {
            _enemyFactory = enemyFactory;

            _statBroker = new EnemyStatBroker();
        }

        public List<Enemy> SpawnedEnemies { get; private set; } = new List<Enemy>();

        /// <summary>
        /// Place a single <see cref="Enemy"/> in the world
        /// </summary>
        /// <param name="entityToSpawn">The entity to be spawned</param>
        private void SpawnSingle(int power)
        {
            //Place in world
            EnemyStats stats = _statBroker.GetNewStats(power);
            Enemy spawnedEnemy = _enemyFactory.Create(stats);
            spawnedEnemy.transform.position = new Vector2(-10, 0);
        }

        /// <summary>
        /// Follow a <see cref="SpawnInstruction"/>, spawning a group of enemies
        /// </summary>
        /// <param name="spawnInstruction">The spawn instruction to be followed</param>
        /// <returns></returns>
        private IEnumerator SpawnGroupRoutine(SpawnInstruction spawnInstruction)
        {
            yield return new WaitForSeconds(spawnInstruction.DelayBefore);
            Debug.Log("<color=cyan>Spawning Group!</color>");

            for (int i = 0; i < spawnInstruction.NumEnemies; i++)
            {
                SpawnSingle(spawnInstruction.EnemyPower);
                yield return new WaitForSeconds(spawnInstruction.DelayBetween);
            }

            yield return new WaitForSeconds(spawnInstruction.DelayAfter);
        }

        /// <summary>
        /// Follow a <see cref="WaveInstruction"/>, spawning a wave of enemies
        /// </summary>
        /// <param name="waveInstruction">The wave instruciton to be followed</param>
        public void SpawnWave(WaveInstruction waveInstruction, Action OnComplete)
        {
            Debug.Log("<color=green>Spawning wave!</color>");

            //Start the wave coroutine
            StartCoroutine(SpawnWaveRoutine(waveInstruction, OnComplete));
        }

        /// <summary>
        /// Follow a <see cref="WaveInstruction"/>, spawning a wave of enemies
        /// </summary>
        /// <param name="waveInstruction">The wave instruction to be followed</param>
        private IEnumerator SpawnWaveRoutine(WaveInstruction waveInstruction, Action OnComplete)
        {
            for (int i = 0; i < waveInstruction.SpawnInstructions.Count; i++)
            {
                yield return StartCoroutine(SpawnGroupRoutine(waveInstruction.SpawnInstructions[i]));
            }

            OnComplete?.Invoke();
        }
    }
}


