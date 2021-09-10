using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool _isInPause = false;
    public GameObject pauseMenu;
    void Start()
    {
        Time.timeScale = 1;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && !GameManager.gameEnded)
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
