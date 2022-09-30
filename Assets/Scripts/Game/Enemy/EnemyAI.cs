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
        private bool alive = true;
        private bool pause = false;
        
        //Patrolling variables
        public GameObject[] waypoints;
        public int waypointInd = 0;
        public float patrolSpeed = 0.5f;
        
        //Chasing variables
        public float chaseSpeed = 1f;
        public GameObject target;
        
        private static EnemyAI _instance = null;
        public static EnemyAI Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<EnemyAI>();
                }
                return _instance;
            }
        
            private set { _instance = value; }
        }


        private void Start()
        {
            agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();

            agent.updatePosition = true;
            agent.updateRotation = false;

            // waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
            // waypointInd = Random.Range(0, waypoints.Length);

            state = EnemyAI.State.PATROL;

            alive = true;
            pause = true;
            
            this.radar.OnTargetDetected += this.Target;

            StartCoroutine("Fsm");
        }

        IEnumerator Fsm()
        {
            while (alive)
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
                //waypointInd = Random.Range(0, waypoints.Length);
                
            }
            else
            {
                enemy.Move (Vector3.zero, agent.destination);
            }
        }

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
        
        private void Chase()
        {
            if (Vector3.Distance(enemy.transform.position, agent.destination) > 100)
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
