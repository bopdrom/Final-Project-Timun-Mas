using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("-------- Audio Source --------")]
    public AudioSource musicSource;
    public AudioSource SFXSource;

    [Header("-------- Musics Audio Clip --------")]
    public AudioClip bgMusic;

	[Header("-------- UI Elements & SFXs Audio Clip --------")]
	public AudioClip btnHover;
	public AudioClip btnClick;
	public AudioClip cutsceneNext;
	public AudioClip transition;
	public AudioClip walk;
	public AudioClip kick;
	public AudioClip defeat;
	public AudioClip blockMoving;
	public AudioClip blockDestroyed;
	public AudioClip finish;
	public AudioClip caught;

	private void Start()
	{
		musicSource.clip = bgMusic;
		musicSource.Play();
	}

	public void PlaySFX(AudioClip clip)
	{
		SFXSource.PlayOneShot(clip);
	}
}
