using System.Collections.Generic;
using Enemies;
using UnityEngine;

namespace Spawning
{
    public class SpawnInstruction
    {
        private int _minEnemyPower;
        private int _maxEnemyPower;
        public int EnemyPower => Random.Range(_minEnemyPower, _maxEnemyPower);
        public int NumEnemies { get; private set; }

        public float DelayBefore { get; private set; }
        public float DelayAfter { get; private set; }
        public float DelayBetween { get; private set; }

        public SpawnInstruction(int minEnemyPower, int maxEnemyPower, int numEnemies, float delayBefore = 0, float delayAfter = 0, float delayBetween = 0)
        {
            _minEnemyPower = minEnemyPower;
            _maxEnemyPower = maxEnemyPower;
            NumEnemies = numEnemies;
            DelayBefore = delayBefore;
            DelayAfter = delayAfter;
            DelayBetween = delayBetween;
        }
    }
}

