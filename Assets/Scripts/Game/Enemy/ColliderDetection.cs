using Unity.VisualScripting;
using UnityEngine;

namespace Game.Enemy
{
    public class ColliderDetection : MonoBehaviour
    {
        public delegate void TargetDetectionEvent(GameObject target, bool isDetected);
        public event TargetDetectionEvent OnTargetDetected;
        
        public string targetTag;

        private void OnTriggerEnter(Collider target)
        {
            if (target.gameObject.CompareTag(targetTag))
            {
                OnTargetDetected?.Invoke(target.gameObject, true);
            }
        }
        
        private void OnTriggerExit(Collider target)
        {
            if (target.gameObject.CompareTag("Player"))
            {
                OnTargetDetected?.Invoke(target.gameObject, false);
            }
        }
    }
}
