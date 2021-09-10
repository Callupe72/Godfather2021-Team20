using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("J-M Menu");
        }
    }
    public void backFromCredits()
    {
        SceneManager.LoadScene("J-M Menu");
    }
}

