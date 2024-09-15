using System.Collections;
using System.Collections.Generic;
using RyanNielson.InputBinder;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private InputBinder _input;
    
    private bool _isPaused;

    private void Start()
    {
        _input.BindKey(KeyCode.P, InputEvent.Pressed, CheckPause);
    }
    void CheckPause()
    {
        if (_isPaused)
        {
            Resume();
        } else
        {
            Pause();
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
