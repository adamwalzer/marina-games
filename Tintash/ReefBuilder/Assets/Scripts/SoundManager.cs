using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {


	public AudioClip []clips;
	private static SoundManager instance;
	
	private AudioSource audioSrc;
	
	public static SoundManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = FindObjectOfType(typeof(SoundManager)) as SoundManager;
				if (instance == null)
				{
					instance = new GameObject("SoundManager").AddComponent<SoundManager>();
				}
			}
			return instance;
		}
	}
	
	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			if (instance != this)
			{
				Destroy(this); // remove self, but don't destroy the gameobject its attached to. i.e. don't kill the host object.
				return;
			}
		}
		Setup();
	}
	private void Setup()
	{
		if (audioSrc == null)
		{
			audioSrc = gameObject.GetComponent<AudioSource>();
		}
		if (audioSrc == null)
		{
			audioSrc = gameObject.AddComponent<AudioSource>();
			audioSrc.playOnAwake = false;
		}
	}
	
	public void Play(AudioClip clip)
	{
		audioSrc.PlayOneShot(clip, AudioListener.volume);
	}
	
	public void ClickSound()
	{
	Play(clips[1]);
	}
	public void PlayFiveGroupCleared()
	{
		Play(clips[0]);
	}
	public void PlayCanon()
	{
		Play(clips[2]);
	}
	public void PlayColoumnCleared()
	{
		Play(clips[3]);
	}
	public void LevelFailed()
	{
		Play(clips[4]);
	}
	public void LevelComplete()
	{
		Play(clips[5]);
	}
	public void PlayRowCleared()
	{
		Play(clips[6]);
	}
}
