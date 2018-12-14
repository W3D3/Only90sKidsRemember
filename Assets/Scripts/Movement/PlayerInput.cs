using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Player))]
public class PlayerInput : MonoBehaviour
{

    Player player;
    GamepadInput gamepadInput;

    void Start()
    {
        player = GetComponent<Player>();
        gamepadInput = GetComponent<GamepadInput>();
    }

    void Update()
    {
        Vector2 directionalInput = new Vector2(gamepadInput.GetLeftHorizontalValue(), 0);
        player.SetDirectionalInput(directionalInput);

        if (gamepadInput.JumpPressed())
        {
            player.OnJumpInputDown();
        }
        if (gamepadInput.JumpReleased())
        {
            player.OnJumpInputUp();
        }
    }
}
