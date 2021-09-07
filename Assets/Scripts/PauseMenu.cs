using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    bool _isInPause = false;
    public GameObject pauseMenu;

    void Start()
    {
        Time.timeScale = 1;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            Pause();
        }
    }

    public void Pause()
    {
        _isInPause = !_isInPause;

        if (_isInPause)
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
        }

        Cursor.visible = _isInPause;
        pauseMenu.SetActive(_isInPause);
    }
}