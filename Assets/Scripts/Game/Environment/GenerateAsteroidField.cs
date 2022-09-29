using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Environment
{
    public class GenerateAsteroidField : MonoBehaviour
    {

        public Transform asteroidPrefab;
        public int fieldRadius = 100;
        public int asteroidCount = 500;
        
        
        // Start is called before the first frame update
        void Start()
        {
            for (int loop = 0; loop < asteroidCount; loop++)
            {
                Instantiate(asteroidPrefab, Random.insideUnitSphere * fieldRadius, Quaternion.identity);
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}