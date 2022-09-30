using System;
using UnityEngine;

namespace Game.Assets
{
    public class Bullet : MonoBehaviour
    {
        public float speed = 40f;
        public float lifeTime = 10f;
        public bool canKill = true;

        /// <summary>
        /// > Destroy the gameObject after a certain amount of time
        /// </summary>
        private void Awake()
        {
            Destroy(gameObject, lifeTime);
        }

        /// <summary>
        /// > If the speed is not equal to zero, then move the object forward by the speed multiplied by the time since the
        /// last frame
        /// </summary>
        private void Update()
        {
            if (speed != 0)
            {
                transform.position += transform.forward * (speed * Time.deltaTime);
            }
        }

        /// <summary>
        /// If the player collides with the enemy, the game is over
        ///
        ///
        /// WIP : next feature is to add a health system to the player and enemies
        ///
        /// 
        /// </summary>
        /// <param name="hit">This is the collision that the object has hit.</param>
        private void OnCollisionEnter(Collision hit)
        {
            if (canKill && hit.gameObject.CompareTag("Player"))
            {
                GameManager.Instance.GameOver();
            }
        }
    }
}