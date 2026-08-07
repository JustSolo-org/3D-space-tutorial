using System;
using UnityEngine;

public class ShipEngine : MonoBehaviour
{

    [SerializeField] GameObject _thruster;

    IMovementControls _shipMovementControls;
    Rigidbody _rigidbody;
    float _thrustForce;
    float _thrustAmount = 0f;

    bool ThrusterEnable => !Mathf.Approximately(0f, _shipMovementControls.ThrustAmount);

    // Update is called once per frame
    void Update()
    {
        ActivateThrusters();
    }

    private void FixedUpdate()
    {
        if (!ThrusterEnable) { return; }

        _rigidbody.AddForce(transform.forward * (_thrustForce * Time.fixedDeltaTime));
    }
        

    void ActivateThrusters()
    {
        _thruster.SetActive(ThrusterEnable);
        if (!ThrusterEnable) return;
        _thrustAmount = _thrustForce * _shipMovementControls.ThrustAmount;

    }

    public void init (IMovementControls movementControls, Rigidbody rb,float thrustForce)
    {
        _rigidbody = rb;
        _thrustForce= thrustForce;
        _shipMovementControls = movementControls;
    }
}
