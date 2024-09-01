using Constants;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor.Rendering;
using UnityEngine;
using Zenject;

namespace Enemies
{
    public class EnemyStats
    {
        private EnemyConfig _config;

        public int Health { get; private set; }
        public int Armour { get; private set; }
        private int _realSpeed;
        public float Speed
        {
            get
            {
                float speed = _realSpeed * _config.SpeedModifier;
                return speed <= _config.MaxSpeed ? speed : _config.MaxSpeed; ;                
            }
            private set
            {
                Debug.LogWarning("something is trying to set real speed, this does nothing");
            }
        }

        public EnemyStats(int health, int armour, int speed, EnemyConfig config)
        {
            Health = health;
            Armour = armour;
            _realSpeed = speed;
            _config = config;
        }

        public EnemyStats(EnemyStats statsToCopy)
        {
            SetValues(statsToCopy);
        }

        public void SetValues(EnemyStats statsToCopy)
        {
            Health = statsToCopy.Health;
            Armour = statsToCopy.Armour;
            _realSpeed = statsToCopy._realSpeed;
            _config = statsToCopy._config;
        }

        public int GetTotalPower()
        {
            return Health + Armour + _realSpeed;           
        }

        public int GetRealSpeed()
        {
            return _realSpeed;
        }

        public override string ToString()
        {
            return $"H:{Health}, A:{Armour}, S:{_realSpeed}, T:{GetTotalPower()}";
        }
    }
}

