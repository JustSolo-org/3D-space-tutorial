using System;
using UnityEngine;

public class ShipEngine : MonoBehaviour
{

    [SerializeField] GameObject _thruster;

    IMovementControls _shipMovementControls;
    Rigidbody _rigidbody;
    float _thrustForce;

    bool ThrusterEnable => !Mathf.Approximately(0f, _shipMovementControls.ThrustAmount);

    // Update is called once per frame
    void Update()
    {
        ActivateThrusters();
    }

    private void FixedUpdate()
    {
        float thrustAmount = _shipMovementControls.ThrustAmount;
        if (Mathf.Approximately(0f, thrustAmount)) { return; }

        _rigidbody.AddForce(transform.forward * (_thrustForce * thrustAmount * Time.fixedDeltaTime));
    }
        

    void ActivateThrusters()
    {
        _thruster.SetActive(ThrusterEnable);
    }

    public void init (IMovementControls movementControls, Rigidbody rb,float thrustForce)
    {
        _rigidbody = rb;
        _thrustForce= thrustForce;
        _shipMovementControls = movementControls;
    }
}
