using UnityEngine;

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

    public delegate void InputEvent(string userAction);

    public event InputEvent OnUserInput;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Z button is press
        if (Input.GetKeyDown(KeyCode.Z))
        {
            this.OnUserInput?.Invoke("speedUp");
        }

        // S button is press
        if (Input.GetKeyDown(KeyCode.S))
        {
            this.OnUserInput?.Invoke("speedDown");
        }

        // Escape button is press
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            this.OnUserInput?.Invoke("Pause");
        }
    }
}