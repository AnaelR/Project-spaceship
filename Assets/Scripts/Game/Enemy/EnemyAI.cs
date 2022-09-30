using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Game.Enemy
{
    public class EnemyAI : MonoBehaviour
    {
        //Navigation Agent
        public UnityEngine.AI.NavMeshAgent agent;
        public EnemyController enemy;

        public ColliderDetection radar;
        public enum State
        {
            PATROL,
            CHASE
        }

        public State state;
        private bool _alive = true;

        //Patrolling variables
        public GameObject[] waypoints;
        public int waypointInd = 0;
        public float patrolSpeed = 0.5f;
        
        //Chasing variables
        public float chaseSpeed = 1f;
        public GameObject target;

        /// <summary>
        /// The Start function is called when the game starts. It gets the NavMeshAgent component from the game object, sets
        /// the updatePosition and updateRotation to true and false respectively, sets the state to PATROL, sets the _alive
        /// variable to true, and subscribes to the OnTargetDetected event
        /// </summary>
        private void Start()
        {
            agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();

            agent.updatePosition = true;
            agent.updateRotation = false;

            //Must be uncomment to activate random patrol
            // waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
            // waypointInd = Random.Range(0, waypoints.Length);

            state = EnemyAI.State.PATROL;

            _alive = true;

            this.radar.OnTargetDetected += this.Target;

            StartCoroutine("Fsm");
        }

        /// <summary>
        /// While the enemy is alive, switch between the two states, and yield null
        /// </summary>
        IEnumerator Fsm()
        {
            while (_alive)
            {
                switch (state)
                {
                    case State.PATROL:
                        Patrol();
                        break;
                    case State.CHASE:
                        Chase();
                        break;
                }

                yield return null;
            }
        }

        /// <summary>
        /// If the enemy is far away from the waypoint, move towards it. If the enemy is close to the waypoint, move to the
        /// next waypoint
        /// </summary>
        private void Patrol()
        {
            agent.speed = patrolSpeed;
            if (Vector3.Distance(enemy.transform.position, waypoints[waypointInd].transform.position) > 15)
            {
                agent.SetDestination(waypoints[waypointInd].transform.position);
                enemy.Move (agent.desiredVelocity, agent.destination);
            }
            else if (Vector3.Distance(enemy.transform.position, waypoints[waypointInd].transform.position) <= 15)
            {
                 waypointInd += 1;
                if (waypointInd >= waypoints.Length)
                {
                    waypointInd = 0;
                }
                
                //Must be uncomment to activate random patrol
                //waypointInd = Random.Range(0, waypoints.Length);
                
            }
            else
            {
                enemy.Move (Vector3.zero, agent.destination);
            }
        }

        /// <summary>
        /// If the enemy is detected, set the target to the new target and set the state to chase. If the enemy is not
        /// detected, set the state to patrol
        /// </summary>
        /// <param name="newTarget">The game object that the enemy is targeting.</param>
        /// <param name="isDetected">This is a boolean that is true if the player is detected by the enemy.</param>
        private void Target( GameObject newTarget, bool isDetected)
        {
            if (isDetected)
            {
                target = newTarget;
                state = EnemyAI.State.CHASE;
            }
            else
            {
                state = EnemyAI.State.PATROL;
            }
        }
        
        /// <summary>
        /// If the distance between the enemy and the player is greater than 100, then the enemy will chase the player
        /// </summary>
        private void Chase()
        {
            if (Vector3.Distance(enemy.transform.position, agent.destination) > 70)
            {
                agent.speed = chaseSpeed;
                agent.SetDestination(target.transform.position);
                enemy.Move (agent.desiredVelocity, agent.destination);
            }
            else
            {
                enemy.Move (Vector3.zero, agent.destination);
            }
        }
    }
}
