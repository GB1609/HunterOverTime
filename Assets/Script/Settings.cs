using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

#if UNITY_POST_PROCESSING_STACK_V2

#endif

namespace Script
{
    public class Settings : MonoBehaviour
    {
        [FormerlySerializedAs("_resolutionDropdown")]
        public TMP_Dropdown resolutionDropdown;

        public GameObject musicSlider;
        public Slider quality;
        public Toggle fullscreen;

        private void Start()
        {
            int val = Managers.Setting.returnVal();
            resolutionDropdown.value = val;
            musicSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("MusicVolume");
            fullscreen.isOn = Screen.fullScreen;
            quality.value = PlayerPrefs.GetInt(Managers.QualityLevelKey);
        }

        public void SetQualityLevel(float index)
        {
            int ind = Mathf.RoundToInt(index);
            SetQualityLevel(ind);
        }

        public void SetQualityLevel(int index)
        {
            Managers.Setting.SetQualityLevel(index);
        }

        public void SetResolution(int index)
        {
            Managers.Setting.SetResolution(index);
        }

        public void SetResolution(int width, int height)
        {
            Managers.Setting.SetResolution(width, height);
        }

        public void SetFullscreen(bool fullScreen)
        {
            Managers.Setting.SetFullscreen(fullScreen);
        }

        public void SetAudioLevel(float vl)
        {
            Managers.Audio.ChangeVolume(musicSlider.GetComponent<Slider>().value);
        }

        public void Exit()
        {
            Application.Quit();
        }
    }
}