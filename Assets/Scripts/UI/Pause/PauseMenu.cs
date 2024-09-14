using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    private bool _isPaused;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (_isPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        _panel.SetActive(false);
        Time.timeScale = 1f;
        _isPaused = false;
    }

    private void Pause()
    {
        _panel.SetActive(true);
        Time.timeScale = 0f;
        _isPaused = true;
    }
}
