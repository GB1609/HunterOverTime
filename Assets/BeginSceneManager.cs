using System;
using System.Collections.Generic;
using UnityEngine;

public class BeginSceneManager : MonoBehaviour
{
    public Camera camera;
    public int speed;
    public String sceneToLoad;

    private void Start()
    {
        // Cursor.lockState = CursorLockMode.Locked;
        Camera.main.ScreenPointToRay(Input.mousePosition);
    }

    // Update is called once per frame
    void Update()
    {
        CameraMove();
    }

    void CameraMove()
    {
        Debug.DrawRay(transform.position, transform.forward * -10000f, Color.green);
        if (Input.GetKey(KeyCode.A))
            transform.position += camera.transform.right * (speed * (-1 * Time.deltaTime));
        else if (Input.GetKey(KeyCode.D))
            transform.position += camera.transform.right * (speed * Time.deltaTime);
        else if (Input.GetKey(KeyCode.W))
            transform.position += camera.transform.forward * (speed * Time.deltaTime);
        else if (Input.GetKey(KeyCode.S))
            transform.position += camera.transform.forward * (speed * (-1 * Time.deltaTime));
    }

    private void goMedieval()
    {
        Managers.Scene.FadeAndLoadScene("MedievalScene");
    }

    private void goMaya()
    {
        Managers.Scene.FadeAndLoadScene("MayaScene");
    }

    private List<GameObject> objects;

    private void OnCollisionEnter(Collision other)
    {
        sceneToLoad = "MedievalScene";
        if (other.gameObject.CompareTag("Teleporter"))
            switch (sceneToLoad)
            {
                case "MedievalScene":
                    goMedieval();
                    break;
                case "MayaScene":
                    goMaya();
                    break;
                default:
                    Debug.Log("No Scene Selected");
                    break;
            }
    }
}