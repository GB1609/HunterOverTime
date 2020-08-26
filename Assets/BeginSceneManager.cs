using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeginSceneManager : MonoBehaviour
{
    public Camera camera;
    public int speed;


    // Update is called once per frame
    void Update()
    {
        CameraMove();
    }

    void CameraMove()
    {
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

    private List<GameObject> objects;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Teleporter"))
            goMedieval();
    }
}