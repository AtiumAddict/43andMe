using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelUI : Level
{
    public GameObject pauseMenu;

    void Start ()
    {
        pauseMenu.SetActive(false);
    }

	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    public void TogglePauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        if (pauseMenu.activeSelf)
        {
            playing = false;
        }

        else
        {
            playing = true;
        }
    }
}
