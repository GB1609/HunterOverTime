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
                var toSum = new Vector3(cameraTransform.forward.x, cameraTransform.forward.y,
                    cameraTransform.forward.z * 5);
                crosshair.SetActive(!crosshair.activeSelf);
                cameraTransform.position = (crosshair.activeSelf)
                    ? cameraTransform.position += toSum
                    : cameraTransform.position -= toSum;
            }
        }
    }
}