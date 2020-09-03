    using UnityEngine;

public class CameraMapManager : MonoBehaviour
{
    public Transform playerTransform;

    public int sensitivy;

    private float minimumX = -360F;
    private float maximumX = 360F;
    public int distances;


    void Start()
    {
        transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y + distances,
            playerTransform.position.z);
        transform.rotation = Quaternion.Euler(new Vector3(90, playerTransform.rotation.y, 0));
    }

    void LateUpdate()
    {
        transform.position = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z);
        float rotationX = Input.GetAxis("Mouse X") * sensitivy;
        rotationX = ClampAngleHorizontal(rotationX, minimumX, maximumX);
        Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.back);
        transform.localRotation = transform.rotation * xQuaternion;
    }

    public static float ClampAngleHorizontal(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}