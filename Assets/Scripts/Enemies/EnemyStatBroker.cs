using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
using Zenject;

namespace Enemies
{
    public class EnemyStatBroker : MonoBehaviour
    {
        [SerializeField] private EnemyConfig _enemyConfig;

        private Dictionary<EnemyStatTypes, float> _weights
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
        private Dictionary<EnemyStatTypes, float> _weightsBacker;
        private float _totalWeight;

        public EnemyStats GetNewStats(int maxValue)
        {
            CalculateWeights();

            int health = Mathf.CeilToInt(maxValue*(_weights[EnemyStatTypes.Health] / _totalWeight));
            if (health == 0) health = 1;

            int armour = Mathf.CeilToInt(maxValue*(_weights[EnemyStatTypes.Armour] / _totalWeight));

            int speed = Mathf.CeilToInt(maxValue*(_weights[EnemyStatTypes.Speed] / _totalWeight));
            if (speed == 0) speed = 1;
            
            EnemyStats newStats = new EnemyStats(health, armour, speed, _enemyConfig);
            return newStats;
        }

        private void CalculateWeights()
        {
            //Eventually this will calculate the stats based off the wave number / players power level or something
            _weights = new Dictionary<EnemyStatTypes, float>
            {
                { EnemyStatTypes.Health, Random.Range(0.1f, 1f) },
                { EnemyStatTypes.Armour, Random.Range(0.1f, 1f) },
                { EnemyStatTypes.Speed, Random.Range(0.1f, 1f) }
            };

            _totalWeight = 0;
            foreach(KeyValuePair<EnemyStatTypes, float> weights in _weights)
            {
                _totalWeight += weights.Value;
            }
        }
    }
}

