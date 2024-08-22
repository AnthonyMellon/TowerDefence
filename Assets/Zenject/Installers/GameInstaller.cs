using Spawning;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private Spawner _spawner;
    public override void InstallBindings()
    {
        Container.Bind().FromComponentInNewPrefab(_spawner).AsSingle();

        Container.BindFactory<WaveManager, WaveManager.Factory>().WithArguments(_spawner);
    }
}