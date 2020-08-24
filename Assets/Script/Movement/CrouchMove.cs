using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchMove
{
    public static void MoveBack(Animator animator, Camera camera, Transform transform, int speed)
    {
        animator.SetFloat(MovementParameterEnum.CROUCH, MovementValuesEnum.BACK);
        Vector3 b = new Vector3(camera.transform.forward.x, 0, camera.transform.forward.z) * -1;
        transform.position += b * (Time.deltaTime * speed/2);
    }

    public static void MoveForward(Animator animator, Camera camera, Transform transform, int speed)
    {
        animator.SetFloat(MovementParameterEnum.CROUCH, MovementValuesEnum.FORWARD);
        Vector3 f = new Vector3(camera.transform.forward.x, 0, camera.transform.forward.z);
        transform.position += f * (Time.deltaTime * speed/2);
    }

    public static void MoveLeft(Animator animator, Camera camera, Transform transform, int speed)
    {
        animator.SetFloat(MovementParameterEnum.CROUCH, MovementValuesEnum.LEFT);
        Vector3 l = new Vector3(camera.transform.right.x, 0, camera.transform.right.z) * -1;
        transform.position += l * (Time.deltaTime * speed/2);
    }

    public static void MoveRight(Animator animator, Camera camera, Transform transform, int speed)
    {
        animator.SetFloat(MovementParameterEnum.CROUCH, MovementValuesEnum.RIGHT);
        Vector3 r = new Vector3(camera.transform.right.x, 0, camera.transform.right.z);
        transform.position += r * (Time.deltaTime * speed/2);
    }
}
