using System;
using UnityEngine;

namespace Game.Enemy
{
    public class TurretController : MonoBehaviour
    {
        public ColliderDetection radar;

        public GameObject bullet;
        public GameObject turret;
        private bool _shotReady;
        private GameObject _target;

        private GameObject _player;

        public float fireRate = 0.2f;
        private float _lastShot = 0.0f;
        private GameObject _newBullet;


        private void Start()
        {
            _shotReady = false;
            radar.OnTargetDetected += Fire;
        }

        private void Update()
        {
            if (_shotReady && Time.time > _lastShot + fireRate)
            {
                _newBullet = Instantiate(bullet, turret.transform.position, Quaternion.identity);
                _newBullet.transform.LookAt(_player.transform.position);
                _lastShot = Time.time;
            }
        }

        private void Fire(GameObject target, bool isDetected)
        {
            if (isDetected)
            {
                _player = GameObject.FindWithTag("Player");
                Debug.Log(target.transform.position);
                _target = target;
                _shotReady = true;
                Debug.Log("Firing");
            }
            else
            {
                _shotReady = false;
            }
        }
    }
}