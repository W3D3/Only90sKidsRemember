using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Player))]
public class PlayerInput : MonoBehaviour
{
    private ThrowScript _throwScript;
    Player player;
    GamepadInput gamepadInput;

    void Start()
    {
        player = GetComponent<Player>();
        gamepadInput = GetComponent<GamepadInput>();
        _throwScript = GetComponent<ThrowScript>();
    }

    void Update()
    {
        Vector2 directionalInput = new Vector2(gamepadInput.GetLeftHorizontalValue(), 0);
        player.SetDirectionalInput(directionalInput);
        _throwScript.SetDirectionalInput(gamepadInput);

        if (gamepadInput.IsJumpPressed())
        {
            player.OnJumpInputDown();
        }
        if (gamepadInput.IsJumpReleased())
        {
            player.OnJumpInputUp();
        }
        
        if (gamepadInput.IsRegularFirePressed())
            Debug.Log("fire pressed");
        if(gamepadInput.IsRegularFireReleased())
            Debug.Log("fire released");
        if(gamepadInput.IsSpecialFirePressed())
            Debug.Log("special pressed");
        if (gamepadInput.IsSpecialFireReleased())
            Debug.Log("special released");

    }
}
