using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

#if UNITY_POST_PROCESSING_STACK_V2

#endif

public class Settings : MonoBehaviour
{
    [FormerlySerializedAs("_resolutionDropdown")]
    public Dropdown resolutionDropdown;

    public GameObject musicSlider;


    private const string QualityLevelKey = "QualityLevel";
    private const string ResolutionWidthKey = "ScreenResolutionWidth";
    private const string ResolutionHeightKey = "ScreenResolutionHeight";
    private const string FullScreenKey = "Fullscreen";
    private Resolution[] _resolutions;

    private void Start()
    {
        musicSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("MusicVolume");
        _resolutions = Screen.resolutions;
        int qualityIndex = PlayerPrefs.GetInt(QualityLevelKey, QualitySettings.GetQualityLevel());
        SetQualityLevel(qualityIndex);
        SetFullscreen(true);
        int val = convertResolution(new int[]
            {_resolutions[_resolutions.Length - 1].width, _resolutions[_resolutions.Length - 1].height});
        resolutionDropdown.value = val;
        int[] r = convertResolution(val);
        SetResolution(r[0], r[1]);
    }

    public void SetQualityLevel(float index)
    {
        int ind = Mathf.RoundToInt(index);
        SetQualityLevel(ind);
    }

    public void SetQualityLevel(int index)
    {
        if (QualitySettings.GetQualityLevel() != index)
            if (index > QualitySettings.GetQualityLevel())
                QualitySettings.IncreaseLevel(true);
            else
                QualitySettings.IncreaseLevel(true);
    }

    public void SetResolution(int index)
    {
        var vl = convertResolution(index);
        SetResolution(vl[0], vl[1]);
    }

    public void SetResolution(int width, int height)
    {
        Screen.SetResolution(width, height, Screen.fullScreen);
        PlayerPrefs.SetInt(ResolutionWidthKey, width);
        PlayerPrefs.SetInt(ResolutionHeightKey, height);
    }

    public void SetFullscreen(bool fullScreen)
    {
        Screen.fullScreen = fullScreen;
        PlayerPrefs.SetInt(FullScreenKey, BoolToInt(fullScreen));
    }

    private static int BoolToInt(bool value)
    {
        return value ? 1 : 0;
    }

    public int[] convertResolution(int vl)
    {
        switch (vl)
        {
            case 0: return new int[] {800, 600};
            case 1: return new int[] {1024, 769};
            case 2: return new int[] {1280, 960};
            case 3: return new int[] {1920, 1080};
            case 4: return new int[] {2560, 1440};
            default: return new int[] { };
        }
    }

    public int convertResolution(int[] vl)
    {
        int[] res = {800, 1024, 1280, 1920, 2560};
        switch (vl[0])
        {
            case 800:
                return 0;
            case 1024:
                return 1;
            case 1280:
                return 2;
            case 1920:
                return 3;
            case 2560:
                return 4;
            default:
                var min = 2560;
                var toReturn = 0;
                for (int i = 0; i < res.Length; i++)
                {
                    if (Math.Abs(res[i] - vl[0]) < min)
                    {
                        min = Math.Abs(res[i] - vl[0]);
                        toReturn = i;
                    }
                }

                return toReturn;
        }
    }

    public void SetAudioLevel(float vl)
    {
        Managers.Audio.ChangeVolume(musicSlider.GetComponent<Slider>().value);
    }
}