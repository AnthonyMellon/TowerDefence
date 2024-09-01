using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemies;
using Zenject;
using System;

namespace Spawning
{
    public class Wave : MonoBehaviour
    {
        private WaveInstruction _waveInstruction;
        private Action _onComplete;
        private Enemy.Factory _enemyFactory;
        private PathManager _pathManager;
        private EnemyStatBroker _statBroker;

        private bool _allEnemiesSpawned;
        public List<Enemy> SpawnedEnemies { get; private set; } = new List<Enemy>();

        [Inject]
        private void Initialise(WaveInstruction waveInstruction, Action onComplete, Enemy.Factory enemyFactory, PathManager pathManager, EnemyStatBroker statBroker)
        {
            _waveInstruction = waveInstruction;
            _onComplete = onComplete;
            _enemyFactory = enemyFactory;
            _pathManager = pathManager;
            _statBroker = statBroker;
        }

        private void Start()
        {
            StartCoroutine(SpawnWaveRoutine(_waveInstruction));
        }

        private void Update()
        {
            if(_allEnemiesSpawned && SpawnedEnemies.Count == 0)
            {
                Debug.Log("Wave Over!");
                _onComplete.Invoke();
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// Place a single <see cref="Enemy"/> in the world
        /// </summary>
        /// <param name="entityToSpawn">The entity to be spawned</param>
        private void SpawnEnemy(int power)
        {
            //Place in world
            EnemyStats stats = _statBroker.GetNewStats(power);
            Path path = _pathManager.GetRandomPath();

            Enemy spawnedEnemy = _enemyFactory.Create(stats, path);
            spawnedEnemy.transform.SetParent(transform);
            spawnedEnemy.OnDelete += HandleEnemyDelete;
            SpawnedEnemies.Add(spawnedEnemy);
        }       

        private void HandleEnemyDelete(Enemy deletedEnemy)
        {
            SpawnedEnemies.Remove(deletedEnemy);
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
                SpawnEnemy(spawnInstruction.EnemyPower);
                yield return new WaitForSeconds(spawnInstruction.DelayBetween);
            }

            yield return new WaitForSeconds(spawnInstruction.DelayAfter);
        }

        /// <summary>
        /// Follow a <see cref="WaveInstruction"/>, spawning a wave of enemies
        /// </summary>
        /// <param name="waveInstruction">The wave instruction to be followed</param>
        private IEnumerator SpawnWaveRoutine(WaveInstruction waveInstruction, Action OnComplete = null)
        {
            for (int i = 0; i < waveInstruction.SpawnInstructions.Count; i++)
            {
                yield return StartCoroutine(SpawnGroupRoutine(waveInstruction.SpawnInstructions[i]));
            }
          
            _allEnemiesSpawned = true;
        }

        public class Factory : PlaceholderFactory<WaveInstruction, Action, Wave> { };
    }
}


