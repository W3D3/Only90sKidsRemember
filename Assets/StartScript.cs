using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    public GamepadInput Input;
    public string SceneName;

    public GameObject JumpImage;
    public GameObject LogoImage;

    public float Speed = 0.5f;

    public bool ZoomIn;
    public bool ZoomOut;

    public bool RotateLeft;
    public bool RotateRight;

    public float RotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        Input = GetComponent<GamepadInput>();
        ZoomIn = true;
        ZoomOut = false;
        RotateLeft = true;
        RotateRight = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.IsJumpPressed())
        {
            SceneManager.LoadScene(SceneName);
        }

        if (ZoomIn)
        {
            JumpImage.transform.localScale = Vector3.MoveTowards(JumpImage.transform.localScale, new Vector3(0.9f, 0.9f, 0.9f), Time.deltaTime * Speed);
        }

        if (ZoomOut)
        {
            JumpImage.transform.localScale = Vector3.MoveTowards(JumpImage.transform.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * Speed);
        }

        if (JumpImage.transform.localScale.x <= 0.98f && JumpImage.transform.localScale.y <= 0.9f)
        {
            ZoomOut = true;
            ZoomIn = false;
        }

        if (JumpImage.transform.localScale.x >= 1f && JumpImage.transform.localScale.y >= 1f)
        {
            ZoomOut = false;
            ZoomIn = true;
        }




        if (RotateLeft)
        {
            LogoImage.transform.rotation = Quaternion.RotateTowards(LogoImage.transform.rotation, Quaternion.Euler(0f, 0f, -7f), Time.deltaTime * RotationSpeed);
        }

        if (RotateRight)
        {
            LogoImage.transform.rotation = Quaternion.RotateTowards(LogoImage.transform.rotation, Quaternion.Euler(0f, 0f, 7f), Time.deltaTime * RotationSpeed);
        }

        // todo: why this number?
        if (LogoImage.transform.rotation.z <= -0.061f)
        {
            RotateRight = true;
            RotateLeft = false;
        }

        if (LogoImage.transform.rotation.z >= 0.061f)
        {
            RotateRight = false;
            RotateLeft = true;
        }
    }

    IEnumerator ZoomOutEnumerator()
    {
        float duration = 5f;
        var actualScale = JumpImage.transform.localScale;

        var targetScale = new Vector3(1f, 1f, 1f);

        for (var t = 0f; t < 1; t += Time.deltaTime / duration)
        {
            JumpImage.transform.localScale = Vector3.Lerp(actualScale, targetScale, t);
            yield return null;
        }

        ZoomOut = false;
        ZoomIn = true;
    }
}
