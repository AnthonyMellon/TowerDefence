using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;

namespace Enemies
{
    public class EnemyStatBroker
    {
        private Dictionary<EnemyStats.StatTypes, float> _weights
        {
            get
            {
                if (_weightsBacker == null) CalculateWeights();
                return _weightsBacker;
            }
            set
            {
                _weightsBacker = value;
            }
        }
        private Dictionary<EnemyStats.StatTypes, float> _weightsBacker;
        private float _totalWeight;

        public EnemyStats GetNewStats(int maxValue)
        {
            CalculateWeights();

            int health = Mathf.CeilToInt(maxValue*(_weights[EnemyStats.StatTypes.Health] / _totalWeight));
            if (health == 0) health = 1;

            int armour = Mathf.CeilToInt(maxValue*(_weights[EnemyStats.StatTypes.Armour] / _totalWeight));

            int speed = Mathf.CeilToInt(maxValue*(_weights[EnemyStats.StatTypes.Speed] / _totalWeight));
            if (speed == 0) speed = 1;

            EnemyStats newStats = new EnemyStats(health, armour, speed);
            return newStats;
        }

        private void CalculateWeights()
        {
            //Eventually this will calculate the stats based off the wave number / players power level or something
            _weights = new Dictionary<EnemyStats.StatTypes, float>
            {
                { EnemyStats.StatTypes.Health, Random.Range(0.1f, 1f) },
                { EnemyStats.StatTypes.Armour, Random.Range(0.1f, 1f) },
                { EnemyStats.StatTypes.Speed, Random.Range(0.1f, 1f) }
            };

            _totalWeight = 0;
            foreach(KeyValuePair<EnemyStats.StatTypes, float> weights in _weights)
            {
                _totalWeight += weights.Value;
            }
        }
    }
}

