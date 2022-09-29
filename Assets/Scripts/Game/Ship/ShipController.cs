using System;
using UnityEngine;

namespace Ship
{
    public class ShipController : MonoBehaviour
    {
        public GameObject ship;
        public GameObject RightReactor;
        public GameObject LeftReactor;
        
        public float speed = 10f;

        private void Update()
        {
            // Move the ship
            ship.transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }
}
