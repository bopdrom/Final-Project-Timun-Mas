using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
	public LevelLoader levelLoader;
	public int levelIndex = 1;
	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Player")
		{
			levelLoader.LoadNextLevel(levelIndex);
			//SceneManager.LoadScene("MainMenu");
			Debug.Log("Finish");
		}
	}
}
