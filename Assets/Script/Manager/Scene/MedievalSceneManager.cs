using DevionGames.UIWidgets;
using UnityEngine;

namespace Script.Manager
{
    public class MedievalSceneManager : MonoBehaviour
    {
        public bool win;
        public Canvas inGame;
        public Canvas instructions;
        public Canvas settings;
        public DialogBox dialog;

        // Start is called before the first frame update
        void Start()
        {
            inGame.gameObject.SetActive(false);
            settings.gameObject.SetActive(false);
            instructions.gameObject.SetActive(true);
            Managers.Pause();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyUp(KeyCode.Escape) && !instructions.gameObject.activeSelf && !Managers.PAUSE)
            {
                Managers.Pause();
                inGame.gameObject.SetActive(false);
                settings.gameObject.SetActive(true);
            }
            else if (Input.GetKeyUp(KeyCode.Escape) && Managers.PAUSE)
                Resume();
        }

        public void ShowExit()
        {
            dialog.gameObject.SetActive(true);
            dialog.GetComponent<CanvasGroup>().alpha = 1;
            dialog.transform.localScale = Vector3.one;
        }

        public void UnShowExit()
        {
            dialog.gameObject.SetActive(false);
            dialog.GetComponent<CanvasGroup>().alpha = 0;
            dialog.transform.localScale = Vector3.zero;
        }

        public void Resume()
        {
            inGame.gameObject.SetActive(true);
            instructions.gameObject.SetActive(false);
            settings.gameObject.SetActive(false);
            Managers.Resume();
        }

        public void Win(UnityEngine.Camera camera)
        {
            Managers.Pause();
            inGame.gameObject.SetActive(false);
            instructions.gameObject.SetActive(false);
            settings.gameObject.SetActive(false);
            Managers.Win(camera);
        }

        public void Die(UnityEngine.Camera camera)
        {
            Managers.Pause();
            inGame.gameObject.SetActive(false);
            instructions.gameObject.SetActive(false);
            settings.gameObject.SetActive(false);
            Managers.Die(camera);
        }
    }
}