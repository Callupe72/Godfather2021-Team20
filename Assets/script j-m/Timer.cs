using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    float timeRemaining = 3;
    public float minutes;
    public float seconds;
    public TextMeshProUGUI timerText;
    public string currentText = "Time Remaining :";
    public string winningText = "You Won !";
    bool timerPlay = true;
    void Update()
    {
        if (!timerPlay)
            return;


        seconds -= Time.deltaTime;
        Debug.Log(seconds);

        if (seconds > 9)
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
            return;
        }

    }
    private void OnGUI()
    {
        if (timeRemaining > 0)
        {
        }
        else
        {
        }
    }
}
