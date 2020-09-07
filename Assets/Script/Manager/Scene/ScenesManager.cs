using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.Manager
{
    public class ScenesManager : MonoBehaviour, GameManager
    {
        public ManagerStatus status { get; private set; }

        public CanvasGroup transition;
        public float fadeDuration = 1f;
        public string startingSceneName;

        private bool isFading;

        public void Startup()
        {
            status = ManagerStatus.Started;

            transition.alpha = 1f;

            StartCoroutine(LoadSceneAndSetActive(startingSceneName));

            StartCoroutine(Fade(0f));
        }

        public void FadeAndLoadScene(string sceneName)
        {
            if (!isFading)
            {
                StartCoroutine(FadeAndSwitchScenes(sceneName));
            }
        }

        private IEnumerator FadeAndSwitchScenes(string sceneName)
        {
            Managers.Audio.PlayTransitionSound();
            yield return StartCoroutine(Fade(1f));

            yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);

            yield return StartCoroutine(LoadSceneAndSetActive(sceneName));

            yield return StartCoroutine(Fade(0f));
        }

        private IEnumerator LoadSceneAndSetActive(string sceneName)
        {
            Managers.Audio.StopMusic();
            yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
            switch (sceneName)
            {
                case "BeginScene":
                    Managers.Audio.PlayBeginSceneMusic();
                    break;
                case "MedievalScene":
                    Managers.Audio.PlayMedievalMusic();
                    break;
                case "MayaScene":
                    Managers.Audio.PlayMayaMusic();
                    break;
                default:
                    Managers.Audio.PlayBeginSceneMusic();
                    break;
            }
        }

        private IEnumerator Fade(float finalAlpha)
        {
            isFading = true;

            transition.blocksRaycasts = true;

            float fadeSpeed = Mathf.Abs(transition.alpha - finalAlpha) / fadeDuration;

            while (!Mathf.Approximately(transition.alpha, finalAlpha))
            {
                transition.alpha = Mathf.MoveTowards(transition.alpha, finalAlpha,
                    fadeSpeed * Time.deltaTime);

                yield return null;
            }

            isFading = false;

            transition.blocksRaycasts = false;
        }
    }
}