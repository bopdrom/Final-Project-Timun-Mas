using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectLevel : MonoBehaviour
{
	int levelsUnlocked;

	public Button[] buttons;

	private void Start()
	{

	}

	private void Awake()
	{
		Debug.Log("Level " + PlayerPrefs.GetInt("levelsUnlocked"));
		levelsUnlocked = PlayerPrefs.GetInt("levelsUnlocked", 1);

		for (int i = 0; i < buttons.Length; i++)
		{
			buttons[i].interactable = false;
		}

		for (int i = 0; i < levelsUnlocked; i++)
		{
			buttons[i].interactable = true;
		}
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			SceneManager.LoadScene("MainMenu");
		}
	}

	public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }
}