using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunMove
{
    public static void MoveForward(Animator animator, Camera camera, Transform transform, int speed, bool boosted)
    {
        animator.SetFloat(MovementParameterEnum.WALK, MovementValuesEnum.FORWARD);
        Vector3 f = new Vector3(camera.transform.forward.x, 0, camera.transform.forward.z);
        if (!boosted)
            transform.position += f * (3 * (Time.deltaTime * speed));
        else
            transform.position += f * (6 * (Time.deltaTime * speed));
    }
}