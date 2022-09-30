using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
/* > A TurretPreset is a ScriptableObject that contains a fire rate and a bullet type */
public class TurretPreset : ScriptableObject
{
    [Tooltip("time factor to rotate with mouse")] [Range(0, 10f)]
    public float fireRate = .5f;

    [Tooltip("Prefab bullet type")] public GameObject bulletType;

}