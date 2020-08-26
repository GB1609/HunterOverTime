using UnityEngine;

public class CameraController : MonoBehaviour {

    private float moveSpeed = 0.5f;
    private float scrollSpeed = 10f;

    void Update () {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) {
            transform.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")) * (moveSpeed * Time.deltaTime);
        }

        if (Input.GetAxis("Mouse ScrollWheel") != 0) {
            transform.position += new Vector3(0, -Input.GetAxis("Mouse ScrollWheel"), 0) * (scrollSpeed * Time.deltaTime);
        }
    }

}