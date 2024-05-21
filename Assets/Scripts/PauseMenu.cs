using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public static bool inOptions = false;

    public GameObject pauseMenuUI;
    public GameObject optionsMenuUI;

	private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                Pause();
            }
            else if (isPaused && inOptions) 
            {
                OptBack();
            }
            else if (isPaused && !inOptions)
            {
                Resume();
            }
        }
    }

	void Pause()
	{
		pauseMenuUI.SetActive(true);
		Time.timeScale = 0f;
		isPaused = true;
	}

	public void Resume()
    {
		pauseMenuUI.SetActive(false);
		Time.timeScale = 1f;
		isPaused = false;
	}

    public void Options()
    {
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(true);
        inOptions = true;
    }

	public void Quit()
	{
		SceneManager.LoadScene("MainMenu");
	}

    public void OptBack()
    {
		pauseMenuUI.SetActive(true);
		optionsMenuUI.SetActive(false);
        inOptions = false;
	}
}
