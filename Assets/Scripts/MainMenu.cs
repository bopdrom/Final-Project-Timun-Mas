using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	public static bool inSettings = false;
	public GameObject settingsMenuUI;
	public GameObject mainMenuUI;

	public Button startButton;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (inSettings)
			{
				BackSettings();
			}
		}
	}
	private void OnEnable()
	{
		startButton.Select();
	}

	public void OpenSettings()
	{
		mainMenuUI.SetActive(false);
		settingsMenuUI.SetActive(true);
		inSettings = true;
	}

	public void BackSettings()
	{
		mainMenuUI.SetActive(true);
		settingsMenuUI.SetActive(false);
		inSettings = false;
	}

	public void StartGame()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene("SelectLevel");
		//StartCoroutine(LevelLoader.LoadLevel(2));
	}

	public void CreditsScene()
	{
		//StartCoroutine(LevelLoader.LoadLevel(4));
		SceneManager.LoadScene("Credits");
	}
	public void QuitGame()
	{
		Application.Quit();
	}
}
