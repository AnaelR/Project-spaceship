using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLifeBarController : MonoBehaviour
{
    /// <summary>
    /// If there is a main camera, look at it and rotate 180 degrees.
    /// </summary>
    /// <returns>
    /// The transform of the object the script is attached to.
    /// </returns>
    void Update()
    {
        if (!Camera.main) return;
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0);
    }
}
