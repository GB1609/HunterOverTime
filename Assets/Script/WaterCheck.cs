using UnityEngine;

public class WaterCheck : MonoBehaviour
{
    public Animator _animator;
    public Transform playerTransform;
    public Camera _camera;

// Start is called before the first frame update
    void Start()
    {
        _animator.SetInteger(MovementParameterEnum.SWIM, 0);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
            _animator.SetInteger(MovementParameterEnum.SWIM, 1);
    }

    private void OnTriggerStay(Collider other)
    {
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            Debug.Log("exit");
            _animator.SetInteger(MovementParameterEnum.SWIM, 2);
        }
    }
}