using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Apple;

namespace Game.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        //public UnityEngine.AI.NavMeshAgent agent;
        //Chasing variables
        public float chaseSpeed = 1f;
        public GameObject targetToChase;

        //Distance between player and enemy
        private float _distance;
        //Enemy target
        public Transform target;
        //Distance between enemy and target
        public float targetDistance = 10f;
        //Attack range 
        public float attackRange = 2f;
        //Cooldown between attacks
        public float attackCooldown = 2f;
        private float _attackTime;
        //Attack damage amount
        public float attackDamage = 1f;
        //Enemy health
        public float enemyHealth = 10f;
        private bool _isDead = false;

        public float searchAreaRadius = 100f;
        private Transform _searchAreaPosition;

        private float shipRotation = 0f;


        private void Start()
        {
            _attackTime = Time.time;
        }

        private void Update()
        {

            // if (!_isDead)
            // {
            //     target = GameObject.Find("Player").transform;
            //
            //     _distance = Vector3.Distance(target.position, transform.position);
            //
            //     //if enemy cannot see target
            //     if (_distance > targetDistance)
            //     {
            //         Patrol();
            //     }
            //
            //     //if enemy can see target but is out of range
            //     if (_distance < targetDistance && _distance > attackRange)
            //     {
            //         Chase();
            //     }
            //
            //     //if enemy is in range
            //     if (_distance < attackRange)
            //     {
            //         Attack();
            //     }
            // }
        }

        private void Chase()
        {
            //Play reactor animation
            
            //Enemy move
            //agent.destination = target.position;
            // agent.speed = chaseSpeed;
            // agent.SetDestination(target.transform.position);
            //character.Move(agent.desiredVelocity, false, false);
        }

        private void Attack()
        {

            //agent.destination = transform.position;
            
            if (Time.time > _attackTime) 
            {
                //Play weapons animation 
                
                //Fire
                //target.GetComponent<PlayInentory>().ApplyDamage(attackDamage);

                _attackTime = Time.time + attackCooldown;
            }
        }

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

        private void Die()
        {
            _isDead = true;
            
            //Animation death
            
            //Destroy
            Destroy(transform.gameObject, 5);
        }
        
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
