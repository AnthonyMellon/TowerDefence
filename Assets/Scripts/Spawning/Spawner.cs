using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemies;
using Zenject;
using System;

namespace Spawning
{
    public class Spawner: MonoBehaviour
    {
        private Enemy.Factory _enemyFactory;
        private PathManager _pathManager;
        private EnemyStatBroker _statBroker;

        [Inject]
        private void Initialise(Enemy.Factory enemyFactory, PathManager pathManager, EnemyStatBroker statBroker)
        {
            _enemyFactory = enemyFactory;
            _pathManager = pathManager;
            _statBroker = statBroker;
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
            Path path = _pathManager.GetRandomPath();

            Enemy spawnedEnemy = _enemyFactory.Create(stats, path);
            spawnedEnemy.transform.SetParent(transform);
        }       

        /// <summary>
        /// Follow a <see cref="SpawnInstruction"/>, spawning a group of enemies
        /// </summary>
        /// <param name="spawnInstruction">The spawn instruction to be followed</param>
        /// <returns></returns>
        private IEnumerator SpawnGroupRoutine(SpawnInstruction spawnInstruction)
        {
            yield return new WaitForSeconds(spawnInstruction.DelayBefore);

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


