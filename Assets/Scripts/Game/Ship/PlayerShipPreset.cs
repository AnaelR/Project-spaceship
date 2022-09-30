using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class PlayerShipPreset : ScriptableObject
{
    [Tooltip("time factor to rotate with mouse")] [Range(0, 400)]
    public float timeFactor = 150f;

    [Tooltip("dead zone in center of screen with no rotation (% of screen)")] [Range(0, 1)]
    public float noTurn = 0.1f;

    [Tooltip("multiplication of speed level")] [Range(0, 50)]
    public float speedRatio = 2;

    [Tooltip("Max forward speed value")] [Range(0, 100)]
    public float speedMaxValue = 10;

    [Tooltip("Min forward speed value")] [Range(-100, 0)]
    public float speedMinValue = -10;

    [Tooltip("Easing time to accelerate")] public float accelerationEase = 1f;

    [Tooltip("Delay before ship rotate")] public float shipRotateDelay = 1f;

    [Tooltip("Delay before ship rotate to origin")]
    public float shipRotateDelayToEnd = 1f;
}