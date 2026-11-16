using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class ShipControl : MonoBehaviour
{
    [FormerlySerializedAs("_movementInput")]
    [InlineEditor(InlineEditorObjectFieldModes.Boxed)]
    [SerializeField]
    [Required]
    private ShipInputControls _inputControls;

    [InlineEditor(InlineEditorObjectFieldModes.Foldout)]
    [SerializeField]
    [Required]
    private ShipDataSo _shipData;

    [BoxGroup("Ship components")]
    [SerializeField]
    [Required]
    private List<ShipEngine> _engines;

    [BoxGroup("Ship components")]
    [SerializeField]
    private List<Blaster> _blasters;

    [BoxGroup("Ship components")]
    [SerializeField]
    private AnimateCocpitcontroles _cockpitControls;

    private Rigidbody _rigidBody;

    [ShowInInspector]
    [Range(-1f, 1f)]
    private float _pitchAmount, _rollAmount, _yawAmount = 0f;

    private IMovementControls MovementInput => _inputControls != null ? _inputControls.MovementControls : null;
    private IWeaponControls WeaponInput => _inputControls != null ? _inputControls.WeaponControls : null;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        if (_engines != null)
        {
            float force = _shipData != null ? _shipData.ThrustForce / _engines.Count : 7500f / _engines.Count;
            foreach (ShipEngine engine in _engines)
            {
                engine.init(MovementInput, _rigidBody, force);
            }
        }

        if (_blasters != null && _shipData != null)
        {
            foreach (Blaster blaster in _blasters)
            {
                blaster.Init(WeaponInput, _shipData.BlasterCooldown, _shipData.BlasterLaunchForce, _shipData.BlasterProjectileDuration, _shipData.BlasterDamage);
            }
        }

        if (_cockpitControls != null && MovementInput != null)
        {
            _cockpitControls.Init(MovementInput);
        }
    }

    private void Update()
    {
        if (MovementInput == null) return;
        _rollAmount = MovementInput.RollAmount;
        _yawAmount = MovementInput.YawAmount;
        _pitchAmount = MovementInput.PitchAmount;
    }

    private void FixedUpdate()
    {
        float pitchForce = _shipData != null ? _shipData.PitchForce : 6000f;
        float rollForce = _shipData != null ? _shipData.RollForce : 1000f;
        float yawForce = _shipData != null ? _shipData.YawForce : 2000f;

        if (!Mathf.Approximately(0f, _pitchAmount))
        {
            _rigidBody.AddTorque(transform.right * (pitchForce * _pitchAmount * Time.fixedDeltaTime));
        }

        if (!Mathf.Approximately(0f, _rollAmount))
        {
            _rigidBody.AddTorque(transform.forward * (rollForce * _rollAmount * Time.fixedDeltaTime));
        }

        if (!Mathf.Approximately(0f, _yawAmount))
        {
            _rigidBody.AddTorque(transform.up * (yawForce * _yawAmount * Time.fixedDeltaTime));
        }
    }
}
