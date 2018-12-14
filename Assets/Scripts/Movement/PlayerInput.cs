using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Player))]
public class PlayerInput : MonoBehaviour {

	Player player;
    GamepadInput gamepadInput;

	void Start () {
		player = GetComponent<Player> ();
        gamepadInput = GetComponent<GamepadInput>();
	}

	void Update () {
		Vector2 directionalInput = new Vector2 (gamepadInput.LeftHorizontalValue(), 0);
		player.SetDirectionalInput (directionalInput);

		if (gamepadInput.Jump()) {
			player.OnJumpInputDown ();
		}
		if (Input.GetKeyUp (KeyCode.Space)) {
			player.OnJumpInputUp ();
		}
	}
}
