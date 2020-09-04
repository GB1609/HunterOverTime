using UnityEngine;

public class RunMove
{
    public static void MoveForward(Animator animator, Camera camera, Transform transform, int speed, bool boosted)
    {
        Vector3 f = new Vector3(camera.transform.forward.x, 0, camera.transform.forward.z);
        if (!boosted)
            transform.position += f * (3 * (Time.deltaTime * speed));
        else
            transform.position += f * (6 * (Time.deltaTime * speed));
        animator.SetFloat(MovementParameterEnum.Walk, MovementValuesEnum.FORWARD);
        animator.SetFloat(MovementParameterEnum.Run, MovementValuesEnum.RUN_SLOWLY);
    }

    public static void JumpForward(Animator animator, Camera camera, Transform transform, int speed,
        Rigidbody rigidbody, bool boosted)
    {
        animator.SetFloat(MovementParameterEnum.Walk, MovementValuesEnum.FORWARD);
        animator.SetBool(MovementParameterEnum.Jump, true);
        Vector3 u = camera.transform.up;
        Vector3 f = new Vector3(camera.transform.forward.x, 0, camera.transform.forward.z);
        int upConstants = (boosted) ? 8 : 5;
        int fConstants = (boosted) ? 8 : 5;
        transform.position += u * (upConstants * (Time.deltaTime * speed));
        transform.position += f * (fConstants * (Time.deltaTime * speed));
        rigidbody.AddForce(Vector3.up * upConstants);
    }
}