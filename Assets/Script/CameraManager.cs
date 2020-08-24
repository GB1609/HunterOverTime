using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform cameraTransform;

    public GameObject crosshair;

    // Start is called before the first frame update
    void Start()
    {
        crosshair = GameObject.FindGameObjectWithTag("CrossHair");
        crosshair.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            crosshair.SetActive(!crosshair.activeSelf);
            cameraTransform.position = (crosshair.activeSelf)
                ? cameraTransform.position += cameraTransform.forward * 3
                : cameraTransform.position -= cameraTransform.forward * 3;
        }

        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            //TODO camera dall0alto
        }
    }
}