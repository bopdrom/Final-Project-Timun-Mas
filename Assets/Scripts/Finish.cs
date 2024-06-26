using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
	public LevelLoader levelLoader;
	public int levelIndex;
	public bool lastLevel;
	AudioManager audioManager;

	private void Start()
	{
		audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Player")
		{
			audioManager.PlaySFX(audioManager.finish);
			UnlockNewLevel();
			levelLoader.LoadNextLevel(levelIndex);
		}
	}

	void UnlockNewLevel()
	{
		if (!lastLevel)
		{
			if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
			{
				PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
				PlayerPrefs.SetInt("levelsUnlocked", PlayerPrefs.GetInt("levelsUnlocked", 1) + 1);
				PlayerPrefs.Save();
			}
		}
		else
		{
			return;
		}
	}
}
