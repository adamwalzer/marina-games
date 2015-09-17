using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
	public const int TORNADO_CHANNEL = 10;
	public const int LIGHTING_CHANNEL = 11;
	public const int Start_END_CHANNEL = 12;

	public enum Fade {
		In,
		Out
	}

	public enum Channel {
		Common,
		Fly01,
		Fly02
	}

	public int audioSourcesCount = 10;
	public int specificAudioSourcesCount = 3;
	public float musicFadeTime = 3.0f;

	private float _soundVolume = 1.0f;
	private bool _muteSound = false;
	private float _musicVolume = 1.0f;
	private bool _muteMusic = false;

	private AudioSource[] soundPool;
	private AudioSource musicSource;

	void Awake ()
	{
		init ();
	}

	void init ()
	{
		GameObject go = new GameObject ("Sound Channels");
		go.transform.parent = transform;
		soundPool = new AudioSource[audioSourcesCount + specificAudioSourcesCount];
		for (int i = 0; i < audioSourcesCount + specificAudioSourcesCount; i++) {
			AudioSource src = go.AddComponent<AudioSource> ();
			soundPool [i] = src;
			src.playOnAwake = false;
		}

		go = new GameObject("Music channel");
		go.transform.parent = transform;
		musicSource = go.AddComponent<AudioSource>();
		musicSource.playOnAwake = false;

		soundVolume = PlayerPrefs.GetFloat("soundVolume", 1.0f);
		musicVolume = PlayerPrefs.GetFloat("musicVolume", 1.0f);
		muteSound = PlayerPrefs.GetInt("muteSound", 0) == 1 ? true : false;
		muteMusic = PlayerPrefs.GetInt("muteMusic", 0) == 1 ? true : false;
	}

	public float soundVolume {
		get {
			return _soundVolume;
		}
		set {
			_soundVolume = Mathf.Clamp01(value);
			PlayerPrefs.SetFloat("soundVolume", _soundVolume);
			//todo
//			NGUITools.soundVolume = _soundVolume;

			foreach(AudioSource s in soundPool) {
				s.volume = innerSoundVolume;
			}
		}
	}

	public bool muteSound {
		get {
			return _muteSound;
		}
		set {
			_muteSound = value;
			PlayerPrefs.SetInt("muteSound", _muteSound ? 1 : 0);
			//todo
//			NGUITools.soundVolume = _muteSound ? 0 : _soundVolume;

			foreach(AudioSource s in soundPool) {
				s.volume = innerSoundVolume;
			}
		}
	}

	public float musicVolume {
		get {
			return _musicVolume;
		}
		set {
			_musicVolume = Mathf.Clamp01(value);
			PlayerPrefs.SetFloat("musicVolume", _musicVolume);
			musicSource.volume = innerMusicVolume;
		}
	}

	public bool muteMusic {
		get {
			return _muteMusic;
		}
		set {
			_muteMusic = value;
			PlayerPrefs.SetInt("muteMusic", _muteMusic ? 1 : 0);
			musicSource.volume = innerMusicVolume;
		}
	}

	private float innerSoundVolume {
		get {
			return _muteSound ? 0.0f : _soundVolume;
		}
	}

	private float innerMusicVolume {
		get {
			return _muteMusic ? 0.0f : _musicVolume;
		}
	}

	public void saveSettings() {
		PlayerPrefs.Save();
	}

	public int freeAudioSourceCount ()
	{
		int res = 0;
		for (int i = 0; i < soundPool.Length; i++) {
			if (soundPool [i].isPlaying) {
				res++;
			}
		}
		return soundPool.Length - res;
	}
	
	int findFreeAudioSource ()
	{
		for (int i = 0; i < soundPool.Length; i++) {
			AudioSource src = soundPool [i];
			if (src.isPlaying == false)
				return i;
		}

		return -1;
	}
	
	public void playSound (AudioClip clip)
	{
		playSound (0, clip, 1.0f);
	}
	
	public void playSound (AudioClip clip, float pitch)
	{
		playSound (0, clip, pitch);
	}
	
	public void playSound (float delay, AudioClip clip, float pitch)
	{
		if (clip == null) {
			Debug.LogWarning ("null AudioClip!");
			return;
		}

		int channelId = findFreeAudioSource ();
		playSoundOnChannel(channelId, delay, clip, pitch, false);
	}

	public void playSoundOnChannel(int channelId, AudioClip clip, bool looped) {
		playSoundOnChannel(channelId, 0, clip, 1.0f, looped);
	}

	public void playSoundOnChannel(int channelId, float delay, AudioClip clip, float pitch, bool looped) {
		if (channelId < 0 || channelId >= soundPool.Length) return;
		AudioSource src = soundPool[channelId];

		if (src && !src.isPlaying) {
			src.pitch = pitch;
			src.clip = clip;
			src.volume = innerSoundVolume;
			src.loop = looped;
			if (delay <= 0)
				src.Play ();
			else
				src.PlayDelayed (delay);
		}
	}

	public void stopSoundOnChannel(int channelId) {
		if (channelId < 0 || channelId >= soundPool.Length) return;
		AudioSource src = soundPool[channelId];
		if (src) {
			src.Stop();
		}
	}

	public void playSound (float startPause, AudioClip clip)
	{
		playSound (startPause, clip, 1.0f);
	}

	public void playMusic(AudioClip music, bool looped) {
		musicSource.clip = music;
		musicSource.volume = innerMusicVolume;
		musicSource.loop = looped;
		musicSource.Play();
	}

	public void stopMusic(AudioClip music) {
		if (musicSource.clip == music) {
			musicSource.Stop();
		}
	}
}
