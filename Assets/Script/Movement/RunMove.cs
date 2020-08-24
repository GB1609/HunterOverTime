using System;
using UnityEngine;

public class RunMove
{
    public static void MoveForward(Animator animator, Camera camera, Transform transform, int speed, bool boosted)
    {
        animator.SetFloat(MovementParameterEnum.WALK, MovementValuesEnum.FORWARD);
        animator.SetFloat(MovementParameterEnum.RUN, MovementValuesEnum.RUN_SLOWLY);
        Vector3 f = new Vector3(camera.transform.forward.x, 0, camera.transform.forward.z);
        if (!boosted)
            transform.position += f * (3 * (Time.deltaTime * speed));
        else
            transform.position += f * (6 * (Time.deltaTime * speed));
    }

    public static void JumpForward(Animator animator, Camera camera, Transform transform, int speed,
        Rigidbody rigidbody, bool boosted)
    {
        animator.SetFloat(MovementParameterEnum.WALK, MovementValuesEnum.FORWARD);
        animator.SetBool(MovementParameterEnum.JUMP, true);
        animator.SetTrigger(MovementParameterEnum.JUMP_TRIGGER);
        Vector3 u = camera.transform.up;
        Vector3 f = new Vector3(camera.transform.forward.x, 0, camera.transform.forward.z);

        int upConstants = (boosted) ? 6 : 3;
        int fConstants = (boosted) ? 6 : 3;
        transform.position += u * (upConstants * (Time.deltaTime * speed));
        transform.position += f * (fConstants * (Time.deltaTime * speed));
        rigidbody.AddForce(Vector3.up * upConstants);
    }
}