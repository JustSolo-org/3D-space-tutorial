using UnityEngine;

public class ShipMovementInput : MonoBehaviour
{
    [SerializeField] ShipIInputManager.InputType _inputType = ShipIInputManager.InputType.HumanDesktop;

    public IMovementControls MovementControls {  get; private set; }


    //start is called before the first frame update

    private void Start()
    {
        MovementControls = ShipIInputManager.GetIMovementControls(_inputType);
    }


    void OnDestroy()
    {
        MovementControls = null;
    }



}
