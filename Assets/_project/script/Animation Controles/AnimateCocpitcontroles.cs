
using System.Collections.Generic;
using UnityEngine;

public class AnimateCocpitcontroles : MonoBehaviour
{
    [SerializeField] Transform _joystick;

    [SerializeField] Vector3 _joysticRange = Vector3.zero;

    [SerializeField] List<Transform> _throttles;

    [SerializeField] float _throttleRange = 35f;

    [SerializeField] 
    ShipMovementInput _movementInput;

    IMovementControls _movementControls;
    // Update is called once per frame
    void Update()
    {
        if (_movementControls == null) return;
        _joystick.localRotation = Quaternion.Euler(
            _movementControls.PitchAmount * _joysticRange.x,
            _movementControls.YawAmount * _joysticRange.y,
            _movementControls.RollAmount * _joysticRange.z
            );
        Vector3 throttleRotation = _throttles[0].localRotation.eulerAngles;
        throttleRotation.x = _movementControls.ThrustAmount * _throttleRange;
        foreach ( Transform throttle in _throttles)
        {
            throttle.localRotation = Quaternion.Euler(throttleRotation);
        }

        
    }

    public void Init (IMovementControls movementControls)
    {
        _movementControls = movementControls;
    }
}
