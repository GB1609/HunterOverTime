using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;

public class AudioManager : MonoBehaviour, GameManager
{
    public ManagerStatus status { get; private set; }

    [SerializeField] private AudioMixer masterMixer;
    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] public string transitionSound;

    [FormerlySerializedAs("menuBGMusic")] [SerializeField]
    public string menuBgMusic;

    [FormerlySerializedAs("medievalBGMusic")] [SerializeField]
    public string medievalBgMusic;

    [FormerlySerializedAs("mayaBGMusic")] [SerializeField]
    public string mayaBgMusic;

    private AudioClip _transitionAudioClip;
    private AudioClip _menuAudioClip;
    private AudioClip _medievalAudioClip;
    private AudioClip _mayaAudioClip;

    public void Startup()
    {
        PlayerPrefs.SetFloat("MusicVolume", -36.85f);
        UpdateMusicVolume();
        _menuAudioClip = Resources.Load<AudioClip>(menuBgMusic);
        _transitionAudioClip = (AudioClip) Resources.Load(transitionSound);
        _medievalAudioClip = (AudioClip) Resources.Load(medievalBgMusic);
        _mayaAudioClip = (AudioClip) Resources.Load(mayaBgMusic);
        status = ManagerStatus.Started;
    }

    public void UpdateMusicVolume()
    {
        Debug.Log("NEW AUDIO=" + PlayerPrefs.GetFloat("MusicVolume"));
        masterMixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume"));
    }

    public void PlayBeginSceneMusic()
    {
        PlayMusic(_menuAudioClip);
    }

    public void PlayTransitionSound()
    {
        PlayMusic(_transitionAudioClip);
    }

    private void PlayMusic(AudioClip clip)
    {
        backgroundMusic.clip = clip;
        backgroundMusic.Play();
    }

    public void StopMusic()
    {
        backgroundMusic.Stop();
    }

    public void ChangeVolume(float value)
    {
        PlayerPrefs.SetFloat("MusicVolume", value);
        Managers.Audio.UpdateMusicVolume();
    }

}