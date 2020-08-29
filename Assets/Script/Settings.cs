using System;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_POST_PROCESSING_STACK_V2

#endif

public class Settings : MonoBehaviour
{
    public Dropdown _resolutionDropdown;
    private const string QUALITY_LEVEL_KEY = "QualityLevel";
    private const string RESOLUTION_WIDTH_KEY = "ScreenResolutionWidth";
    private const string RESOLUTION_HEIGHT_KEY = "ScreenResolutionHeight";
    private const string FULL_SCREEN_KEY = "Fullscreen";
    private Resolution[] resolutions;

    private void Start()
    {
        resolutions = Screen.resolutions;
        int qualityIndex = PlayerPrefs.GetInt(QUALITY_LEVEL_KEY, QualitySettings.GetQualityLevel());
        SetQualityLevel(qualityIndex);
        SetFullscreen(true);
        int val = convertResolution(new int[]
            {resolutions[resolutions.Length - 1].width, resolutions[resolutions.Length - 1].height});
        _resolutionDropdown.value = val;
        int[] r = convertResolution(val);
        SetResolution(r[0], r[1]);
    }

    #region Quality

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

    #endregion

    #region Resolution

    public void SetResolution(int index)
    {
        var vl = convertResolution(index);
        SetResolution(vl[0], vl[1]);
    }

    public void SetResolution(int width, int height)
    {
        Screen.SetResolution(width, height, Screen.fullScreen);
        PlayerPrefs.SetInt(RESOLUTION_WIDTH_KEY, width);
        PlayerPrefs.SetInt(RESOLUTION_HEIGHT_KEY, height);
    }

    #endregion

    #region Fullscreen

    public bool IsFullscreen
    {
        get { return Screen.fullScreen; }
    }

    public void SetFullscreen(bool fullScreen)
    {
        Screen.fullScreen = fullScreen;
        PlayerPrefs.SetInt(FULL_SCREEN_KEY, BoolToInt(fullScreen));
    }

    #endregion

    #region TextureQuality

    #endregion

    private static int BoolToInt(bool value)
    {
        return value ? 1 : 0;
    }

    private static bool IntToBool(int value)
    {
        return value != 0;
    }

    public void SetTextureQuality(int textureLimit)
    {
        QualitySettings.masterTextureLimit = textureLimit;
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
}