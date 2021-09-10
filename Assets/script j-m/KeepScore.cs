using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeepScore : MonoBehaviour
{
    public static int Score = 500;
    public TextMeshProUGUI scoreText;

    public void ChangeScore(int minus)
    {
        Score -= minus;
        scoreText.text = "Score : " + Score;
        //if (Score<0)

    }

    void OnGUI()
    {
        GUI.Box(new Rect(100, 100, 100, 100), Score.ToString());
    }
}
