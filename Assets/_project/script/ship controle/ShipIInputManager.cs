using System;
using UnityEngine;

public class ShipIInputManager : MonoBehaviour
{   
    public enum InputType
    {
        HumanDesktop,
        HumanMobile,
        Bot
    }

    public static IMovementControls GetIMovementControls(InputType inputType)
    {
        return inputType switch
        {
            InputType.HumanDesktop => new DesktopMovementControl(),
            InputType.HumanMobile => null,
            InputType.Bot => null,

            _ => throw new ArgumentOutOfRangeException(nameof(inputType), inputType, null)


        };
        
    }


}
