using Enemies;
using Spawning;
using System;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    //Managers
    [SerializeField] private PathManager _pathManager;
    [SerializeField] private WaveManager _waveManager;
    
    [SerializeField] private EnemyStatBroker _enemyStatBroker;
    [SerializeField] private Wave _wave;

    public override void InstallBindings()
    {
        //Managers
        Container.Bind<PathManager>().FromComponentInHierarchy(_pathManager).AsSingle();
        Container.Bind<WaveManager>().FromComponentInHierarchy(_waveManager).AsSingle();
        
        Container.Bind<EnemyStatBroker>().FromComponentInHierarchy(_enemyStatBroker).AsSingle();
        Container.BindFactory<WaveInstruction, Action, Wave, Wave.Factory>().FromComponentInNewPrefab(_wave).AsSingle();
    }
}