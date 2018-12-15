﻿using System.Collections;
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

    public Vector3 growScale;

    public GameObject Winner1;
    public GameObject Winner2;
    public GameObject Winner3;
    public GameObject Winner4;

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

            Winner1.SetActive(false);
            Winner2.SetActive(false);
            Winner3.SetActive(false);
            Winner4.SetActive(false);

            switch (winner.Name)
            {
                case "Player1":
                    Winner1.SetActive(true);
                    break;
                case "Player2":
                    Winner2.SetActive(true);
                    break;
                case "Player3":
                    Winner3.SetActive(true);
                    break;
                case "Player4":
                    Winner4.SetActive(true);
                    break;
            }
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