using UnityEngine;

namespace Script.Camera
{
    public class PlayerCameraManager : MonoBehaviour
    {

        public Transform cameraTransform;

        public GameObject crosshair;

        // Start is called before the first frame update
        void Start()
        {
            crosshair.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                crosshair.SetActive(!crosshair.activeSelf);
                cameraTransform.position = (crosshair.activeSelf)
                    ? cameraTransform.position += cameraTransform.forward * 5
                    : cameraTransform.position -= cameraTransform.forward * 5;
            }
        }
    }
}