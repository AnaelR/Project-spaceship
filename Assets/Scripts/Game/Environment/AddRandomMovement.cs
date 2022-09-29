using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Environment
{
    public class AddRandomMovement : MonoBehaviour
    {
        public float minSpinSpeed = 1f;
        public float maxSpinSpeed = 5f;
        public float minThrust = 0.1f;
        public float maxThrust = 0.5f;
        private float spinSpeed;


        private void Start()
        {
            spinSpeed = Random.Range(minSpinSpeed, maxSpinSpeed);
            float thrust = Random.Range(minThrust, maxThrust);

            Rigidbody rg = GetComponent<Rigidbody>();
            rg.AddForce(transform.forward * thrust, ForceMode.Impulse);
        }

        private void Update()
        {
            transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
        }
    }
}
