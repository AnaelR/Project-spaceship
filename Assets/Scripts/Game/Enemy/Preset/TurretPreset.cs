using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class TurretPreset : ScriptableObject
{
    [Tooltip("time factor to rotate with mouse")] [Range(0, 10f)]
    public float fireRate = .5f;

    [Tooltip("Prefab bullet type")] public GameObject bulletType;
}