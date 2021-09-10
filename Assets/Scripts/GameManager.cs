using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool gameEnded;
    private void Start()
    {
        Time.timeScale = 1f;
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void BackToMenu(int _sceneMenuIndex)
    {
        SceneManager.LoadScene(_sceneMenuIndex);
    }
}
