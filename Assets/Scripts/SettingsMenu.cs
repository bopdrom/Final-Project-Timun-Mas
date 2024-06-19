using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
	List<ResItem> resolutions = new List<ResItem>();
	//List<string> options = new List<string>();
	private int selectedRes;
    private int selectedDisplay = 1;
    public TMP_Text ResLabel;
	public TMP_Text DspModeLabel;

    string[] displayModes = { "Windowed", "Fullscreen" };

    /// Tes Resolusi
    Resolution[] reso;
    /// --------

	private void Start()
	{
		/*reso = Screen.resolutions;
        
        //List<string> options = new List<string>();

        selectedRes = 0;

        for (int i = 0; i < reso.Length; i++)
        {
            string option = reso[i].width + " : " + reso[i].height;
            options.Add(option);

            if (reso[i].width == Screen.currentResolution.width &&
				reso[i].height == Screen.currentResolution.height)
            {
				selectedRes = i;
				UpdateRes(selectedRes);
            }
        }*/
		reso = Screen.resolutions;
		bool foundRes = false;
		for (int i = 0; i < reso.Length; i++)
		{
			int h = reso[i].width;
			int v = reso[i].height;
			//resolutions[i].horizontal = h;
			//resolutions[i].vertical = v;


			ResItem resolution = new ResItem();
			resolution.horizontal = h;
			resolution.vertical = v;

			resolutions.Add(resolution);
			Debug.Log(resolutions[i].horizontal + " x " + resolutions[i].vertical);

			if (Screen.width == resolutions[i].horizontal && Screen.height == resolutions[i].vertical)
			{
				foundRes = true;
				selectedRes = i;
				UpdateRes();
			}
			//if (reso[i].width == Screen.currentResolution.width &&
			//	reso[i].height == Screen.currentResolution.height)
			//{
			//	selectedRes = i;
			//	UpdateRes(selectedRes);
			//}
		}

		//bool foundRes = false;
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
            selectedDisplay = 1;
            UpdateDisplayMode();
        }
        else if (!Screen.fullScreen)
        {
			//foundDisplayModes = false;
			selectedDisplay = 0;
			UpdateDisplayMode();
		}

	}

	//public void ResLeft()
	//{
	//	selectedRes--;
	//	if (selectedRes < 0)
	//	{
	//		selectedRes = 0;
	//	}
	//	UpdateRes(selectedRes);
	//}

	//public void ResRight()
	//{
	//	selectedRes++;
	//	if (selectedRes > options.Count - 1)
	//	{
	//		selectedRes = options.Count - 1;
	//	}
	//	UpdateRes(selectedRes);
	//}
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


	//public void UpdateRes(int resolutionIndex)
	//{
	//       Resolution resolution = reso[resolutionIndex];
	//	ResLabel.text = resolution.width.ToString() + " : " + resolution.height.ToString();
	//       Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
	//}
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
        if (selectedDisplay == 1)
        {
            Screen.fullScreen = true;
        }
		if (selectedDisplay == 0)
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