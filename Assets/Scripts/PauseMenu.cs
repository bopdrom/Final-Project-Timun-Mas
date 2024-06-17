using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
	public AudioMixer mainMixer;
	public AudioManager audioManager;

	public static bool isPaused = false;
    public static bool inSettings = false;

    public GameObject pauseMenuUI;
    public GameObject pauseButton;
	public GameObject settingsMenuUI;
    public GameObject ornament;

	[SerializeField] private Slider masterSlider;
	[SerializeField] private Slider musicSlider;
	[SerializeField] private Slider SFXSlider;

	private void Start()
	{
		if (PlayerPrefs.HasKey("masterVolume"))
		{
			LoadVolume();
		}
		else
		{
			SetMasterVolume();
			SetMusicVolume();
			SetSFXVolume();
		}
	}
	private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                Pause();
            }
            else if (isPaused && inSettings) 
            {
                BackSettings();
            }
            else if (isPaused && !inSettings)
            {
                Resume();
            }
        }
		if (Input.GetKeyDown(KeyCode.R))
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
    }

	public void SetMasterVolume()
	{
		float volume = masterSlider.value;
		mainMixer.SetFloat("master", Mathf.Log10(volume) * 20);
		PlayerPrefs.SetFloat("masterVolume", volume);
	}

	public void SetMusicVolume()
	{
		float volume = musicSlider.value;
		mainMixer.SetFloat("music", Mathf.Log10(volume) * 20);
		PlayerPrefs.SetFloat("musicVolume", volume);
	}

	public void SetSFXVolume()
	{
		float volume = SFXSlider.value;
		mainMixer.SetFloat("sfx", Mathf.Log10(volume) * 20);
		PlayerPrefs.SetFloat("SFXVolume", volume);
	}

	private void LoadVolume()
	{
		masterSlider.value = PlayerPrefs.GetFloat("masterVolume");
		musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
		SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");

		SetMasterVolume();
		SetMusicVolume();
		SetSFXVolume();
	}

	//UI SFX
	public void HoverSound()
	{
		audioManager.PlaySFX(audioManager.btnHover);
	}

	public void ClickSound()
	{
		audioManager.PlaySFX(audioManager.btnClick);
	}

	public void Pause()
	{
		pauseMenuUI.SetActive(true);
        pauseButton.SetActive(false);
		ornament.SetActive(false);
		Time.timeScale = 0f;
		isPaused = true;
	}

	public void Resume()
    {
		pauseMenuUI.SetActive(false);
		pauseButton.SetActive(true);
		ornament.SetActive(true);
		Time.timeScale = 1f;
		isPaused = false;
	}

    public void OpenSettings()
    {
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(true);
        inSettings = true;
    }

	public void BackSettings()
	{
		pauseMenuUI.SetActive(true);
		settingsMenuUI.SetActive(false);
		inSettings = false;
	}

	public void Quit()
	{
		SceneManager.LoadScene("MainMenu");
		Time.timeScale = 1f;
	}
}
