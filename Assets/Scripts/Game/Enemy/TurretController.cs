using System;
using UnityEngine;

namespace Game.Enemy
{
    public class TurretController : MonoBehaviour
    {
        public ColliderDetection radar;
        public TurretPreset preset;

        public GameObject bullet;
        public GameObject turret;
        private bool _shotReady;

        private GameObject _player;

        public float fireRate = 0.2f;
        private float _lastShot;
        private GameObject _newBullet;


        /// <summary>
        /// If the preset variable is not null, then set the fireRate and bullet variables to the values of the preset's
        /// fireRate and bulletType variables.
        /// </summary>
        private void Awake()
        {
            if (preset)
            {
                fireRate = preset.fireRate;
                bullet = preset.bulletType;
            }
        }

        /// <summary>
        /// > When the target is detected, fire
        /// </summary>
        private void Start()
        {
            _shotReady = false;
            _lastShot = 0.0f;
            radar.OnTargetDetected += Fire;
        }

        /// <summary>
        /// If the shot is ready and the time is greater than the last shot plus the fire rate, then create a new bullet,
        /// set the last shot to the current time, and fire
        /// </summary>
        private void Update()
        {
            if (_shotReady && Time.time > _lastShot + fireRate)
            {
                _newBullet = Instantiate(bullet, turret.transform.position, Quaternion.identity);
                _newBullet.transform.LookAt(_player.transform.position);
                _lastShot = Time.time;
            }
        }

        /// <summary>
        /// If the player is detected, then the shot is ready
        /// </summary>
        /// <param name="target">The object that the enemy is shooting at.</param>
        /// <param name="isDetected">This is a boolean that is true when the player is in the field of view of the
        /// enemy.</param>
        private void Fire(GameObject target, bool isDetected)
        {
            if (isDetected)
            {
                _player = GameObject.FindWithTag("Player");
                _shotReady = true;
            }
            else
            {
                _shotReady = false;
            }
        }
    }
}