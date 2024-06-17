using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public List<ResItem> resolutions = new List<ResItem>();
	//public List<DisplayMode> displayModes = new List<DisplayMode>();
	private int selectedRes;
    private int selectedDisplay = 0;
    public TMP_Text ResLabel;
	public TMP_Text DspModeLabel;

    string[] displayModes = { "Fullscreen", "Windowed" };

	private void Start()
	{
		bool foundRes = false;
        for (int i = 0; i < resolutions.Count; i++)
        {
            if (Screen.width == resolutions[i].horizontal && Screen.height == resolutions[i].vertical)
            {
                foundRes = true;
                selectedRes = i;
                UpdateRes();
            }
        }

        if (!foundRes)
        {
            ResItem newRes = new ResItem();
            newRes.horizontal = Screen.width;
            newRes.vertical = Screen.height;

            resolutions.Add(newRes);
            selectedRes = resolutions.Count - 1;

            UpdateRes();
        }

        //bool foundDisplayModes = false;
        if (Screen.fullScreen)
        {
            //foundDisplayModes = true;
            selectedDisplay = 0;
            UpdateDisplayMode();
        }
        else if (!Screen.fullScreen)
        {
			//foundDisplayModes = false;
			selectedDisplay = 1;
			UpdateDisplayMode();
		}

	}
	public void ResLeft()
    {
        selectedRes--;
        if (selectedRes < 0)
        {
            selectedRes = 0;
        }
        UpdateRes();
	}

    public void ResRight()
    {
        selectedRes++;
        if (selectedRes > resolutions.Count - 1)
        {
            selectedRes = resolutions.Count - 1;
		}
        UpdateRes();
	}

    public void UpdateRes()
    {
        ResLabel.text = resolutions[selectedRes].horizontal.ToString() + " : " + resolutions[selectedRes].vertical.ToString();
        Screen.SetResolution(resolutions[selectedRes].horizontal, resolutions[selectedRes].vertical, Screen.fullScreen);
    }

    public void DisplayLeft()
    {
		selectedDisplay--;
		if (selectedDisplay < 0)
		{
			selectedDisplay = 0;
		}
        UpdateDisplayMode();
	}

    public void DisplayRight()
    {
		selectedDisplay++;
		if (selectedDisplay > displayModes.Length - 1)
		{
			selectedDisplay = displayModes.Length - 1;
		}
        UpdateDisplayMode();
	}

    public void UpdateDisplayMode()
    {
		DspModeLabel.text = displayModes[selectedDisplay].ToString();
        if (selectedDisplay == 0)
        {
            Screen.fullScreen = true;
        }
		if (selectedDisplay == 1)
		{
			Screen.fullScreen = false;
		}
	}
}

[System.Serializable]
public class ResItem
{
    public int horizontal, vertical;
}

[System.Serializable]
public class DisplayMode
{
	public string displayMode;
}