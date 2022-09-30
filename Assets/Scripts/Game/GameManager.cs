using System;
using System.Collections;
using System.Collections.Generic;
using Ship;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game
{
    private static GameManager _instance = null;
    public GameObject PlayerShipPrefab;
    public Camera CinematicCamera;
    public GameObject EnterMenu;
    public GameObject PauseMenu;
    public GameObject GameOverMenu;

    private bool _pauseStatus = false;
    private bool _gameIsOver = false;

    public delegate void GamePause(bool status);

    public event GamePause ToggleGamePause;

    public static GameManager Instance
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

            return _instance;
        }

        private set { _instance = value; }
    }

    private void Awake()
    {
        if (InputController.Instance)
            InputController.Instance.OnUserInput += UserInput;
    }

    // Start is called before the first frame update
    void Start()
    {
        CinematicCamera.enabled = true;
    }

    public void StartGame()
    {
        if (PlayerShipPrefab)
            GameObject.Instantiate(PlayerShipPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        EnterMenu.SetActive(false);
        StartCoroutine(startGameIntro());
    }

    public void GameOver()
    {
        ToggleGamePause?.Invoke(true);
        GameOverMenu.SetActive(true);
        _gameIsOver = true;
    }

    IEnumerator startGameIntro()
    {
        float timeElapsed = 0;
        float duration = 2f;
        var playerCamera = GameObject.FindWithTag("MainCamera");
        var basePosition = CinematicCamera.transform.position;
        var baseRotation = CinematicCamera.transform.rotation;


        while (timeElapsed <= duration)
        {
            CinematicCamera.transform.position =
                Vector3.Lerp(basePosition, playerCamera.transform.position,
                    timeElapsed / duration);
            CinematicCamera.transform.rotation = Quaternion.Lerp(baseRotation, playerCamera.transform.rotation,
                timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            if (timeElapsed >= duration)
            {
                CinematicCamera.enabled = false;
                ToggleGamePause?.Invoke(false);
            }

            yield return null;
        }
    }

    private void UserInput(string useraction)
    {
        // Disable pause / unpause if game is end
        if (_gameIsOver)
            return;
        // Toggle pause
        if (useraction == "Pause")
        {
            _pauseStatus = !_pauseStatus;
            ToggleGamePause?.Invoke(_pauseStatus);
            PauseMenu.SetActive(_pauseStatus);
        }
    }
}