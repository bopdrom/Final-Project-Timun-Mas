using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Cutscene : MonoBehaviour
{
	public Image bgImage;
	public Image cutsceneImage; // Reference to the UI Image component
	public TextMeshProUGUI cutsceneText; // Reference to the TextMeshProUGUI component
	public Sprite[] images; // Array of images to display
	public TextGroup[] textGroups; // Array of TextGroups to display texts for each image
	public float imageDisplayTime = 10.0f; // Time each image is displayed
	public float textDisplayTime = 7.0f; // Time each image is displayed

	private int currentImageIndex = 0;
	private int currentTextIndex = 0;
	private float imagetimer;
	private float textTimer;
	private bool isDisplayingText = false;

	private Animator animatorBg;
	private Animator animatorImage;

	LevelLoader levelLoader;
	public int nextLevel;

	void Start()
	{
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
		if (imagetimer >= imageDisplayTime && !isDisplayingText)
		{
			NextImage();
		}

		// Check for player input to change the text
		if (Input.anyKeyDown && isDisplayingText)
		{
			NextText();
		}

		if (isDisplayingText)
		{
			textTimer += Time.deltaTime;
			if (textTimer >= textDisplayTime)
			{
				NextText();
			}
		}
	}

	void ShowImage()
	{
		TriggerAnimations();
		cutsceneImage.sprite = images[currentImageIndex];
		currentTextIndex = 0; // Reset text index for the new image
		ShowText();
	}

	void ShowText()
	{
		if (currentImageIndex < textGroups.Length && currentTextIndex < textGroups[currentImageIndex].texts.Length)
		{
			cutsceneText.text = textGroups[currentImageIndex].texts[currentTextIndex];
			isDisplayingText = true;
			textTimer = 0f; // Reset the text display timer
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
}

[Serializable]
public class TextGroup
{
	public string[] texts;
}
