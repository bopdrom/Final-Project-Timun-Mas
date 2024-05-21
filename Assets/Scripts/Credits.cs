using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			SceneManager.LoadScene("MainMenu");
		}
	}
	public void BackCredits()
	{
		//StartCoroutine(LevelLoader.LoadLevel(0));
		SceneManager.LoadScene("MainMenu");
	}
}
