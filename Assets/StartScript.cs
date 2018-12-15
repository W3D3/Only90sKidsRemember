using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    public GamepadInput Input;
    public string SceneName;

    // Start is called before the first frame update
    void Start()
    {
        Input = GetComponent<GamepadInput>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.IsJumpPressed())
        {
            SceneManager.LoadScene(SceneName);
        }
    }
}
