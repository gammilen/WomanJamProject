using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private bool _started;
    public void StartGame()
    {
        if (!_started)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            _started = true;
        }
    }
}
