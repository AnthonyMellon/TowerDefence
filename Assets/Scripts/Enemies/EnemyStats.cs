using Constants;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

namespace Enemies
{
    public class EnemyStats
    {
        public enum StatTypes
        {
            Health,
            Armour,
            Speed
        }

        public Dictionary<StatTypes, int> stats { get; private set; }

        public EnemyStats(int health, int armour, int speed)
        {
            stats = new Dictionary<StatTypes, int>
            {
                { StatTypes.Health, health },
                { StatTypes.Armour, armour },
                { StatTypes.Speed, speed }
            };
        }

        public EnemyStats(EnemyStats statsToCopy)
        {
            SetValues(statsToCopy);
        }

        public void SetValues(EnemyStats statsToCopy)
        {
            if (stats == null) stats = new Dictionary<StatTypes, int>();

            foreach(KeyValuePair<StatTypes, int> entry in statsToCopy.stats)
            {
                if (stats.ContainsKey(entry.Key))
                {
                    stats[entry.Key] = entry.Value;

                }
                else
                {
                    stats.Add(entry.Key, entry.Value);
                }
            }
        }

        public int GetTotalPower()
        {
            int totalPower = 0;
            foreach(KeyValuePair<StatTypes, int> stat in stats)
            {
                totalPower += stat.Value;
            }

            return totalPower;
        }

        public override string ToString()
        {
            return $"H:{stats[StatTypes.Health]}, A:{stats[StatTypes.Armour]}, S:{stats[StatTypes.Speed]}, T:{GetTotalPower()}";
        }
    }
}

