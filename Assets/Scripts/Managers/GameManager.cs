using Spawning;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    private WaveManager _waveManager;

    [Inject]
    private void Initialise(WaveManager waveManager)
    {
        _waveManager = waveManager;
    }

    private void Start()
    {        
        _waveManager.CreateNextWave();
    }
}
