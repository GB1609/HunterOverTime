using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour, GameManager {
    public ManagerStatus status { get; private set; }

	[SerializeField] private AudioMixer masterMixer;
	[SerializeField] private AudioSource backgroundMusic;
	[SerializeField] private string menuBGMusic;

    public AudioClip[] yells;

    //TODO: Aggiungere qui dentro le varie musiche

    public void Start() {
		UpdateVolumes();
	}

    public void Startup() {
        Debug.Log("Audio manager starting...");
		UpdateVolumes();
        status = ManagerStatus.Started;
    }

	public void UpdateSoundFXVolume() {
		masterMixer.SetFloat("SFXVolume", PlayerPrefs.GetFloat("SFXVolume"));
	}

	public void UpdateMusicVolume() {
		masterMixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume"));
	}

	public void UpdateVolumes() {
		UpdateSoundFXVolume();
		UpdateMusicVolume();
	}

	public void PlayBeginSceneMusic() {
		PlayMusic((AudioClip)Resources.Load("Sounds/"+menuBGMusic));
	}

    private void PlayMusic(AudioClip clip) {
		backgroundMusic.clip = clip;
		backgroundMusic.Play();
	}
	public void StopMusic() {
		backgroundMusic.Stop();
	}



}
