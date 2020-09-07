using System.Collections;
using System.Collections.Generic;
using Script.Manager;
using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(ScenesManager))]
// [RequireComponent(typeof(SaveManager))]
[RequireComponent(typeof(AudioManager))]
[RequireComponent(typeof(SettingManager))]
public class Managers : MonoBehaviour
{
    public static bool PAUSE;
    public static ScenesManager Scene { get; private set; }
    public static AudioManager Audio { get; private set; }
    public static SettingManager Setting { get; private set; }
    public const string QualityLevelKey = "QualityLevel";

    private static VideoPlayer[] _videos;

    private void Start()
    {
        PAUSE = false;
        _videos = gameObject.GetComponents<VideoPlayer>();
    }

    private List<GameManager> _startSequence;


    void Awake()
    {
        Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, FullScreenMode
            .FullScreenWindow);
        PlayerPrefs.SetInt(QualityLevelKey, QualitySettings.names.Length - 1);
        Scene = GetComponent<ScenesManager>();
        // Save = GetComponent<SaveManager>();
        Audio = GetComponent<AudioManager>();
        Setting = GetComponent<SettingManager>();

        _startSequence = new List<GameManager>();
        // _startSequence.Add(Save);
        _startSequence.Add(Scene);
        _startSequence.Add(Audio);
        _startSequence.Add(Setting);

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

    public static void Pause()
    {
        PAUSE = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0;
    }

    public static void Resume()
    {
        PAUSE = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
    }

    public static void Die(Camera camera)
    {
        _videos[1].targetCamera = camera;
        _videos[1].Play();
        _videos[1].loopPointReached += CheckOver;
    }

    public static void Win(Camera camera)
    {
        _videos[0].targetCamera = camera;
        _videos[0].Play();
        _videos[0].loopPointReached += CheckOver;
    }

    private static void CheckOver(VideoPlayer vp)
    {
        Resume();
        Scene.FadeAndLoadScene("Managers");
    }
}