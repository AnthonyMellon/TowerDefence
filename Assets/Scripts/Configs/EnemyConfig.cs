using Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyConfig", menuName = GameConstants.AssetMenuBasePath + "Configs/Enemy")]
public class EnemyConfig : ScriptableObject
{
    public uint MaxSpeed => _maxSpeed;
    [Range(0, 15)]
    [SerializeField] private uint _maxSpeed;
    public float SpeedModifier => _speedModifier;
    [Range(0, 0.1f)]
    [SerializeField] private float _speedModifier;
}
