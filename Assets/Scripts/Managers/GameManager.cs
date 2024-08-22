using Spawning;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    private WaveManager.Factory _waveManagerFactory;

    [Inject]
    private void Initialise(WaveManager.Factory waveManagerFactroy)
    {
        _waveManagerFactory = waveManagerFactroy;
    }

    private void Start()
    {
        WaveManager waveManager = _waveManagerFactory.Create();
        waveManager.StartNewWave();
    }
}
