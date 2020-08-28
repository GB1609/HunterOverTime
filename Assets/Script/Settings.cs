using UnityEngine;
#if UNITY_POST_PROCESSING_STACK_V2
using UnityEngine.Rendering.PostProcessing;

#endif

public class Settings : MonoBehaviour
{
    private const string QUALITY_LEVEL_KEY = "QualityLevel";
    private const string RESOLUTION_WIDTH_KEY = "ScreenResolutionWidth";
    private const string RESOLUTION_HEIGHT_KEY = "ScreenResolutionHeight";
    private const string FULL_SCREEN_KEY = "Fullscreen";
    private Resolution[] resolutions;
    private int qualties;

    private void Start()
    {
        resolutions = Screen.resolutions;
        qualties = QualitySettings.names.Length / 4;
        int qualityIndex = PlayerPrefs.GetInt(QUALITY_LEVEL_KEY, QualitySettings.GetQualityLevel());
        SetQualityLevel(qualityIndex);
        PlayerPrefs.GetInt(RESOLUTION_WIDTH_KEY, Screen.currentResolution.width);
        PlayerPrefs.GetInt(RESOLUTION_HEIGHT_KEY, Screen.currentResolution.height);
        SetFullscreen(true);
        SetTextureQuality(3);
        Resolution r = resolutions[resolutions.Length - 1];
        SetResolution(r.width, r.height);
        QualitySettings.SetQualityLevel(0);
    }

    #region Quality

    public void SetQualityLevel(float index)
    {
        int ind = Mathf.RoundToInt(index);
        if (QualitySettings.GetQualityLevel() != ind)
            if (QualitySettings.GetQualityLevel() != ind)
                if (index > QualitySettings.GetQualityLevel())
                    QualitySettings.IncreaseLevel();
                else
                    QualitySettings.DecreaseLevel();
    }

    public void SetQualityLevel(int index)
    {
        if (QualitySettings.GetQualityLevel() != index)
            if (index > QualitySettings.GetQualityLevel())
                QualitySettings.IncreaseLevel();
            else
                QualitySettings.DecreaseLevel();
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
        if (IsStandalone && Screen.fullScreen)
        {
            Screen.SetResolution(width, height, Screen.fullScreen);
            PlayerPrefs.SetInt(RESOLUTION_WIDTH_KEY, width);
            PlayerPrefs.SetInt(RESOLUTION_HEIGHT_KEY, height);
        }
    }

    public void IncreaseResolution()
    {
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            Resolution resolution = Screen.resolutions[i];
            if (resolution.width > Screen.currentResolution.width)
            {
                SetResolution(i);
                break;
            }
        }
    }

    public void DecreaseResolution()
    {
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            Resolution resolution = Screen.resolutions[i];
            if (resolution.width == Screen.currentResolution.width && i > 0)
            {
                SetResolution(i - 1);
                break;
            }
        }
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

    public void SetEffectAntialiasing(bool state)
    {
#if UNITY_POST_PROCESSING_STACK_V2
        PostProcessLayer layer = Camera.main.GetComponent<PostProcessLayer>();
        if (layer == null)
        {
            return;
        }

        if (state)
        {
            layer.antialiasingMode = PostProcessLayer.Antialiasing.TemporalAntialiasing;
        }
        else
        {
            layer.antialiasingMode = PostProcessLayer.Antialiasing.None;
        }
#endif
    }

    public void SetEffectBloom(bool state)
    {
#if UNITY_POST_PROCESSING_STACK_V2
        PostProcessVolume volume = Camera.main.GetComponent<PostProcessVolume>();
        if (volume == null)
        {
            return;
        }

        Bloom bloom;
        if (volume.profile.TryGetSettings<Bloom>(out bloom))
        {
            bloom.active = state;
        }
#endif
    }

    public void SetEffectVignette(bool state)
    {
#if UNITY_POST_PROCESSING_STACK_V2
        PostProcessVolume volume = Camera.main.GetComponent<PostProcessVolume>();
        if (volume == null)
        {
            return;
        }

        Vignette vignette;
        if (volume.profile.TryGetSettings<Vignette>(out vignette))
        {
            vignette.active = state;
        }
#endif
    }

    public void SetEffectAmbient(bool state)
    {
#if UNITY_POST_PROCESSING_STACK_V2
        PostProcessVolume volume = Camera.main.GetComponent<PostProcessVolume>();
        if (volume == null)
        {
            return;
        }

        AmbientOcclusion ambient;
        if (volume.profile.TryGetSettings<AmbientOcclusion>(out ambient))
        {
            ambient.active = state;
        }
#endif
    }

    public bool IsStandalone
    {
        get
        {
#if UNITY_STANDALONE
            return true;
#else
				return false;
#endif
        }
    }

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
}