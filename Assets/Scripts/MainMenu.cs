using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	public AudioMixer mainMixer;
	public AudioManager audioManager;

	public static bool inSettings = false;
	public static bool inLevelSelect = false;
	public static bool inCredits = false;
	public static bool inOtherPanel = false;

	public GameObject settingsMenuUI;
	public GameObject mainMenuUI;
	public GameObject levelSelectUI;
	public GameObject creditsUI;

	[SerializeField] private Slider masterSlider;
	[SerializeField] private Slider musicSlider;
	[SerializeField] private Slider SFXSlider;

	public Button startButton;

	private void OnEnable()
	{
		//startButton.Select();
	}

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
			if (inOtherPanel)
			{
				BackPanel();
			}
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

	//Level Select Panel//
	public void OpenLevelSelect()
	{
		mainMenuUI.SetActive(false);
		levelSelectUI.SetActive(true);
		inLevelSelect = true;
		inOtherPanel = true;
	}

	public void BackLevelSelect()
	{
		mainMenuUI.SetActive(true);
		levelSelectUI.SetActive(false);
		inLevelSelect = false;
	}
	//Settings Panel//
	public void OpenSettings()
	{
		mainMenuUI.SetActive(false);
		settingsMenuUI.SetActive(true);
		inSettings = true;
		inOtherPanel = true;
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
	}

	//Credits Panel//
	public void OpenCredits()
	{
		mainMenuUI.SetActive(false);
		creditsUI.SetActive(true);
		inCredits = true;
		inOtherPanel = true;
	}

	public void BackCredits()
	{
		mainMenuUI.SetActive(true);
		creditsUI.SetActive(false);
		inCredits = false;
	}

	public void BackPanel()
	{
		mainMenuUI.SetActive(true);
		levelSelectUI.SetActive(false);
		settingsMenuUI.SetActive(false);
		creditsUI.SetActive(false);
		inOtherPanel = false;
	}

	public void CreditsScene()
	{
		SceneManager.LoadScene("Credits");
	}
	public void QuitGame()
	{
		Application.Quit();
	}
}
