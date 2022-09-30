using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Apple;

namespace Game.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        //Enemy health
        public float enemyHealth = 10f;
        private bool _isDead = false;

        private float shipRotation = 0f;


        /// <summary>
        /// If the enemy is not dead, subtract the damage from the enemy's health, and if the enemy's health is less than or
        /// equal to zero, call the Die() function
        ///
        /// WIP Not implemented yet
        ///
        /// 
        /// </summary>
        /// <param name="theDamage">The amount of damage to apply to the enemy.</param>
        public void ApplyDamage(float theDamage)
        {
            if (!_isDead)
            {
                enemyHealth = enemyHealth - theDamage;

                if (enemyHealth <= 0)
                {
                    Die();
                }
            }
        }

        /// <summary>
        /// If the player is dead, destroy the game object after 5 seconds
        ///
        /// 
        /// WIP Not implemented yet
        ///
        /// 
        /// </summary>
        private void Die()
        {
            _isDead = true;
            //Destroy
            Destroy(transform.gameObject, 5);
        }
        
        /// <summary>
        /// If the ship doesn't look at the destination point then rotate it to face it progressively, otherwise move it
        /// forward
        /// </summary>
        /// <param name="Vector3">agentDesiredVelocity - the velocity of the agent</param>
        /// <param name="Vector3">destination - the target destination</param>
        public void Move(Vector3 agentDesiredVelocity, Vector3 destination)
        {
            
            var direction = destination - this.transform.position;
            var angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            //if ship doesn't look at destination point then rotate it to face it progressively
            if (Math.Abs(transform.rotation.y - Quaternion.Euler(0, angle, 0).y) > 0.01)
            {
                if (Mathf.Sign(transform.rotation.y) != Mathf.Sign(Quaternion.Euler(0, angle, 0).y))
                {
                    if (Math.Abs(Math.Abs(transform.rotation.y) - Math.Abs(Quaternion.Euler(0, angle, 0).y)) > 0.01)
                    {
                        shipRotation += 0.001f;
                        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, angle, 0), shipRotation);
                    }
                    else
                    {
                        this.transform.position += agentDesiredVelocity;
                        shipRotation = 0f;
                    }
                }
                else
                {
                    shipRotation += 0.001f;
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, angle, 0), shipRotation);
                }
            }
            else
            {
                this.transform.position += agentDesiredVelocity;
                shipRotation = 0f;
            }
        }
    }
}
