using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeepScore : MonoBehaviour
{
    public static int Score = 500;
    public TextMeshProUGUI scoreText;

    public GameObject gameOver;
    public Text text;
    public Timer timer;

    void Start()
    {
        Score = 500;
        scoreText.text = "Score : " + Score;
    }

    public void ChangeScore(int minus)
    {
        Score -= minus;
        scoreText.text = "Score : " + Score;
        if(Score <= 0)
        {
            gameOver.SetActive(true);
            text.text = timer.minutes + ":" + timer.seconds;
            Time.timeScale = 0;
            GameManager.gameEnded = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

}
