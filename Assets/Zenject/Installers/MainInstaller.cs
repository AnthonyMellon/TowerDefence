using Enemies;
using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    //Entities
    [SerializeField] private Enemy _enemy;

    public override void InstallBindings()
    {
        //Entities
        Container.BindFactory<EnemyStats, Path, Enemy, Enemy.Factory>().FromComponentInNewPrefab(_enemy).AsSingle();
    }
}