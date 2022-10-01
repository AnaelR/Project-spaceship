using System;
using System.Collections;
using System.Collections.Generic;
using Game.Assets;
using Game.Enemy;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;
    public GameObject PlayerShipPrefab;
    public Camera CinematicCamera;
    public GameObject EnterMenu;
    public GameObject PauseMenu;
    public GameObject WinMenu;
    public GameObject LooseMenu;

    private bool _pauseStatus = false;
    private bool _gameIsOver = false;

    public delegate void GamePause(bool status);
    public event GamePause ToggleGamePause;

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

    /// <summary>
    /// If the InputController exists, subscribe to the OnUserInput event
    /// </summary>
    private void Awake()
    {
        if (InputController.Instance)
            InputController.Instance.OnUserInput += UserInput;
    }

    /// <summary>
    /// The Start() function is called when the script is first run. It enables the CinematicCamera
    /// </summary>
    void Start()
    {
        CinematicCamera.enabled = true;
    }

    /// <summary>
    /// It instantiates the player ship prefab, disables the enter menu, and starts the start game intro coroutine
    /// </summary>
    public void StartGame()
    {
        Debug.Log("Start");
        if (PlayerShipPrefab)
            GameObject.Instantiate(PlayerShipPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        EnterMenu.SetActive(false);
        StartCoroutine(StartGameIntro());
    }

    /// <summary>
    /// This function is called when the player dies. It pauses the game, and displays the game over menu
    /// </summary>
    public void GameOver(bool result)
    {
        ToggleGamePause?.Invoke(true);
        if (result)
        {
            WinMenu.SetActive(true);
        }
        else
        {
            LooseMenu.SetActive(true);
        }
        _gameIsOver = true;
    }

    /// <summary>
    /// We start at the cinematic camera's position and rotation, and then we move to the player's camera position and
    /// rotation over the course of 2 seconds
    /// </summary>
    IEnumerator StartGameIntro()
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

    /// <summary>
    /// If the game is not over, toggle the pause status and invoke the pause event
    /// </summary>
    /// <param name="userAction">The name of the action that was triggered.</param>
    /// <returns>
    /// The return type is void.
    /// </returns>
    private void UserInput(string userAction)
    {
        // Disable pause / unpause if game is end
        if (_gameIsOver)
            return;
        // Toggle pause
        if (userAction != "Pause") return;
        _pauseStatus = !_pauseStatus;
        ToggleGamePause?.Invoke(_pauseStatus);
        PauseMenu.SetActive(_pauseStatus);
    }
}