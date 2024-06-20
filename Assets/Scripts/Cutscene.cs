using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Cutscene : MonoBehaviour
{
	public Image bgImage;
	public Image cutsceneImage;
	public TextMeshProUGUI extraText;// Reference to the UI Image component
	public TextMeshProUGUI cutsceneText; // Reference to the TextMeshProUGUI component
	public Sprite[] images; // Array of images to display
	public TextGroup[] textGroups; // Array of TextGroups to display texts for each image
	public string[] extraTexts;
	float imageDisplayTime = 60.0f; // Time each image is displayed
	float textDisplayTime = 50.0f; // Time each image is displayed

	private int currentImageIndex = 0;
	private int currentTextIndex = 0;
	private int currentExtraTextIndex = 0;
	private float imagetimer;
	private float textTimer;
	private bool isDisplayingText = false;
	private bool displayingExtraText = false;
	private bool skippable;

	private Animator animatorBg;
	private Animator animatorImage;
	private Animator transition;

	LevelLoader levelLoader;
	public int nextLevel;
	AudioManager audioManager;

	void Start()
	{
		audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
		skippable = false;
		transition = GameObject.FindGameObjectWithTag("Transition").GetComponent<Animator>();
		levelLoader = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>();
		animatorBg = bgImage.GetComponent<Animator>();
		animatorImage = cutsceneImage.GetComponent<Animator>();

		if (images.Length > 0 && textGroups.Length > 0)
		{
			ShowImage();
			ShowText();
		}
		else
		{
			Debug.LogError("No images or texts assigned.");
		}
	}

	void Update()
	{
		imagetimer += Time.deltaTime;

		// Check if it's time to change the image
		if (imagetimer >= imageDisplayTime && !isDisplayingText && !displayingExtraText)
		{
			NextImage();
		}

		
		if (skippable)// Check for player input to change the text
		{
			if (Input.anyKeyDown)
			{
				audioManager.PlaySFX(audioManager.btnClick);
				if (isDisplayingText)
				{
					NextText();
				}
				else if (displayingExtraText)
				{
					NextExtraText();
				}
			}
		}

		if (isDisplayingText)
		{
			textTimer += Time.deltaTime;
			if (textTimer >= textDisplayTime)
			{
				NextText();
			}
		}
		else if (displayingExtraText)
		{
			textTimer += Time.deltaTime;
			if (textTimer >= textDisplayTime)
			{
				NextExtraText();
			}
		}

		Transition();
	}

	void ShowImage()
	{
		TriggerAnimations();
		cutsceneImage.sprite = images[currentImageIndex];
		currentTextIndex = 0;
		imagetimer = 0f;// Reset text index for the new image
		ShowText();
	}

	void NextImage()
	{
		currentImageIndex++;
		if (currentImageIndex < images.Length)
		{
			ShowImage();
			imagetimer = 0f; // Reset the timer for the new image
		}
		else
		{
			bgImage.enabled = false;
			cutsceneImage.enabled = false;
			cutsceneText.enabled = false;
			if (extraTexts.Length > 0)
			{
				extraText.enabled = true;
				ShowExtraText();
			}
			else
			{
				EndCutscene();
			}
		}
	}

	void ShowText()
	{
		if (currentImageIndex < textGroups.Length && currentTextIndex < textGroups[currentImageIndex].texts.Length)
		{
			cutsceneText.text = textGroups[currentImageIndex].texts[currentTextIndex];
			isDisplayingText = true;
			textTimer = 0f; // Reset the text display timer
		}
		else
		{
			isDisplayingText = false;
		}
	}

	void NextText()
	{
		if (currentImageIndex < textGroups.Length)
		{
			currentTextIndex++;
			if (currentTextIndex < textGroups[currentImageIndex].texts.Length)
			{
				ShowText();
			}
			else
			{
				isDisplayingText = false;
				NextImage();
			}
		}
	}

	void ShowExtraText()
	{
		if (currentExtraTextIndex < extraTexts.Length)
		{
			extraText.text = extraTexts[currentExtraTextIndex];
			displayingExtraText = true;
			textTimer = 0f; // Reset the text display timer
		}
		else
		{
			EndCutscene();
		}
	}

	void NextExtraText()
	{
		currentExtraTextIndex++;
		if (currentExtraTextIndex < extraTexts.Length)
		{
			ShowExtraText();
		}
		else
		{
			EndCutscene();
		}
	}

	void EndCutscene()
	{
		// Perform any actions needed when the cutscene ends, like loading a new scene
		levelLoader.LoadNextLevel(nextLevel);
		Debug.Log("Cutscene Ended.");
	}

	void TriggerAnimations()
	{
		// Trigger animations in the animators
		animatorBg.SetTrigger("AnimateBackground");
		animatorImage.SetTrigger("AnimateImage");
	}

	void Transition()
	{
		AnimatorStateInfo stateInfo = transition.GetCurrentAnimatorStateInfo(0);

		if (stateInfo.normalizedTime >= 1.0f)
		{
			skippable = true;
		}
	}
}

[Serializable]
public class TextGroup
{
	public string[] texts;
}
