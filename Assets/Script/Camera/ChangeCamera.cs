using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
    public GameObject mini;
    public GameObject middle;

    // Start is called before the first frame update
    void Start()
    {
        mini.SetActive(true);
        middle.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (mini.activeSelf) mini.SetActive(false);
            if (!middle.activeSelf) middle.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            if (!mini.activeSelf) mini.SetActive(true);
            if (middle.activeSelf) middle.SetActive(false);
        }
    }
}