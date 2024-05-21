using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
	public TextMeshProUGUI transitionText;
	public Animator transition;
	public int levelIndex;

	public string animationText;
	public float transitionTime = 1f;

	private void Start()
	{
		transitionText.text = animationText;
	}

	//private void Update()
	//{
	//	if (Input.GetMouseButton(0))
	//	{
	//		LoadNextLevel(levelIndex);
	//	}
	//}

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
