using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    float timeRemaining = 3;
    public float minutes;
    public float seconds;
    public TextMeshProUGUI timerText;
    public string currentText = "Time Remaining :";
    public string winningText = "You Won !";
    bool timerPlay = true;

    public GameObject victoryScreen;
    public Text textVictory;
    public KeepScore score;

    void Update()
    {
        if (!timerPlay)
            return;

        seconds -= Time.deltaTime;
        if (seconds >= 10)
            timerText.text = currentText + " 0" + (int)minutes + " : " + (int)seconds;
        else
            timerText.text = currentText + " 0" + (int)minutes + " : 0" + (int)seconds;
        if (seconds < 0)
        {
            seconds = 60;
            minutes--;
        }
        if (minutes < 0)
        {
            timerText.text = winningText;
            timerPlay = false;

            victoryScreen.SetActive(true);
            textVictory.text = KeepScore.Score + " pts";

            return;
        }
    }
}
