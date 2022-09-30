using System.Collections;
using UnityEngine;

public class PlayerShipController : MonoBehaviour
{
    public PlayerShipPreset preset;
    public GameObject TPSCamera;
    private Vector3 _centerBasedOnScreenSize;
    private float _timeFactor = 150f;
    private float _noTurn = 0.1f;
    private float _speed = 0;
    private float _speedRatio = 2;
    private float _speedCurent = 0;
    private float _speedTarget = 0;
    private float _speedOnClic = 0;
    private float _speedMaxValue = 10;
    private float _speedMinValue = -10;
    private float _accelerationEase = 1f;
    private float _accelerationEaseCurrent;

    private float _shipRotateDelayCurrent;

    private bool _isPaused = true;

    private void Awake()
    {
        //Use preset data
        if (preset)
        {
            _timeFactor = preset.timeFactor;
            _noTurn = preset.noTurn;
            _speedRatio = preset.speedRatio;
            _speedMaxValue = preset.speedMaxValue;
            _speedMinValue = preset.speedMinValue;
            _accelerationEase = preset.accelerationEase;
        }

        //Toggle pause on init
        TogglePause(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        _centerBasedOnScreenSize = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        if (InputController.Instance)
            InputController.Instance.OnUserInput += InstanceOnOnUserInput;

        if (GameManager.Instance)
            GameManager.Instance.ToggleGamePause += TogglePause;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isPaused)
            return;
        ShipRotation();
        ShipSpeed();
    }

    private void ShipSpeed()
    {
        _accelerationEaseCurrent += Time.deltaTime / _accelerationEase;
        _speedCurent = Mathf.Lerp(_speedOnClic, _speedTarget, _accelerationEaseCurrent);
        transform.position += transform.forward * _speedCurent;
    }

    /// <summary>
    /// Rotate ship with mouse position
    /// </summary>
    private void ShipRotation()
    {
        var delta = (Input.mousePosition - _centerBasedOnScreenSize) / Screen.height;
        if (delta.y > _noTurn)
        {
            transform.Rotate(-(delta.y - _noTurn) * Time.deltaTime * _timeFactor, 0, 0);
            DelayCameraRotation(-(delta.y - _noTurn) * Time.deltaTime * _timeFactor, 0, 0);
        }

        if (delta.y < -_noTurn)
        {
            transform.Rotate(-(delta.y + _noTurn) * Time.deltaTime * _timeFactor, 0, 0);
            DelayCameraRotation(-(delta.y + _noTurn) * Time.deltaTime * _timeFactor, 0, 0);
        }

        if (delta.x > _noTurn)
        {
            transform.Rotate(0, (delta.x - _noTurn) * Time.deltaTime * _timeFactor, 0);
            DelayCameraRotation(0, (delta.x - _noTurn) * Time.deltaTime * _timeFactor, 0);
        }

        if (delta.x < -_noTurn)
        {
            transform.Rotate(0, (delta.x + _noTurn) * Time.deltaTime * _timeFactor, 0);
            DelayCameraRotation(0, (delta.x + _noTurn) * Time.deltaTime * _timeFactor, 0);
        }
    }

    /// <summary>
    /// Delay the camera rotation when ship is moving
    /// </summary>
    private void DelayCameraRotation(float x, float y, float z)
    {
        TPSCamera.transform.Rotate(x, y, z);
        StartCoroutine(recenterCamera());
    }

    /// <summary>
    /// Recenter camera on based rotation
    /// </summary>
    IEnumerator recenterCamera()
    {
        while (TPSCamera.transform.localRotation.eulerAngles.magnitude >= 1)
        {
            var newRotation =
                Quaternion.Lerp(TPSCamera.transform.localRotation, Quaternion.identity, 0.1f * Time.deltaTime);
            TPSCamera.transform.localRotation = newRotation;
            yield return null;
        }

        TPSCamera.transform.localRotation = Quaternion.identity;
        yield return null;
    }

    /// <summary>
    /// Handle speed input from player
    /// </summary>
    /// <param name="useraction">String: Action to do</param>
    private void InstanceOnOnUserInput(string useraction)
    {
        if (useraction == "speedUp")
        {
            if (_speed <= _speedMaxValue)
            {
                _speed += 1;
                LerpSpeed();
            }
        }

        if (useraction == "speedDown")
        {
            if (_speed >= _speedMinValue)
            {
                _speed -= 1;
                LerpSpeed();
            }
        }

    }

    private void LerpSpeed()
    {
        _speedOnClic = _speedCurent;
        _accelerationEaseCurrent = 0;
        _speedTarget = new Vector3(0, 0, _speed * _speedRatio).magnitude;
    }

    /// <summary>
    /// Toggle pause status
    /// </summary>
    /// <param name="status">Bool: Pause status (True : Pause enable, False : Pause disable)</param>
    private void TogglePause(bool status)
    {
        _isPaused = status;
    }
}