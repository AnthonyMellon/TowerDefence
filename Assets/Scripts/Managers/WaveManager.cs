using Spawning;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Zenject;

public class WaveManager : MonoBehaviour
{
    public int WaveNumber { get; private set; }
    private Wave.Factory _waveFactory;

    [Inject]
    private void Initialise(Wave.Factory waveFactory)
    {
        _waveFactory = waveFactory;
        WaveNumber = 0;
    }

    public void CreateNextWave()
    {
        WaveNumber++;
        Debug.Log($"Creating wave {WaveNumber}");

        //Temp
        Action onComplete = null;
        if (WaveNumber <= 100) onComplete = CreateNextWave;


        WaveInstruction waveInstruction = GenerateNewWaveInstruction();
        _waveFactory.Create(waveInstruction, onComplete);
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
}
