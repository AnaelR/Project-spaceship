using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        public delegate void SpeedEvent(float newSpeed); 
        public event SpeedEvent OnSpeedChange;
    
        private static GameManager _instance = null;
        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<GameManager>();
                }
                return _instance;
            }
        
            private set { _instance = value; }
        }

        [Tooltip("The speed at which the object will rotate")]
        private float _speed = 1;
        public float Speed
        {
            get => _speed;
            set
            {
                if (OnSpeedChange != null && value != _speed)
                {
                    OnSpeedChange.Invoke(value);
                }
                _speed = value;
            }
        }
        //public float speed { get; set; }
    
        private float _previousSpeed = 0.0f;

        // Start is called before the first frame update
        void Start()
        {
            if (GameManager.Instance == null)
                GameManager.Instance = this;


            // if (InputController.Instance)
            //     InputController.Instance.OnUserPause += Pause;
        }
    }
}
