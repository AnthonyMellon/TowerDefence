using Enemies;
using System.Runtime.CompilerServices;
using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    [SerializeField] private Enemy _enemyBase;

    public override void InstallBindings()
    {        
        Container.BindFactory<EnemyStats, Enemy, Enemy.Factory>().FromComponentInNewPrefab(_enemyBase).AsSingle();
    }
}