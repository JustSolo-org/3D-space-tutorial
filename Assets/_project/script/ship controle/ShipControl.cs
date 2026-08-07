using System.Collections.Generic;
using UnityEngine;

public class ShipControl : MonoBehaviour
{
    [SerializeField] ShipMovementInput _movementInput;
    [SerializeField][Range(1000f, 10000f)]
    float _thrustForce = 7500f,
        _pitchForce = 6000f,
        _rollForce = 1000f,
        _yawForce = 2000f;


    [SerializeField] List<ShipEngine> _engines;

    [SerializeField] AnimateCocpitcontroles _cockpitControls;


    Rigidbody _rigidBody;
    [Range(-1f,1f)]
    [SerializeField]
    float _pitchAmount, _rollAmount, _yawAmount = 0f;
    IMovementControls ControlInput => _movementInput.MovementControls;

    void Awake() 
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        foreach (ShipEngine engine in _engines)
        {
            engine.init(ControlInput, _rigidBody, _thrustForce/_engines.Count);
        }

        _cockpitControls.Init(ControlInput);
    }

    void Update()
    {
        //_thrustAmount = ControlInput.ThrustAmount;
        _rollAmount = ControlInput.RollAmount;
        _yawAmount = ControlInput.YawAmount;
        _pitchAmount = ControlInput.PitchAmount;
    }

    void FixedUpdate()
    {
        if (!Mathf.Approximately(0f, _pitchAmount))
        {
            _rigidBody.AddTorque(
                transform.right * (_pitchForce * _pitchAmount * Time.fixedDeltaTime)
            );
        }

        if (!Mathf.Approximately(0f, _rollAmount))
        {
            _rigidBody.AddTorque(
                 transform.forward * (_rollForce* _rollAmount * Time.fixedDeltaTime)
                );
        }

        if (!Mathf.Approximately(0f, _yawAmount))
        {
            _rigidBody.AddTorque(
                 transform.up * (_yawForce * _yawAmount * Time.fixedDeltaTime)
                );
        }

        
        

    }
}
