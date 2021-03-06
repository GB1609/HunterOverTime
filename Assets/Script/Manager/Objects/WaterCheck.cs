﻿using UnityEngine;

public class WaterCheck : MonoBehaviour
{
    public Animator _animator;
    public Transform playerTransform;
    public Camera _camera;

// Start is called before the first frame update
    void Start()
    {
        _animator.SetInteger(MovementParameterEnum.Swim, 0);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
            _animator.SetInteger(MovementParameterEnum.Swim, 1);
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            Debug.Log("exit");
            _animator.SetInteger(MovementParameterEnum.Swim, 2);
        }
    }
}