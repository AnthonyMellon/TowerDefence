using UnityEngine;
using Zenject;

namespace Enemies
{
    public class Enemy : MonoBehaviour
    {
        public EnemyStats MaxStats { get; private set; }
        public EnemyStats CurrStats { get; private set; }

        private EnemyStatBroker _statBroker;
        private Path _path;
        private Vector2 _currPoint;
        private Vector2? _nextPoint;
        private int _currPointIndex;
        private float _progressTowardsNextPoint;

        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        [Inject]
        private void Initialise(EnemyStats stats, Path path)
        {
            SetMaxStats(stats, true);
            _statBroker = new EnemyStatBroker();
            _path = path;
        }

        private void Start()
        {
            SetupOnPath();

            //Temp but kinda cool
            _spriteRenderer.color = new Color(
                CurrStats.stats[EnemyStats.StatTypes.Health] / (float)CurrStats.GetTotalPower(),
                CurrStats.stats[EnemyStats.StatTypes.Armour] / (float)CurrStats.GetTotalPower(),
                CurrStats.stats[EnemyStats.StatTypes.Speed] / (float)CurrStats.GetTotalPower()
            );
        }

        private void Update()
        {
            Move();
        }

        private void SetupOnPath()
        {
            _currPointIndex = 0;
            _currPoint = _path.GetFirstPoint();
            _nextPoint = _path.GetNextPoint(0);
            transform.position = _path.GetFirstPoint();
        }

        private void OnReachPoint()
        {
            if(_nextPoint != null) //If there is another point to head towards
            {
                _currPointIndex++;
                _progressTowardsNextPoint = 0;

                _currPoint = _nextPoint.Value;
                _nextPoint = _path.GetNextPoint(_currPointIndex);
            }
            
            
            if(_nextPoint == null)
            {
                OnEscape();
            }
        }

        private void Move()
        {
            if (_nextPoint == null) return; //There is no point to move to

            _progressTowardsNextPoint += Time.deltaTime * CurrStats.stats[EnemyStats.StatTypes.Speed];
            transform.position = Vector2.Lerp(_currPoint, _nextPoint.Value, _progressTowardsNextPoint);

            if (_progressTowardsNextPoint >= 1) OnReachPoint();
        }

        private void OnEscape()
        {
            //Remove lives
            Debug.Log("I escaped!");

            Kill();
        }

        private void OnDeath()
        {
            //Add money

            Kill();
        }

        private void Kill()
        {
            Destroy(gameObject);
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

        public class Factory : PlaceholderFactory<EnemyStats, Path, Enemy> { };
    }
}

