using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    float timeRemaining = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeRemaining -= Time.deltaTime;
                
    }
    private void OnGUI()
    {
        if(timeRemaining > 0)
        {
            GUI.Label(new Rect(580, 10, 200, 100), "TEMPS RESTANT : " + (int)timeRemaining);
        }
        else
        {
            GUI.Label(new Rect(610, 10, 100, 100), "GAGNER");
        }
    }
}
