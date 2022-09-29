using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerShipController : MonoBehaviour
{
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

    private float _shipRotateDelay = 1f;
    private float _shipRotateDelayCurrent;
    private float _shipRotateDelayToEnd = 1f;


    [Tooltip("Player ship prefab")] public GameObject Ship;

    // Start is called before the first frame update
    void Start()
    {
        _centerBasedOnScreenSize = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        if (InputController.Instance)
            InputController.Instance.OnUserInput += InstanceOnOnUserInput;
    }

    // Update is called once per frame
    void Update()
    {
        ShipRotation();
        ShipSpeed();
    }

    private void ShipSpeed()
    {
        _accelerationEaseCurrent += Time.deltaTime / _accelerationEase;
        _speedCurent = Mathf.Lerp(_speedOnClic, _speedTarget, _accelerationEaseCurrent);
        transform.position += transform.forward * _speedCurent;
    }

    private void ShipRotation()
    {
        var delta = (Input.mousePosition - _centerBasedOnScreenSize) / Screen.height;
        if (delta.y > _noTurn)
        {
            transform.Rotate(-(delta.y - _noTurn) * Time.deltaTime * _timeFactor, 0, 0);
            // DelayShipRotation(-(delta.y - _noTurn * 1.3f) * Time.deltaTime * _timeFactor, 0, 0);
        }

        if (delta.y < -_noTurn)
        {
            transform.Rotate(-(delta.y + _noTurn) * Time.deltaTime * _timeFactor, 0, 0);
            // DelayShipRotation(-(delta.y + _noTurn * 1.3f) * Time.deltaTime * _timeFactor, 0, 0);
        }

        if (delta.x > _noTurn)
        {
            transform.Rotate(0, (delta.x - _noTurn) * Time.deltaTime * _timeFactor, 0);
            // DelayShipRotation(0, (delta.x - _noTurn * 1.3f) * Time.deltaTime * _timeFactor, 0);
        }

        if (delta.x < -_noTurn)
        {
            transform.Rotate(0, (delta.x + _noTurn) * Time.deltaTime * _timeFactor, 0);
            // DelayShipRotation(0, (delta.x + _noTurn * 1.3f) * Time.deltaTime * _timeFactor, 0);
        }
    }

    private void DelayShipRotation(float x, float y, float z)
    {
        Ship.transform.Rotate(-x, -y, -z);
        StartCoroutine(recenterShip());
    }

    IEnumerator recenterShip()
    {
        Debug.Log("Enter in coroutine");
        yield return new WaitForSeconds(0.1f);

        Ship.transform.Rotate(0, 0, 0);

        yield return null;
    }
    // IEnumerator recenterShip()
    // {
    //     Debug.Log("Enter in coroutine");
    //     _shipRotateDelayCurrent = 0;
    //     yield return new WaitForSeconds(0.1f);
    //     while (_shipRotateDelayCurrent <= _shipRotateDelay)
    //     {
    //         var shipRotation = Ship.transform.localRotation;
    //         Ship.transform.Rotate(Mathf.LerpAngle(shipRotation.x, 0, _shipRotateDelayCurrent / _shipRotateDelay),
    //             Mathf.LerpAngle(shipRotation.y, 0, _shipRotateDelayCurrent / _shipRotateDelay),
    //             Mathf.LerpAngle(shipRotation.z, 0, _shipRotateDelayCurrent / _shipRotateDelay));
    //         _shipRotateDelayCurrent += Time.deltaTime;
    //         Debug.Log("delayCurrent : " + _shipRotateDelayCurrent);
    //         yield return null;
    //     }
    // }

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

        Debug.Log(_speedCurent);
    }

    private void LerpSpeed()
    {
        _speedOnClic = _speedCurent;
        _accelerationEaseCurrent = 0;
        _speedTarget = new Vector3(0, 0, _speed * _speedRatio).magnitude;
    }
}