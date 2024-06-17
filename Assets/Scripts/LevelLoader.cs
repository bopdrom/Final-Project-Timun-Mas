using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
	public Animator transition;
	public int levelIndex;

	public string animationText;
	public float transitionTime = 3.5f;

	private void Start()
	{

	}

	public void LoadNextLevel(int level)
	{
		StartCoroutine(LoadLevel(level));
	}

	IEnumerator LoadLevel(int levelIndex)
	{
		transition.SetTrigger("Start");

		yield return new WaitForSeconds(transitionTime);

		SceneManager.LoadScene(levelIndex);
	}
}
