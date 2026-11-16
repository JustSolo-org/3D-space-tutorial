using UnityEngine;

public class ShipInputControls : MonoBehaviour
{
    [SerializeField] private ShipInputManager.InputType _inputType = ShipInputManager.InputType.HumanDesktop;

    public IMovementControls MovementControls { get; private set; }
    public IWeaponControls WeaponControls { get; private set; }

    private void Awake()
    {
        MovementControls = ShipInputManager.GetMovementControls(_inputType);
        WeaponControls = ShipInputManager.GetWeaponControls(_inputType);
    }

    private void Start()
    {
        if (MovementControls == null)
            MovementControls = ShipInputManager.GetMovementControls(_inputType);
        if (WeaponControls == null)
            WeaponControls = ShipInputManager.GetWeaponControls(_inputType);
    }

    private void OnDestroy()
    {
        MovementControls = null;
        WeaponControls = null;
    }
}
