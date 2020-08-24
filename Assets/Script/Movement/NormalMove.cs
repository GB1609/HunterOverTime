using UnityEngine;

public static class NormalMove
{
    public static void MoveBack(Animator animator, Camera camera, Transform transform, int speed)
    {
        animator.SetFloat(MovementParameterEnum.WALK, MovementValuesEnum.BACK);
        Vector3 b = new Vector3(camera.transform.forward.x, 0, camera.transform.forward.z) * -1;
        transform.position += b * (Time.deltaTime * speed);
    }

    public static void MoveForward(Animator animator, Camera camera, Transform transform, int speed)
    {
        animator.SetFloat(MovementParameterEnum.WALK, MovementValuesEnum.FORWARD);
        Vector3 f = new Vector3(camera.transform.forward.x, 0, camera.transform.forward.z);
        transform.position += f * (Time.deltaTime * speed);
    }

    public static void MoveLeft(Animator animator, Camera camera, Transform transform, int speed)
    {
        animator.SetFloat(MovementParameterEnum.WALK, MovementValuesEnum.LEFT);
        Vector3 l = new Vector3(camera.transform.right.x, 0, camera.transform.right.z) * -1;
        transform.position += l * (Time.deltaTime * speed);
    }

    public static void MoveRight(Animator animator, Camera camera, Transform transform, int speed)
    {
        animator.SetFloat(MovementParameterEnum.WALK, MovementValuesEnum.RIGHT);
        Vector3 r = new Vector3(camera.transform.right.x, 0, camera.transform.right.z);
        transform.position += r * (Time.deltaTime * speed);
    }

    public static void Jump(Animator animator, Transform transform, Camera camera, int speed, Rigidbody rigidbody)
    {
        animator.SetFloat(MovementParameterEnum.WALK, MovementValuesEnum.FORWARD);
        animator.SetBool(MovementParameterEnum.JUMP, true);
        Vector3 u = camera.transform.up;
        Vector3 f = new Vector3(camera.transform.forward.x, 0, camera.transform.forward.z);
        int upConstants = 4;
        int fConstants = 2;
        transform.position += u * (upConstants * (Time.deltaTime * speed));
        transform.position += f * (fConstants * (Time.deltaTime * speed));
        rigidbody.AddForce(Vector3.up * upConstants);
    }
}