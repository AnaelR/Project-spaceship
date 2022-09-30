using System;
using UnityEngine;

namespace Game.Assets
{
    public class Bullet : MonoBehaviour
    {
        public float speed = 40f;
        public float lifeTime = 10f;

        private void Awake()
        {
            Destroy(gameObject, lifeTime);
        }

        private void Update()
        {
            if (speed != 0)
            {
                transform.position += transform.forward * (speed * Time.deltaTime);
            }
        }
    }
}