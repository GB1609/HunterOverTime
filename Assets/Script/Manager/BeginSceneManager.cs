using System;
using System.Collections.Generic;
using UnityEngine;

public class BeginSceneManager : MonoBehaviour
{
    public Camera camera;
    public int speed;
    public ParticleSystem particleSystem;

    public Vector3 positionOption;
    public Quaternion rotationOption;

    public Vector3 positionSelect;
    public Quaternion rotationSelect;
    private bool inSelection;
    private bool inSettings;

    private List<GameObject> objects;
    private Camera onLineCamera;
    private String sceneToLoad;
    private Vector3 tempPosition;

    private Quaternion tempRotation;

    private void Start()
    {
        inSettings = inSelection = false;
        sceneToLoad = "";
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
        sceneToLoad = mission;
        ActiveTeleporter();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (particleSystem.gameObject.activeSelf && other.gameObject.CompareTag("Teleporter"))
            switch (sceneToLoad)
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
            inSettings = true;
        }
        else if (other.gameObject.CompareTag("SelectMissionButton"))
        {
            changeView();
            transform.position = positionSelect;
            transform.rotation = rotationSelect;
            inSelection = true;
        }
    }

    private void changeView()
    {
        GetComponent<MouseLook>().enabled = false;
        tempPosition = transform.position;
        tempRotation = transform.rotation;
        Cursor.lockState = CursorLockMode.Confined;
    }

    void CameraMove()
    {
        if (!(inSettings || inSelection))
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
        transform.rotation = tempRotation;
        transform.position = tempPosition;
        camera.transform.position += camera.transform.forward * -1 * 3;
        inSettings = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ExitSelectMission()
    {
        GetComponent<MouseLook>().enabled = true;
        transform.rotation = tempRotation;
        transform.position = tempPosition;
        transform.position += transform.forward * -1 * 3;
        inSelection = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}