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


        private void Awake()
        {
            if (preset)
            {
                fireRate = preset.fireRate;
                bullet = preset.bulletType;
            }
        }

        private void Start()
        {
            _shotReady = false;
            _lastShot = 0.0f;
            radar.OnTargetDetected += Fire;
        }

        private void Update()
        {
            if (_shotReady && Time.time > _lastShot + fireRate)
            {

                Debug.Log(Time.time + " > " + _lastShot + fireRate);
                _newBullet = Instantiate(bullet, turret.transform.position, Quaternion.identity);
                _newBullet.transform.LookAt(_player.transform.position);
                _lastShot = Time.time;
                Debug.Log("FIRE");
            }
        }

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