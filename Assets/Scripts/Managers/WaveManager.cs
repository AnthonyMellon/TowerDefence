using Spawning;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Zenject;

public class WaveManager
{
    public int WaveNumber { get; private set; }
    private Spawner _spawner;

    [Inject]
    private void Initialise(Spawner spawner)
    {
        _spawner = spawner;
        WaveNumber = 0;
    }

    public void StartNewWave()
    {
        WaveNumber++;
        Debug.Log($"Starting wave {WaveNumber}");

        //Temp
        Action onComplete = null;
        if (WaveNumber <= 100) onComplete = StartNewWave;


        WaveInstruction waveInstruction = GenerateNewWaveInstruction();
        _spawner.SpawnWave(waveInstruction, onComplete);
    }

    private WaveInstruction GenerateNewWaveInstruction()
    {
        int numGroups = Mathf.CeilToInt(WaveNumber / 3f); //TODO: better group number deciding

        List<SpawnInstruction> spawnInstructions = new List<SpawnInstruction>();
        for(int i = 0; i < numGroups; i++)
        {
            spawnInstructions.Add(GenerateNewSpawnInstruction());
        }

        return new WaveInstruction(spawnInstructions);
    }

    private SpawnInstruction GenerateNewSpawnInstruction()
    {
        //TODO: better way of deciding these numbers
        int minPower = WaveNumber*10;
        int maxPower = WaveNumber*15;
        int numEnemies = WaveNumber + 1;
        float delayBefore = UnityEngine.Random.Range(0.1f, 0.25f);

        return new SpawnInstruction(minPower, maxPower, numEnemies, delayBetween: 0f, delayBefore: delayBefore);
    }

    public class Factory : PlaceholderFactory<WaveManager> { };
}
