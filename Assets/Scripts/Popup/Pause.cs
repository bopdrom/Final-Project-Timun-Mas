using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject objectToToggle;
    public string level = "";

    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            pauseMenu();
        }else if(Input.GetKeyDown(KeyCode.R)){
            SceneManager.LoadScene(level);
        }
    }

    public void pauseMenu(){
		objectToToggle.SetActive(true);
		Time.timeScale = 0f;
	}

	public void Resume(){
		objectToToggle.SetActive(false);
		Time.timeScale = 1f;
	}

    public void restart(){
        Time.timeScale = 1f;
        SceneManager.LoadScene(level);
    }
}
