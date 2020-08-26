using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScenesManager : MonoBehaviour, GameManager
{
    public ManagerStatus status { get; private set; }

    public CanvasGroup faderCanvasGroup;
    public float fadeDuration = 1f;
    public string startingSceneName;

    private bool isFading;

    public void Startup()
    {
        status = ManagerStatus.Started;

        faderCanvasGroup.alpha = 1f;

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
        yield return StartCoroutine(Fade(1f));

        yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);

        yield return StartCoroutine(LoadSceneAndSetActive(sceneName));

        yield return StartCoroutine(Fade(0f));
    }

    private IEnumerator LoadSceneAndSetActive(string sceneName)
    {
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
        // switch (sceneName)
        // {
        // 	case "BeginScene":
        // 		Managers.Audio.PlayMenuMusic();
        // 		break;
        // 	case "Hub":
        // 		Managers.Audio.PlayHubMusic();
        // 		Managers.Save.Save();
        // 		break;
        // 	case "Arena":
        //               Managers.Audio.PlayArenaMusic();
        //               Managers.Save.Save();
        // 		break;
        //           case "Catacomb":
        //               Managers.Audio.PlayCatacombMusic();
        //               Managers.Save.Save();
        //               break;
        //           case "Finale":
        //               Managers.Audio.StopMusic();
        //               break;
        //           case "Tutorial":
        //               Managers.Audio.PlayTutorialMusic();
        //               break;
        //           default:
        // 		Managers.Audio.PlayMenuMusic();
        // 		break;
        // }
    }

    private IEnumerator Fade(float finalAlpha)
    {

        isFading = true;

        faderCanvasGroup.blocksRaycasts = true;

        float fadeSpeed = Mathf.Abs(faderCanvasGroup.alpha - finalAlpha) / fadeDuration;

        while (!Mathf.Approximately(faderCanvasGroup.alpha, finalAlpha))
        {
            faderCanvasGroup.alpha = Mathf.MoveTowards(faderCanvasGroup.alpha, finalAlpha,
                fadeSpeed * Time.deltaTime);

            yield return null;
        }

        isFading = false;

        faderCanvasGroup.blocksRaycasts = false;
    }
    void Awake ()
    {
        DontDestroyOnLoad (gameObject);
    }
}