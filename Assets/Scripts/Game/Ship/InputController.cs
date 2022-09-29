using UnityEngine;

namespace Ship
{
    public class InputController : MonoBehaviour
    {
        private static InputController _instance = null;
    
    
        public static InputController Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<InputController>();
                }
                return _instance;
            }
        
            private set { _instance = value; }
        }

        public delegate void InputEvent( string userAction);
        public event InputEvent OnUserInput;
        public delegate void InputSpecificEvent();
        public event InputSpecificEvent OnUserPause;
    
    
        // Start is called before the first frame update
        void Start()
        {
       
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                this.OnUserInput?.Invoke("pause");
                this.OnUserPause?.Invoke();
            }
        
        }
    }
}
