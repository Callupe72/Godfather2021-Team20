using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeepScore : MonoBehaviour
{
    public static int Score = 500;
    public TextMeshProUGUI scoreText;

    void Start()
    {
        Score = 500;
        scoreText.text = "Score : " + Score;
    }

    public void ChangeScore(int minus)
    {
        Score -= minus;
        scoreText.text = "Score : " + Score;

    }

}
