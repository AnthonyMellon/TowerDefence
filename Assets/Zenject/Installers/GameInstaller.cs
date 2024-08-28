using Spawning;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    //Managers
    [SerializeField] private PathManager _pathManager;

    [SerializeField] private Spawner _spawner;
    public override void InstallBindings()
    {
        //Managers
        Container.Bind<PathManager>().FromComponentInNewPrefab(_pathManager).AsSingle();
        Container.BindFactory<WaveManager, WaveManager.Factory>().WithArguments(_spawner);

        Container.Bind().FromComponentInNewPrefab(_spawner).AsSingle();

    }
}