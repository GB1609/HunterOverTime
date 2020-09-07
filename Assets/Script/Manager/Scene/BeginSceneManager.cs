using System;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Manager.Scene
{
    public class BeginSceneManager : MonoBehaviour
    {
        public UnityEngine.Camera camera;
        public int speed;
        public ParticleSystem particleSystem;

        public Vector3 positionOption;
        public Quaternion rotationOption;

        public Vector3 positionSelect;
        public Quaternion rotationSelect;
        private bool _inSelection;
        private bool _inSettings;

        private List<GameObject> _objects;
        private UnityEngine.Camera _onLineCamera;
        private String _sceneToLoad;
        private Vector3 _tempPosition;

        private Quaternion _tempRotation;

        private void Start()
        {
            _inSettings = _inSelection = false;
            _sceneToLoad = "";
            Cursor.lockState = CursorLockMode.Locked;
            particleSystem.gameObject.SetActive(false);
        }


        // Update is called once per frame
        void Update()
        {
            CameraMove();
        }

        public void setMission(string mission)
        {
            _sceneToLoad = mission;
            ActiveTeleporter();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (particleSystem.gameObject.activeSelf && other.gameObject.CompareTag("Teleporter"))
                switch (_sceneToLoad)
                {
                    case "MedievalScene":
                        GOMedieval();
                        break;
                    case "MayaScene":
                        GOMaya();
                        break;
                    default:
                        Debug.Log("No Scene Selected");
                        break;
                }

            else if (other.gameObject.CompareTag("OptionButton"))
            {
                changeView();
                transform.position = positionOption;
                transform.rotation = rotationOption;
                _inSettings = true;
            }
            else if (other.gameObject.CompareTag("SelectMissionButton"))
            {
                changeView();
                transform.position = positionSelect;
                transform.rotation = rotationSelect;
                _inSelection = true;
            }
        }

        private void changeView()
        {
            GetComponent<MouseLook>().enabled = false;
            _tempPosition = transform.position;
            _tempRotation = transform.rotation;
            Cursor.lockState = CursorLockMode.Confined;
        }

        void CameraMove()
        {
            if (!(_inSettings || _inSelection))
            {
                if (Input.GetKey(KeyCode.A))
                    camera.transform.position += camera.transform.right * (speed * (-1 * Time.deltaTime));
                else if (Input.GetKey(KeyCode.D))
                    camera.transform.position += camera.transform.right * (speed * Time.deltaTime);
                else if (Input.GetKey(KeyCode.W))
                    camera.transform.position += camera.transform.forward * (speed * Time.deltaTime);
                else if (Input.GetKey(KeyCode.S))
                    camera.transform.position += camera.transform.forward * (speed * (-1 * Time.deltaTime));
            }
        }

        private void GOMedieval()
        {
            Managers.Scene.FadeAndLoadScene("MedievalScene");
        }

        private void GOMaya()
        {
            Managers.Scene.FadeAndLoadScene("MayaScene");
        }

        private void ActiveTeleporter()
        {
            particleSystem.gameObject.SetActive(true);
        }

        public void ExitSettings()
        {
            GetComponent<MouseLook>().enabled = true;
            transform.rotation = _tempRotation;
            transform.position = _tempPosition;
            camera.transform.position += camera.transform.forward * -1 * 3;
            _inSettings = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void ExitSelectMission()
        {
            GetComponent<MouseLook>().enabled = true;
            transform.rotation = _tempRotation;
            transform.position = _tempPosition;
            transform.position += transform.forward * -1 * 3;
            _inSelection = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}