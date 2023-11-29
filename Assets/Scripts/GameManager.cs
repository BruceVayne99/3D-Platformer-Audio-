using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static float currentTimer;
    public float startTimer;
    public static int score;

    public Text scoreText;
    public Text timerText;
    public Text highScore;
    public Text gameOverMessage;
    public AudioSource winSound;
    public AudioSource loseSound;
    public AudioSource backgroundMusic;

    public static bool playing;


    // Start is called before the first frame update
    void Start()
    {
        playing = true;
        score = 0;
        currentTimer = startTimer;
        gameOverMessage.text = "";
        highScore.text = "Highscore: " + PlayerPrefs.GetInt("highscore",0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            PlayerPrefs.DeleteAll();
        }


        if (currentTimer > 0)
        {
            currentTimer -= Time.deltaTime;
        }else if (playing)
        {
            backgroundMusic.Stop();

            //do gameover stuff
            if(score > PlayerPrefs.GetInt("highscore", 0))
            {
                highScore.text = "Highscore: " + score;
                gameOverMessage.text = "New Highscore!";
                PlayerPrefs.SetInt("highscore", score);
                winSound.Play();

            }
            else
            {
                gameOverMessage.text = "Better luck next time!";
                loseSound.Play();
            }

            playing = false;
        }

        scoreText.text = "Score: " + score;
        timerText.text = "Time: " + Mathf.Round(currentTimer);
    }
}
