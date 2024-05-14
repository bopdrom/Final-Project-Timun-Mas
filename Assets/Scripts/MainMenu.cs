using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public void StartGame()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene("Level1");
	}

	public void CreditsScene()
	{
		SceneManager.LoadScene("Credits");
	}
	public void QuitGame()
	{
		Application.Quit();
	}

	public void BackCredits()
	{
		SceneManager.LoadScene("MainMenu");
	}
}
