using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ScenesManager))]
[RequireComponent(typeof(SaveManager))]
[RequireComponent(typeof(AudioManager))]
public class Managers : MonoBehaviour
{
    public static ScenesManager Scene { get; private set; }
    public static SaveManager Save { get; private set; }
    public static AudioManager Audio { get; private set; }
    public const string QualityLevelKey = "QualityLevel";


    private List<GameManager> _startSequence;


    void Awake()
    {
        Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, FullScreenMode
            .FullScreenWindow);
        PlayerPrefs.SetInt(QualityLevelKey,QualitySettings.names.Length - 1);
        Scene = GetComponent<ScenesManager>();
        Save = GetComponent<SaveManager>();
        Audio = GetComponent<AudioManager>();

        _startSequence = new List<GameManager>();
        _startSequence.Add(Save);
        _startSequence.Add(Scene);
        _startSequence.Add(Audio);

        StartCoroutine(StartupManagers());
    }

    private IEnumerator StartupManagers()
    {
        foreach (GameManager manager in _startSequence)
        {
            manager.Startup();
        }

        yield return null;

        int numModules = _startSequence.Count;
        int numReady = 0;

        while (numReady < numModules)
        {
            numReady = 0;

            foreach (GameManager manager in _startSequence)
            {
                if (manager.status == ManagerStatus.Started)
                {
                    numReady++;
                }
            }

            yield return null;
        }
    }
}