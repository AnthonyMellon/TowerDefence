using System.Runtime.CompilerServices;
using UnityEngine;
using Zenject;

namespace Enemies
{
    public class Enemy : MonoBehaviour
    {
        public EnemyStats MaxStats { get; private set; }
        public EnemyStats CurrStats { get; private set; }

        private EnemyStatBroker _statBroker;

        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        [Inject]
        private void Initialise(EnemyStats stats)
        {
            SetMaxStats(stats, true);
            _statBroker = new EnemyStatBroker();

            Debug.Log($"I exist! {MaxStats}");
        }

        private void Start()
        {

            //Temp but kinda cool
            _spriteRenderer.color = new Color(
                CurrStats.stats[EnemyStats.StatTypes.Health] / (float)CurrStats.GetTotalPower(),
                CurrStats.stats[EnemyStats.StatTypes.Armour] / (float)CurrStats.GetTotalPower(),
                CurrStats.stats[EnemyStats.StatTypes.Speed] / (float)CurrStats.GetTotalPower()
            );
        }

        private void Update()
        {
            transform.Translate(new Vector3(CurrStats.stats[EnemyStats.StatTypes.Speed] / 1000f, 0, 0));
        }

        private void SetMaxStats(EnemyStats stats, bool setCurrStats = false)
        {
            if (stats == null) return;

            MaxStats = new EnemyStats(stats);

            if (setCurrStats)
            {
                if (CurrStats == null) CurrStats = new EnemyStats(stats);
                else CurrStats.SetValues(MaxStats);
            }
        }

        public class Factory : PlaceholderFactory<EnemyStats, Enemy> { };
    }
}

