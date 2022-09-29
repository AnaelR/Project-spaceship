using System.Collections;
using System.Collections.Generic;
using Ship;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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

    // Start is called before the first frame update
    void Start()
    {
    }
}