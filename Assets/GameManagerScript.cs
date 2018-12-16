using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Image = UnityEngine.Experimental.UIElements.Image;

public class GameManagerScript : MonoBehaviour
{
    public List<Player> AllPlayers;
    public string Scene;
    public GameObject GameOverScreen;
    public GameObject PreStartScreen;
    public Text PlayerWonText;
    public GamepadInput input;
    public bool GameOver;

    public bool GameNotStarted = true;

    public GameObject Image1;
    public GameObject Image2;
    public GameObject Image3;
    public GameObject Fight;

    public GameObject CurrentImage;
    public GameObject JumpImage;
    public bool ZoomIn;
    public bool ZoomOut;
    public bool RotateLeft;
    public bool RotateRight;

    public float Speed = 0.5f;
    public float RotationSpeed;
    public Vector3 growScale;

    public Text WinnerName;

    // Start is called before the first frame update
    void Start()
    {

        // todo: fix ugly code 
        input = AllPlayers.FirstOrDefault().GetComponent<GamepadInput>();
        foreach (var allPlayer in AllPlayers)
        {
            allPlayer.GetComponent<GamepadInput>().EnablePlayerControls = false;
        }
        StartCoroutine(ShowNumber());

        Image3.transform.localScale = Vector3.zero;
        Image2.transform.localScale = Vector3.zero;
        Image1.transform.localScale = Vector3.zero;
        Fight.transform.localScale = Vector3.zero;

        GameOverScreen.SetActive(false);
        PreStartScreen.SetActive(true);

        ZoomIn = true;
        RotateLeft = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var count = AllPlayers.Count;
        if (AllPlayers.Count(x => x.health > 0) == 1 && !GameOver && !GameNotStarted)
        {
            GameOver = true;

            // only 1 player alive
            GameOverScreen.SetActive(true);
            var winner = AllPlayers.First(x => x.health > 0);

            WinnerName.text = winner.Name;
        }

        if (AllPlayers.Any(x => x.GetComponent<GamepadInput>().IsJumpPressed()) && GameOver)
        {
            RestartLevel();
        }

        if (CurrentImage != null)
        {
            growScale += new Vector3(1, 1, 1) * 0.01f;
            CurrentImage.transform.localScale = Vector3.Lerp(transform.localScale, growScale, 1f);
        }

        if (GameOver)
        {
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
                WinnerName.transform.rotation = Quaternion.RotateTowards(WinnerName.transform.rotation, Quaternion.Euler(0f, 0f, -7f), Time.deltaTime * RotationSpeed);
            }

            if (RotateRight)
            {
                WinnerName.transform.rotation = Quaternion.RotateTowards(WinnerName.transform.rotation, Quaternion.Euler(0f, 0f, 7f), Time.deltaTime * RotationSpeed);
            }

            // todo: why this number?
            if (WinnerName.transform.rotation.z <= -0.061f)
            {
                RotateRight = true;
                RotateLeft = false;
            }

            if (WinnerName.transform.rotation.z >= 0.061f)
            {
                RotateRight = false;
                RotateLeft = true;
            }
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(Scene);
    }

    IEnumerator ShowNumber()
    {
        CurrentImage = Image3;
        growScale = new Vector3(0,0,0);
        Image3.SetActive(true);
        print(Time.time);
        yield return new WaitForSeconds(1);
        CurrentImage = Image2;
        growScale = new Vector3(0, 0, 0);
        Image3.SetActive(false);
        Image2.SetActive(true);
        print(Time.time);
        yield return new WaitForSeconds(1);
        CurrentImage = Image1;
        growScale = new Vector3(0, 0, 0);
        Image2.SetActive(false);
        Image1.SetActive(true);
        print(Time.time);
        yield return new WaitForSeconds(1);
        CurrentImage = Fight;
        growScale = new Vector3(0, 0, 0);
        Image1.SetActive(false);
        Fight.SetActive(true);
        print(Time.time);
        yield return new WaitForSeconds(1);
        CurrentImage = null;
        Fight.SetActive(false);
        GameNotStarted = false;

        foreach (var allPlayer in AllPlayers)
        {
            allPlayer.GetComponent<GamepadInput>().EnablePlayerControls = true;
        }
    }
}
